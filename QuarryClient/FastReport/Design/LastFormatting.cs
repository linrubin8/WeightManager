using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Design
{
  internal class LastFormatting
  {
    public Border Border;
    public FillBase Fill;
    public Font Font;
    public HorzAlign HorzAlign;
    public VertAlign VertAlign;
    public FillBase TextFill;
    public int Angle;

    public void SetFormatting(ReportComponentBase c)
    {
      if (c != null)
      {
        if (Border != null && c.FlagUseBorder)
          c.Border = Border.Clone();
        if (c.FlagUseFill)
          c.Fill = Fill.Clone();
      }
      if (c is TextObject)
      {
        TextObject c1 = c as TextObject;
        if (Font != null)
          c1.Font = Font;
        c1.HorzAlign = HorzAlign;
        c1.VertAlign = VertAlign;
        c1.TextFill = TextFill.Clone();
        c1.Angle = Angle;
      }
    }

    public LastFormatting()
    {
      Border = new Border();
      Fill = new SolidFill();
      TextFill = new SolidFill(Color.Black);
      Font = Config.DesignerSettings.DefaultFont;
    }
  }
}
