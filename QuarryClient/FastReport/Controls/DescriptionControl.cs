using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.DevComponents.DotNetBar;
using System.Drawing;
using System.Reflection;
using FastReport.Utils;
using FastReport.Editor.Syntax.Parsers;
using FastReport.Data;

namespace FastReport.Controls
{
  internal class DescriptionControl : Panel
  {
    private LabelX lblDescription;

    private void RecalcDescriptionSize()
    {
      // hack to force GetPreferredSize do the work
      lblDescription.UseMnemonic = false;
      int maxWidth = Width - SystemInformation.VerticalScrollBarWidth - 8;
      Size preferredSize = lblDescription.GetPreferredSize(new Size(maxWidth, 0));
      lblDescription.Size = preferredSize;
    }
    
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      RecalcDescriptionSize();
    }
    
    public void ShowDescription(Report report, object info)
    {
      string descr = "";
      
      if (info is SystemVariable)
      {
        descr = "<b>" + (info as SystemVariable).Name + "</b>";
        descr += "<br/><br/>" + ReflectionRepository.DescriptionHelper.GetDescription(info.GetType());
      }
      else if (info is MethodInfo)
      {
        descr = report.CodeHelper.GetMethodSignature(info as MethodInfo, true);
        descr += "<br/><br/>" + ReflectionRepository.DescriptionHelper.GetDescription(info as MethodInfo);

        foreach (ParameterInfo parInfo in (info as MethodInfo).GetParameters())
        {
          // special case - skip "thisReport" parameter
          if (parInfo.Name == "thisReport")
            continue;
          
          string s = ReflectionRepository.DescriptionHelper.GetDescription(parInfo);
          s = s.Replace("<b>", "{i}").Replace("</b>:", "{/i}").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("{i}", "<i>").Replace("{/i}", "</i>");
          descr += "<br/><br/>" + s;
        }
      }
      
      lblDescription.Text = descr.Replace("\t", "<br/>");
      RecalcDescriptionSize();
    }

    public DescriptionControl()
    {
      lblDescription = new LabelX();
      lblDescription.AntiAlias = false;
      lblDescription.BackColor = SystemColors.Window;
      lblDescription.PaddingLeft = 2;
      lblDescription.PaddingRight = 2;
      lblDescription.PaddingTop = 2;
      lblDescription.TextLineAlignment = StringAlignment.Near;
      lblDescription.UseMnemonic = false;
      lblDescription.WordWrap = true;
      Controls.Add(lblDescription);

      AutoScroll = true;
      BackColor = SystemColors.Window;
    }
  }
}
