using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using FastReport.Editor.Syntax;
using FastReport.Editor.Syntax.Parsers;
using Microsoft.VisualBasic;
using System.Drawing;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Design.PageDesigners.Code;

namespace FastReport.Code
{
	internal partial class VbCodeHelper : CodeHelperBase
    {
        #region Private Methods
        private string GetEquivalentKeyword(string s)
        {
            if (s.EndsWith("[]"))
                return GetEquivalentKeyword1(s.Substring(0, s.Length - 2)) + "()";
            return GetEquivalentKeyword1(s);
        }

        private string GetEquivalentKeyword1(string s)
        {
            switch (s)
            {
                case "DateTime":
                    return "Date";
                case "Int16":
                    return "Short";
                case "UInt16":
                    return "UShort";
                case "Int32":
                    return "Integer";
                case "UInt32":
                    return "UInteger";
                case "Int64":
                    return "Long";
                case "UInt64":
                    return "ULong";
            }
            return s;
        }
        #endregion

        #region Protected Methods
        protected override NetSyntaxParser CreateSyntaxParser()
        {
            NetSyntaxParser parser = new VbParser();
            parser.Options = SyntaxOptions.Outline | SyntaxOptions.CodeCompletion |
              SyntaxOptions.SyntaxErrors | SyntaxOptions.QuickInfoTips | SyntaxOptions.SmartIndent;
            parser.CodeCompletionChars = SyntaxConsts.ExtendedNetCodeCompletionChars.ToCharArray();
            return parser;
        }

        protected override void CompareEvent(string eventParams, string codeLine, List<string> eventNames)
        {
            // split code line to words
            // for example: "Private Sub NewClick(ByVal sender As object, ByVal e As System.EventArgs)"
            string[] codeWords = codeLine.Split(new string[] { " ", ",", "(", ")",
        "Private", "Public", "Protected", "Virtual", "Override", "Sub", "ByVal", "As" }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            if (i < codeWords.Length)
            {
                // now we get: "NewClick sender object e System.EventArgs"
                string eventName = codeWords[i];
                string pars = "";
                i += 2;
                // first argument
                if (i < codeWords.Length)
                    pars = codeWords[i] + ",";
                i += 2;
                if (i < codeWords.Length)
                {
                    string secondArg = codeWords[i];
                    if (secondArg.IndexOf('.') != -1)
                    {
                        // if second argument is, for example, "System.EventArgs", take only "EventArgs" part
                        string[] splitSecondArg = secondArg.Split(new char[] { '.' });
                        secondArg = splitSecondArg[splitSecondArg.Length - 1];
                    }
                    pars += secondArg + ",";
                }
                if (String.Compare(eventParams, pars, true) == 0)
                    eventNames.Add(eventName);
            }
        }

        protected override string GetTypeDeclaration(Type type)
        {
            if (type.IsGenericType)
            {
                string result = type.Name;
                result = result.Substring(0, result.IndexOf('`'));
                result += "(Of ";

                foreach (Type elementType in type.GetGenericArguments())
                {
                    result += GetTypeDeclaration(elementType) + ",";
                }

                result = result.Substring(0, result.Length - 1) + ")";
                return result;
            }
            else
            {
                string typeName = type.Name;
                typeName = typeName.Replace("[]", "()");
                return typeName;
            }
        }
        #endregion

        #region Public Methods
        public override string EmptyScript()
        {
            return "Imports System\r\nImports System.Collections\r\nImports System.Collections.Generic\r\n" +
              "Imports System.ComponentModel\r\nImports System.Windows.Forms\r\nImports System.Drawing\r\n" +
              "Imports Microsoft.VisualBasic\r\n" +
              "Imports FastReport\r\nImports FastReport.Data\r\nImports FastReport.Dialog\r\nImports FastReport.Table\r\n" +
              "Imports FastReport.Barcode\r\nImports FastReport.Utils\r\n\r\nNamespace FastReport\r\n" +
              "  Public Class ReportScript\r\n\r\n  End Class\r\nEnd Namespace\r\n";
        }

        public override int GetPositionToInsertOwnItems(string scriptText)
        {
            int pos = scriptText.IndexOf("Public Class ReportScript");
            if (pos == -1)
                return -1;
            return scriptText.IndexOf('\n', pos) + 1;
        }

        public override string AddField(Type type, string name)
        {
            name = name.Replace(" ", "_");
            return "    Public " + name + " as Global." + type.FullName + "\r\n";
        }

		// Simon: 注释原方法
		//public override string BeginCalcExpression()
		//{
		//	return "    Private Function CalcExpression(ByVal expression As String, ByVal Value as Global.FastReport.Variant) As Object\r\n      ";
		//}

		// Simon: 注释原方法
		//public override string AddExpression(string expr, string value)
		//{
		//	expr = expr.Replace("\"", "\"\"");
		//	return "If expression = \"" + expr + "\" Then\r\n        Return " + value + "\r\n      End If\r\n      ";
		//}

        public override string EndCalcExpression()
        {
            return "Return Nothing\r\n    End Function\r\n\r\n";
        }

        public override string ReplaceColumnName(string name, Type type)
        {
            string typeName = GetTypeDeclaration(type);
            string result = "CType(Report.GetColumnValue(\"" + name + "\"";
            result += "), " + typeName + ")";
            return result;
        }

        public override string ReplaceParameterName(Parameter parameter)
        {
            string typeName = GetTypeDeclaration(parameter.DataType);
            return "CType(Report.GetParameterValue(\"" + parameter.FullName + "\"), " + typeName + ")";
        }

        public override string ReplaceVariableName(Parameter parameter)
        {
            string typeName = GetTypeDeclaration(parameter.DataType);
            return "CType(Report.GetVariableValue(\"" + parameter.FullName + "\"), " + typeName + ")";
        }

        public override string ReplaceTotalName(string name)
        {
            return "Report.GetTotalValue(\"" + name + "\")";
        }

        public override string GenerateInitializeMethod()
        {
            Hashtable events = new Hashtable();
            string reportString = StripEventHandlers(events);
            string result = "    Private Sub InitializeComponent\r\n      ";

            // form the reportString
            result += "Dim reportString As String = _\r\n        ";

            int totalLength = 0;
            while (reportString.Length > 0)
            {
                string part = "";
                if (reportString.Length > 80)
                {
                    part = reportString.Substring(0, 80);
                    reportString = reportString.Substring(80);
                }
                else
                {
                    part = reportString;
                    reportString = "";
                }

                part = "\"" + part.Replace("\"", "\"\"").Replace("\u201c", "\"\"").Replace("\u201d", "\"\"") + "\"";
                part = part.Replace("\r\n", "\" + ChrW(13) + ChrW(10) + \"");
                part = part.Replace("\r", "\" + ChrW(13) + \"");
                part = part.Replace("\n", "\" + ChrW(10) + \"");
                result += part;
                if (reportString != "")
                {
                    if (totalLength > 1024)
                    {
                        totalLength = 0;
                        result += "\r\n      reportString = reportString + ";
                    }
                    else
                        result += " + _\r\n        ";
                    totalLength += part.Length;
                }
                else
                {
                    result += "\r\n      ";
                }
            }

            result += "LoadFromString(reportString)\r\n      ";
            result += "InternalInit()\r\n      ";

            // form objects' event handlers
            foreach (DictionaryEntry de in events)
            {
                result += "AddHandler " + de.Key.ToString() + ", AddressOf " +
                  de.Value.ToString() + "\r\n      ";
            }

            result += "\r\n    End Sub\r\n\r\n";
            result += "    Public Sub New()\r\n      InitializeComponent()\r\n    End Sub\r\n";
            return result;
        }

        public override string ReplaceClassName(string scriptText, string className)
        {
            // replace the first occurence of "ReportScript"
            string replace = "Class ReportScript";
            int index = scriptText.IndexOf(replace);
            scriptText = scriptText.Remove(index, replace.Length);
            scriptText = scriptText.Insert(index, "Class " + className + "\r\n    Inherits Report");
            // replace other items
            return scriptText.Replace("Private Function CalcExpression", "Protected Overrides Function CalcExpression");
        }

        public override string GetMethodSignature(MethodInfo info, bool fullForm)
        {
            string result = info.Name + "(";
            string fontBegin = "<font color=\"Blue\">";
            string fontEnd = "</font>";

            System.Reflection.ParameterInfo[] pars = info.GetParameters();
            foreach (System.Reflection.ParameterInfo par in pars)
            {
                // special case - skip "thisReport" parameter
                if (par.Name == "thisReport")
                    continue;

                string modifier = "ByVal";
                if (par.IsOptional)
                    modifier = "Optional " + modifier;
                object[] attr = par.GetCustomAttributes(typeof(ParamArrayAttribute), false);
                if (attr.Length > 0)
                    modifier += " ParamArray";
                result += fullForm ? fontBegin + modifier + fontEnd + " " + par.Name + " " + fontBegin + "As" + fontEnd + " " : "";
                result += (fullForm ? fontBegin : "") + GetEquivalentKeyword(par.ParameterType.Name) + (fullForm ? fontEnd : "");
#if DOTNET_4
                if (par.IsOptional && fullForm)
                    result += CodeUtils.GetOptionalParameter(par, CodeUtils.Language.Vb);
#endif
                result += ", ";
            }

            if (result.EndsWith(", "))
                result = result.Substring(0, result.Length - 2);

            result += ")";
            if (fullForm)
                result += " " + fontBegin + "As " + info.ReturnType.Name + fontEnd;
            return result;
        }

        public override string GetMethodSignatureAndBody(MethodInfo info)
        {
            string result = info.Name + "(";
            result = "    Private Function " + result;

            System.Reflection.ParameterInfo[] pars = info.GetParameters();
            foreach (System.Reflection.ParameterInfo par in pars)
            {
                // special case - skip "thisReport" parameter
                if (par.Name == "thisReport")
                    continue;

                string parName = "_" + par.Name;
                string modifier = "ByVal";
                if (par.IsOptional)
                    modifier = "Optional " + modifier;
                object[] attr = par.GetCustomAttributes(typeof(ParamArrayAttribute), false);
                if (attr.Length > 0)
                    modifier += " ParamArray";
                result += modifier + " " + parName + " As ";
                result += GetTypeDeclaration(par.ParameterType);
#if DOTNET_4
                if (par.IsOptional)
                    result += CodeUtils.GetOptionalParameter(par, CodeUtils.Language.Vb);
#endif
                result += ", ";
            }

            if (result.EndsWith(", "))
                result = result.Substring(0, result.Length - 2);
            result += ")";

            result += " As " + GetTypeDeclaration(info.ReturnType);
            result += "\r\n";
            result += "      Return Global." + info.ReflectedType.Namespace + "." +
              info.ReflectedType.Name + "." + info.Name + "(";

            foreach (System.Reflection.ParameterInfo par in pars)
            {
                string parName = "_" + par.Name;
                // special case - handle "thisReport" parameter
                if (parName == "_thisReport")
                    parName = "Report";

                result += parName + ", ";
            }

            if (result.EndsWith(", "))
                result = result.Substring(0, result.Length - 2);
            result += ")\r\n";
            result += "    End Function\r\n";
            result += "\r\n";

            return result;
        }

        public override void LocateHandler(string eventName)
        {
            List<ISyntaxNode> methods = new List<ISyntaxNode>();
            EnumSyntaxNodes(methods, Parser.SyntaxTree.Root, NetNodeType.Method);

            foreach (ISyntaxNode method in methods)
            {
                if (method.Name == eventName)
                {
                    Editor.Focus();
                    Editor.Position = method.Position;
                    Editor.Position = new Point(method.Position.X + 2, method.Position.Y + 1);
                    break;
                }
            }
        }

        public override bool AddHandler(Type eventType, string eventName)
        {
            // get delegate params
            MethodInfo invoke = eventType.GetMethod("Invoke");
            System.Reflection.ParameterInfo[] pars = invoke.GetParameters();
            string eventParams = "";
            if (pars.Length == 2)
                eventParams = "(ByVal sender As object, ByVal e As " + pars[1].ParameterType.Name + ")";
            else
            {
                FRMessageBox.Error(String.Format(Res.Get("Messages,DelegateError"), eventType.ToString()));
                return false;
            }

            List<ISyntaxNode> classes = new List<ISyntaxNode>();
            EnumSyntaxNodes(classes, Parser.SyntaxTree.Root, NetNodeType.Class);
            foreach (ISyntaxNode node in classes)
            {
                if (node.Name == "ReportScript")
                {
                    Point startPos = node.Position;
                    Point endPos = new Point(0, startPos.Y + node.Size.Height);
                    Editor.Lines.Insert(endPos.Y, "");
                    Editor.Lines.Insert(endPos.Y + 1, Indent(2) + "Private Sub " + eventName + eventParams);
                    Editor.Lines.Insert(endPos.Y + 2, Indent(3));
                    Editor.Lines.Insert(endPos.Y + 3, Indent(2) + "End Sub");
                    Editor.Focus();
                    Editor.Position = new Point(3 * CodePageSettings.TabSize, endPos.Y + 2);
                    return true;
                }
            }
            FRMessageBox.Error(Res.Get("Messages,EventError"));
            return false;
        }

        public override System.CodeDom.Compiler.CodeDomProvider GetCodeProvider()
        {
            return new VBCodeProvider();
        }
#endregion

        public VbCodeHelper(Report report) : base(report)
        {
        }
    }

}