using FastReport.Data;
using FastReport.Engine;
using FastReport.Utils;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
#if DOTNET_4
using System.Collections.Concurrent;
#endif

namespace FastReport.Code
{
    internal class AssemblyDescriptor
    {
#if DOTNET_4
        private static ConcurrentDictionary<string, Assembly> FAssemblyCache;
#else
        private static Dictionary<string, Assembly> FAssemblyCache;
#endif
        private Assembly FAssembly;
        private object FInstance;
        private Report FReport;
        private FastString FScriptText;
        private Hashtable FExpressions;
        private List<SourcePosition> FSourcePositions;
        private int FInsertLine;
        private int FInsertPos;
        private bool FNeedCompile;

        public Assembly Assembly
        {
            get { return FAssembly; }
        }

        public object Instance
        {
            get { return FInstance; }
        }

        public Report Report
        {
            get { return FReport; }
        }

        public Hashtable Expressions
        {
            get { return FExpressions; }
        }

        private void InsertItem(string text, string objName)
        {
            string[] lines = text.Split(new char[] { '\r' });
            FScriptText.Insert(FInsertPos, text);
            SourcePosition pos = new SourcePosition(objName, FInsertLine, FInsertLine + lines.Length - 2);
            FSourcePositions.Add(pos);
            FInsertLine += lines.Length - 1;
            FInsertPos += text.Length;
        }

        private void InitField(string name, object c)
        {
            FieldInfo info = Instance.GetType().GetField(name,
              BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            info.SetValue(Instance, c);
        }

        private void InitFields()
        {
            InitField("Report", Report);
            InitField("Engine", Report.Engine);
            ObjectCollection allObjects = Report.AllObjects;
            foreach (Base c in allObjects)
            {
                if (!String.IsNullOrEmpty(c.Name))
                    InitField(c.Name, c);
            }
        }

        private string GetErrorObjectName(int errorLine)
        {
            foreach (SourcePosition pos in FSourcePositions)
            {
                if (errorLine >= pos.Start && errorLine <= pos.End)
                {
                    return pos.SourceObject;
                }
            }
            return "";
        }

        private int GetScriptLine(int errorLine)
        {
            int start = FSourcePositions[0].Start;
            int end = FSourcePositions[FSourcePositions.Count - 1].End;
            if (errorLine >= start && errorLine <= end)
                return -1;
            if (errorLine > end)
                return errorLine - (end - start + 1);
            return errorLine;
        }

        private string ReplaceDataItems(string expression)
        {
            FindTextArgs args = new FindTextArgs();
            args.Text = new FastString(expression);
            args.OpenBracket = "[";
            args.CloseBracket = "]";

            while (args.StartIndex < args.Text.Length)
            {
                expression = CodeUtils.GetExpression(args, true);
                if (expression == "")
                    break;

                if (DataHelper.IsValidColumn(Report.Dictionary, expression))
                {
                    Type type = DataHelper.GetColumnType(Report.Dictionary, expression);
                    expression = Report.CodeHelper.ReplaceColumnName(expression, type);
                }
                else if (DataHelper.IsValidParameter(Report.Dictionary, expression))
                {
                    expression = Report.CodeHelper.ReplaceParameterName(DataHelper.GetParameter(Report.Dictionary, expression));
                }
                else if (DataHelper.IsValidTotal(Report.Dictionary, expression))
                {
                    expression = Report.CodeHelper.ReplaceTotalName(expression);
                }
                else
                {
                    expression = "[" + ReplaceDataItems(expression) + "]";
                }

                args.Text = args.Text.Remove(args.StartIndex, args.EndIndex - args.StartIndex);
                args.Text = args.Text.Insert(args.StartIndex, expression);
                args.StartIndex += expression.Length;
            }
            return args.Text.ToString();
        }

        private bool ContansAssembly(StringCollection assemblies, string assembly)
        {
            string asmName = Path.GetFileName(assembly);
            foreach (string a in assemblies)
            {
                string asmName1 = Path.GetFileName(a);
                if (String.Compare(asmName, asmName1, true) == 0)
                    return true;
            }
            return false;
        }

        private void AddFastReportAssemblies(StringCollection assemblies)
        {
            List<ObjectInfo> list = new List<ObjectInfo>();
            RegisteredObjects.Objects.EnumItems(list);

            foreach (ObjectInfo info in list)
            {
                string aLocation = "";
                if (info.Object != null)
                    aLocation = info.Object.Assembly.Location;
                else if (info.Function != null)
                    aLocation = info.Function.DeclaringType.Assembly.Location;

                if (aLocation != "" && !ContansAssembly(assemblies, aLocation))
                    assemblies.Add(aLocation);
            }
        }

        private void AddReferencedAssemblies(StringCollection assemblies, string defaultPath)
        {
            foreach (string s in Report.ReferencedAssemblies)
            {
                string location = GetFullAssemblyReference(s, defaultPath);
                if (location != "" && !ContansAssembly(assemblies, location))
                    assemblies.Add(location);
            }
        }

        private string GetFullAssemblyReference(string relativeReference, string defaultPath)
        {
            if (relativeReference == null || relativeReference.Trim() == "")
                return "";

            // Strip off any trailing ".dll" ".exe" if present.
            string dllName = relativeReference;
            if (string.Compare(relativeReference.Substring(relativeReference.Length - 4), ".dll", true) == 0 ||
              string.Compare(relativeReference.Substring(relativeReference.Length - 4), ".exe", true) == 0)
                dllName = relativeReference.Substring(0, relativeReference.Length - 4);

            // See if the required assembly is already present in our current AppDomain
            foreach (Assembly currAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (string.Compare(currAssembly.GetName().Name, dllName, true) == 0)
                {
                    // Found it, return the location as the full reference.
                    return currAssembly.Location;
                }
            }

            // See if the required assembly is present in the ReferencedAssemblies but not yet loaded
            foreach (AssemblyName assemblyName in System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                if (string.Compare(assemblyName.Name, dllName, true) == 0)
                {
                    // Found it, try to load assembly and return the location as the full reference.
                    try
                    {
                        return System.Reflection.Assembly.ReflectionOnlyLoad(assemblyName.FullName).Location;
                    }
                    catch { }
                }
            }

            // See if the required assembly is present locally
            string path = Path.Combine(defaultPath, relativeReference);
            if (File.Exists(path))
                return path;

            return relativeReference;
        }

        private void AddExpression(string expression, Base source, bool forceSimpleItems)
        {
            if (expression.Trim() == "" || FExpressions.ContainsKey(expression))
                return;

            string expr = expression;
            if (expr.StartsWith("[") && expr.EndsWith("]"))
                expr = expr.Substring(1, expr.Length - 2);

            // skip simple items. Report.Calc does this.
            if (!forceSimpleItems)
            {
                if (DataHelper.IsSimpleColumn(Report.Dictionary, expr) ||
                  DataHelper.IsValidParameter(Report.Dictionary, expr) ||
                  DataHelper.IsValidTotal(Report.Dictionary, expr))
                    return;
            }

            // handle complex expressions, relations
            ExpressionDescriptor descriptor = new ExpressionDescriptor(this);
            FExpressions.Add(expression, descriptor);
            descriptor.MethodName = "CalcExpression";

            if (DataHelper.IsValidColumn(Report.Dictionary, expr))
                expr = "[" + expr + "]";
            else
                expr = expression;
            string expressionCode = ReplaceDataItems(expr);
            InsertItem(Report.CodeHelper.AddExpression(expression, expressionCode), source == null ? "" : source.Name);
            FNeedCompile = true;
        }

        public void AddObjects()
        {
            ObjectCollection allObjects = Report.AllObjects;
            SortedList<string, Base> objects = new SortedList<string, Base>();

            // add all report objects
            InsertItem(Report.CodeHelper.AddField(typeof(Report), "Report") +
              Report.CodeHelper.AddField(typeof(ReportEngine), "Engine"), "Report");
            foreach (Base c in allObjects)
            {
                if (!String.IsNullOrEmpty(c.Name) && !objects.ContainsKey(c.Name))
                    objects.Add(c.Name, c);
            }
            foreach (Base c in objects.Values)
            {
                InsertItem(Report.CodeHelper.AddField(c.GetType(), c.Name), c.Name);
            }

            // add custom script
            string processedCode = "";
            foreach (Base c in objects.Values)
            {
                string customCode = c.GetCustomScript();
                // avoid custom script duplicates
                if (!String.IsNullOrEmpty(customCode) && processedCode.IndexOf(customCode) == -1)
                {
                    InsertItem(customCode, c.Name);
                    processedCode += customCode;
                    FNeedCompile = true;
                }
            }
        }

        public void AddSingleExpression(string expression)
        {
            InsertItem(Report.CodeHelper.BeginCalcExpression(), "");
            AddExpression(expression, null, true);
            InsertItem(Report.CodeHelper.EndCalcExpression(), "");
            FNeedCompile = true;
        }

        public void AddExpressions()
        {
            InsertItem(Report.CodeHelper.BeginCalcExpression(), "");

            ObjectCollection allObjects = Report.AllObjects;
            ObjectCollection l = Report.Dictionary.AllObjects;
            foreach (Base c in l)
            {
                allObjects.Add(c);
            }

            foreach (Base c in allObjects)
            {
                string[] expressions = c.GetExpressions();
                if (expressions != null)
                {
                    foreach (string expr in expressions)
                    {
                        AddExpression(expr, c, false);
                    }
                }
            }

            InsertItem(Report.CodeHelper.EndCalcExpression(), "");
        }

        public void AddFunctions()
        {
            List<ObjectInfo> list = new List<ObjectInfo>();
            RegisteredObjects.Objects.EnumItems(list);

            foreach (ObjectInfo info in list)
            {
                if (info.Function != null)
                {
                    InsertItem(Report.CodeHelper.GetMethodSignatureAndBody(info.Function), "Function");
                }
            }
        }

        public string GenerateReportClass(string className)
        {
            InsertItem(Report.CodeHelper.GenerateInitializeMethod(), "");
            return Report.CodeHelper.ReplaceClassName(FScriptText.ToString(), className);
        }

        public void Compile()
        {
            if (FNeedCompile)
                InternalCompile();
        }

        private void InternalCompile()
        {
            // set the current folder
            string currentFolder = Config.ApplicationFolder;
            if (Config.WebMode)
            {
                try
                {
                    if (Directory.Exists(currentFolder + @"Bin\"))
                        currentFolder += @"Bin\";
                }
                catch
                {
                }
            }
            // Commented by Samuray
            //Directory.SetCurrentDirectory(currentFolder);

            // configure compiler options
            CompilerParameters cp = new CompilerParameters();
            AddFastReportAssemblies(cp.ReferencedAssemblies);
            AddReferencedAssemblies(cp.ReferencedAssemblies, currentFolder);
            cp.GenerateInMemory = true;
            // sometimes the system temp folder is not accessible...
            if (Config.TempFolder != null)
                cp.TempFiles = new TempFileCollection(Config.TempFolder, false);

            // find assembly in cache
            StringBuilder assemblyHashSB = new StringBuilder();
            foreach (string a in cp.ReferencedAssemblies)
                assemblyHashSB.Append(a);
            assemblyHashSB.Append(FScriptText.ToString());
            string assemblyHash = Convert.ToBase64String(new SHA1Managed().ComputeHash(Encoding.Unicode.GetBytes(assemblyHashSB.ToString())));
            Assembly cachedAssembly = null;
            if (FAssemblyCache.TryGetValue(assemblyHash, out cachedAssembly))
            {
                FAssembly = cachedAssembly;
                InitInstance(FAssembly.CreateInstance("FastReport.ReportScript"));
                return;
            }

            // compile report script
            using (CodeDomProvider provider = Report.CodeHelper.GetCodeProvider())
            {
                CompilerResults cr = provider.CompileAssemblyFromSource(cp, FScriptText.ToString());
                FAssembly = null;
                FInstance = null;
                if (cr.Errors.Count > 0)
                {
                    string errors = "";
                    foreach (CompilerError ce in cr.Errors)
                    {
                        int line = GetScriptLine(ce.Line);
                        // error is inside own items
                        if (line == -1)
                        {
                            string errObjName = GetErrorObjectName(ce.Line);
                            errors += String.Format("({0}): " + Res.Get("Messages,Error") + " {1}: {2}", new object[] { errObjName, ce.ErrorNumber, ce.ErrorText }) + "\r\n";
                            if (Report.Designer != null)
                                Report.Designer.ErrorMsg(errObjName + ": " + Res.Get("Messages,Error") + " " + ce.ErrorNumber + ": " + ce.ErrorText, errObjName);
                        }
                        else
                        {
                            errors += String.Format("({0},{1}): " + Res.Get("Messages,Error") + " {2}: {3}", new object[] { line, ce.Column, ce.ErrorNumber, ce.ErrorText }) + "\r\n";
                            if (Report.Designer != null)
                                Report.Designer.ErrorMsg(Res.Get("Messages,Error") + " " + ce.ErrorNumber + ": " + ce.ErrorText, line, ce.Column);
                        }
                    }
                    throw new CompilerException(errors);
                }
                else
                {
#if DOTNET_4
                    FAssemblyCache.TryAdd(assemblyHash, cr.CompiledAssembly);
#else
                    FAssemblyCache.Add(assemblyHash, cr.CompiledAssembly);
#endif
                    FAssembly = cr.CompiledAssembly;
                    InitInstance(FAssembly.CreateInstance("FastReport.ReportScript"));
                }
            }
        }

        public void InitInstance(object instance)
        {
            FInstance = instance;
            InitFields();
        }

        public bool ContainsExpression(string expr)
        {
            return FExpressions.ContainsKey(expr);
        }

        public object CalcExpression(string expr, Variant value)
        {
            return (FExpressions[expr] as ExpressionDescriptor).Invoke(new object[] { expr, value });
        }

        public void InvokeEvent(string name, object[] parms)
        {
            if (String.IsNullOrEmpty(name))
                return;

            string exprName = "event_" + name;
            if (!ContainsExpression(exprName))
            {
                ExpressionDescriptor descriptor = new ExpressionDescriptor(this);
                FExpressions.Add(exprName, descriptor);
                descriptor.MethodName = name;
            }
            try
            {
                (FExpressions[exprName] as ExpressionDescriptor).Invoke(parms);
            }
            catch (TargetInvocationException ex)
            {
                throw (ex.InnerException); // ex now stores the original exception
            }
        }

        public AssemblyDescriptor(Report report, string scriptText)
        {
            FReport = report;
            FScriptText = new FastString(scriptText);
            FExpressions = new Hashtable();
            FSourcePositions = new List<SourcePosition>();
            FInsertPos = Report.CodeHelper.GetPositionToInsertOwnItems(scriptText);
            if (FInsertPos == -1)
            {
                string msg = Res.Get("Messages,ClassError");
                if (Report.Designer != null)
                    Report.Designer.ErrorMsg(msg, null);
                throw new CompilerException(msg);
            }
            else
            {
                string[] lines = scriptText.Substring(0, FInsertPos).Split(new char[] { '\r' });
                FInsertLine = lines.Length;
                if (scriptText != Report.CodeHelper.EmptyScript())
                    FNeedCompile = true;
            }
        }

        static AssemblyDescriptor()
        {
#if DOTNET_4
            FAssemblyCache = new ConcurrentDictionary<string, Assembly>();
#else
            FAssemblyCache = new Dictionary<string, Assembly>();
#endif
        }

        private class SourcePosition
        {
            public string SourceObject;
            public int Start;
            public int End;

            public SourcePosition(string obj, int start, int end)
            {
                SourceObject = obj;
                Start = start;
                End = end;
            }
        }
    }
}
