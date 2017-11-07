using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using FastReport.Editor;
using FastReport.Editor.Syntax;
using FastReport.Editor.Syntax.Parsers;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using FastReport.Data;
using FastReport.Engine;
using FastReport.Design.PageDesigners.Code;
using System.IO;
using FastReport.Utils;

namespace FastReport.Code
{
	internal abstract partial class CodeHelperBase
  {
    #region Fields
    private NetSyntaxParser FParser;
    private Report FReport;
    #endregion

    #region Properties
    public NetSyntaxParser Parser
    {
      get 
      { 
        if (FParser == null)
          FParser = CreateSyntaxParser();
        return FParser; 
      }
      set { FParser = value; }
    }

    public Report Report
    {
      get { return FReport; }
    }
    
    public SyntaxEdit Editor
    {
      get { return Report.Designer.Editor.Edit; }
    }
    #endregion

    #region Protected Methods
    protected virtual NetSyntaxParser CreateSyntaxParser()
    {
      return null;
    }
    
    protected void EnumSyntaxNodes(List<ISyntaxNode> list, ISyntaxNode node, NetNodeType nodeType)
    {
      if ((NetNodeType)node.NodeType == nodeType)
        list.Add(node);

      // scan only "ReportScript" class!
      if ((NetNodeType)node.NodeType == NetNodeType.Class)
      {
        if (String.Compare(node.Name, "ReportScript", true) != 0)
          return;
      }
      
      if (node.ChildList != null)
        foreach (ISyntaxNode n in node.ChildList)
        {
          EnumSyntaxNodes(list, n, nodeType);
        }
    }
    
    protected string StripEventHandlers(Hashtable events)
    {
      using (Report report = new Report())
      {
        report.LoadFromString(Report.SaveToString());
        report.ScriptText = EmptyScript();
        
        List<Base> list = new List<Base>();
        foreach (Base c in report.AllObjects)
        {
          list.Add(c);
        }
        list.Add(report);
        
        foreach (Base c in list)
        {
          PropertyInfo[] props = c.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
          foreach (PropertyInfo info in props)
          {
            if (info.PropertyType == typeof(string) && info.Name.EndsWith("Event"))
            {
              string value = (string)info.GetValue(c, null);
              if (!String.IsNullOrEmpty(value))
              {
                string cName = c.Name + ".";
                if (c is Report)
                  cName = "";
                events.Add(cName + info.Name.Replace("Event", ""), value);
                info.SetValue(c, "", null);
              }
            }
          }
        }
        
        return report.SaveToString();
      }  
    }

    protected string Indent(int num)
    {
      return "".PadLeft(num * CodePageSettings.TabSize, ' ');
    }

    protected abstract void CompareEvent(string eventParams, string codeLine, List<string> eventNames);
    protected abstract string GetTypeDeclaration(Type type);
    #endregion

    #region Public Methods
    public abstract string EmptyScript();
    public abstract bool AddHandler(Type eventType, string eventName);
    public abstract void LocateHandler(string eventName);
    public abstract CodeDomProvider GetCodeProvider();
    public abstract int GetPositionToInsertOwnItems(string scriptText);
    public abstract string AddField(Type type, string name);
    public abstract string BeginCalcExpression();
    public abstract string AddExpression(string expr, string value);
    public abstract string EndCalcExpression();
    public abstract string ReplaceColumnName(string name, Type type);
    public abstract string ReplaceParameterName(Parameter parameter);
    public abstract string ReplaceVariableName(Parameter parameter);
    public abstract string ReplaceTotalName(string name);
    public abstract string GenerateInitializeMethod();
    public abstract string ReplaceClassName(string scriptText, string className);
    public abstract string GetMethodSignature(MethodInfo info, bool fullForm);
    public abstract string GetMethodSignatureAndBody(MethodInfo info);
    
    public List<string> GetEvents(Type eventType)
    {
      // build a string containing the event params. 
      // for example, event type "EventHandler" will generate the following string: "object,EventArgs,"
      string eventParams = "";
      MethodInfo invoke = eventType.GetMethod("Invoke");
      System.Reflection.ParameterInfo[] pars = invoke.GetParameters();
      foreach (System.Reflection.ParameterInfo p in pars)
      {
        eventParams += p.ParameterType.Name + ",";
      }

      // update the syntax tree
      (Report.Designer.ActiveReportTab.Plugins.Find("Code") as CodePageDesigner).UpdateLanguage();

      List<string> result = new List<string>();
      List<ISyntaxNode> methods = new List<ISyntaxNode>();
      EnumSyntaxNodes(methods, FParser.SyntaxTree.Root, NetNodeType.Method);

      foreach (ISyntaxNode method in methods)
      {
        // get a code line, for example: "  private void NewClick(object sender, System.EventArgs e)"
        string codeLine = Editor.Lines[method.Position.Y].Trim();
        // compare it with event params, and add the event name to the result list if succesful
        CompareEvent(eventParams, codeLine, result);
      }
      return result;
    }
    
    public void Locate(int line, int column)
    {
      Editor.Focus();
      Editor.Position = new Point(column - 1, line - 1);
    }
    
    public void RegisterAssemblies()
    {
      NetSyntaxParser parser = Parser as NetSyntaxParser;
 
      // register assemblies
      foreach (string assm in Report.ReferencedAssemblies)
      {
        try
        {
          // GAC assemblies should be registered without an extension
          parser.RegisterAssembly(assm.Replace(".dll", "").Replace(".exe", ""));
        }
        catch
        {
          try
          {
            // not a GAC assembly, try its full name.
            string fullName = assm;
            if (!Path.IsPathRooted(fullName))
              fullName = Config.ApplicationFolder + assm;
            parser.RegisterAssembly(fullName);
          }
          catch
          {
          }
        }  
      }

      // register FastReport.dll
      parser.RegisterAssembly(this.GetType().Assembly);
      
      // register objects
      ObjectCollection allObjects = Report.AllObjects;
      foreach (Base c in allObjects)
      {
        parser.RegisterObject(c.Name, c);
      }
      parser.RegisterObject("Report", Report);
      parser.RegisterObject("Engine", new ReportEngine(Report));
    }
    #endregion
    
    public CodeHelperBase(Report report)
    {
      FReport = report;
    }

  }

}