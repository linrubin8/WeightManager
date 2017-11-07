using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using System.Globalization;
using FastReport.Format;

namespace FastReport.Forms
{
  internal partial class FormatEditorForm : BaseDialogForm
  {
    private TextObjectBase FTextObject;
    private int FFormatIndex;
    private List<FormatBase[]> FFormats;
    private List<int> FSelectedFormatIndices;
  
    public TextObjectBase TextObject
    {
      get { return FTextObject; }
      set 
      {
        FTextObject = value;
        
        // get expressions
        string[] expressions = FTextObject.GetExpressions();
        if (expressions == null || expressions.Length == 0)
          expressions = new string[] { "" };
        cbxExpression.Items.AddRange(expressions);
        
        // disable controls if there is only one expression
        if (expressions.Length < 2)
        {
          lblExpression.Visible = false;
          cbxExpression.Visible = false;
          pc1.Top -= 32;
          btnOk.Top -= 32;
          btnCancel.Top -= 32;
          gbSample.Top -= 32;
          Height -= 32; 
        }

        FFormats = new List<FormatBase[]>();
        FSelectedFormatIndices = new List<int>();

        for (int i = 0; i < expressions.Length; i++)
        {
          FormatBase format = null;
          if (i < FTextObject.Formats.Count)
            format = FTextObject.Formats[i];
          else
            format = new GeneralFormat();
          
          FormatBase[] formats = new FormatBase[] {
            new GeneralFormat(),
            new NumberFormat(),
            new CurrencyFormat(),
            new DateFormat(),
            new TimeFormat(),
            new PercentFormat(),
            new BooleanFormat(),
            new CustomFormat() };
          FFormats.Add(formats);  

          int formatIndex = 0;
          for (int j = 0; j < formats.Length; j++)
          {
            if (formats[j].GetType() == format.GetType())
            {
              formats[j] = format.Clone();
              formatIndex = j;
              break;
            }
          }
          
          FSelectedFormatIndices.Add(formatIndex);
        }

        cbxExpression.SelectedIndex = 0;
      }
    }
    
    public FormatCollection Formats
    {
      get
      {
        FormatCollection collection = new FormatCollection();
        for (int i = 0; i < FFormats.Count; i++)
        {
          FormatBase format = FFormats[i][FSelectedFormatIndices[i]];
          collection.Add(format);
        }
        return collection;
      }
    }
    
    private int FormatIndex
    {
      get { return FFormatIndex; }
      set
      {
        FFormatIndex = value;
        pc1.ActivePageIndex = FSelectedFormatIndices[FFormatIndex];
        UpdateControls();
      }
    }
    
    private FormatBase Format
    {
      get 
      {
        int index = FSelectedFormatIndices[FFormatIndex];
        return FFormats[FFormatIndex][index];
      }
    }
    
    private NumberFormat NumberFormat
    {
      get { return FFormats[FFormatIndex][1] as NumberFormat; }
    }

    private CurrencyFormat CurrencyFormat
    {
      get { return FFormats[FFormatIndex][2] as CurrencyFormat; }
    }

    private DateFormat DateFormat
    {
      get { return FFormats[FFormatIndex][3] as DateFormat; }
    }

    private TimeFormat TimeFormat
    {
      get { return FFormats[FFormatIndex][4] as TimeFormat; }
    }

    private PercentFormat PercentFormat
    {
      get { return FFormats[FFormatIndex][5] as PercentFormat; }
    }

    private BooleanFormat BooleanFormat
    {
      get { return FFormats[FFormatIndex][6] as BooleanFormat; }
    }

    private CustomFormat CustomFormat
    {
      get { return FFormats[FFormatIndex][7] as CustomFormat; }
    }

    private void Change()
    {
      lblSample.Text = Format.GetSampleValue();
    }
    
    private void UpdateControls()
    {
      // number
      cbNumberUseLocale.Checked = NumberFormat.UseLocale;
      if (!NumberFormat.UseLocale)
      {
        udNumberDecimalDigits.Value = NumberFormat.DecimalDigits;
        cbxNumberDecimalSeparator.Text = NumberFormat.DecimalSeparator;
        cbxNumberGroupSeparator.Text = NumberFormat.GroupSeparator;
        cbxNumberNegativePattern.SelectedIndex = NumberFormat.NegativePattern;
      }

      // currency
      cbCurrencyUseLocale.Checked = CurrencyFormat.UseLocale;
      if (!CurrencyFormat.UseLocale)
      {
        udCurrencyDecimalDigits.Value = CurrencyFormat.DecimalDigits;
        cbxCurrencyDecimalSeparator.Text = CurrencyFormat.DecimalSeparator;
        cbxCurrencyGroupSeparator.Text = CurrencyFormat.GroupSeparator;
        cbxCurrencyPositivePattern.SelectedIndex = CurrencyFormat.PositivePattern;
        cbxCurrencyNegativePattern.SelectedIndex = CurrencyFormat.NegativePattern;
        cbxCurrencySymbol.Text = CurrencyFormat.CurrencySymbol;
      }

      // date
      lbxDates.SelectedItem = DateFormat.Format;
      
      // time
      lbxTimes.SelectedItem = TimeFormat.Format;

      // percent
      cbPercentUseLocale.Checked = PercentFormat.UseLocale;
      if (!PercentFormat.UseLocale)
      {
        udPercentDecimalDigits.Value = PercentFormat.DecimalDigits;
        cbxPercentDecimalSeparator.Text = PercentFormat.DecimalSeparator;
        cbxPercentGroupSeparator.Text = PercentFormat.GroupSeparator;
        cbxPercentPositivePattern.SelectedIndex = PercentFormat.PositivePattern;
        cbxPercentNegativePattern.SelectedIndex = PercentFormat.NegativePattern;
        cbxPercentSymbol.Text = PercentFormat.PercentSymbol;
      }

      // boolean
      cbxBooleanFalse.Text = BooleanFormat.FalseText;
      cbxBooleanTrue.Text = BooleanFormat.TrueText;
      
      // custom
      tbCustom.Text = CustomFormat.Format;
    }

    private void cbNumberUseLocale_CheckedChanged(object sender, EventArgs e)
    {
      NumberFormat.UseLocale = cbNumberUseLocale.Checked;
      if (cbNumberUseLocale.Checked)
      {
        NumberFormatInfo info = CultureInfo.CurrentCulture.NumberFormat;
        udNumberDecimalDigits.Value = info.NumberDecimalDigits;
        cbxNumberDecimalSeparator.Text = info.NumberDecimalSeparator;
        cbxNumberGroupSeparator.Text = info.NumberGroupSeparator;
        cbxNumberNegativePattern.SelectedIndex = info.NumberNegativePattern;
      }
      Control[] controls = new Control[] { lblNumberDecimalDigits, udNumberDecimalDigits, 
        lblNumberDecimalSeparator, cbxNumberDecimalSeparator, 
        lblNumberGroupSeparator, cbxNumberGroupSeparator,
        lblNumberNegativePattern, cbxNumberNegativePattern };
      foreach (Control c in controls)
      {
        c.Enabled = !cbNumberUseLocale.Checked;
      }
      Change();
    }

    private void udNumberDecimalDigits_ValueChanged(object sender, EventArgs e)
    {
      NumberFormat.DecimalDigits = (int)udNumberDecimalDigits.Value;
      Change();
    }

    private void cbxNumberDecimalSeparator_TextChanged(object sender, EventArgs e)
    {
      NumberFormat.DecimalSeparator = cbxNumberDecimalSeparator.Text;
      Change();
    }

    private void cbxNumberGroupSeparator_TextChanged(object sender, EventArgs e)
    {
      if (cbxNumberGroupSeparator.Text == Res.Get("Misc,None"))
        NumberFormat.GroupSeparator = "";
      else
        NumberFormat.GroupSeparator = cbxNumberGroupSeparator.Text;
      Change();
    }

    private void cbxNumberNegativePattern_SelectedValueChanged(object sender, EventArgs e)
    {
      NumberFormat.NegativePattern = cbxNumberNegativePattern.SelectedIndex;
      Change();
    }

    private void cbxCurrencyUseLocale_CheckedChanged(object sender, EventArgs e)
    {
      CurrencyFormat.UseLocale = cbCurrencyUseLocale.Checked;
      if (cbCurrencyUseLocale.Checked)
      {
        NumberFormatInfo info = CultureInfo.CurrentCulture.NumberFormat;
        udCurrencyDecimalDigits.Value = info.CurrencyDecimalDigits;
        cbxCurrencyDecimalSeparator.Text = info.CurrencyDecimalSeparator;
        cbxCurrencyGroupSeparator.Text = info.CurrencyGroupSeparator;
        cbxCurrencyPositivePattern.SelectedIndex = info.CurrencyPositivePattern;
        cbxCurrencyNegativePattern.SelectedIndex = info.CurrencyNegativePattern;
        cbxCurrencySymbol.Text = info.CurrencySymbol;
      }
      Control[] controls = new Control[] { lblCurrencyDecimalDigits, udCurrencyDecimalDigits, 
        lblCurrencyDecimalSeparator, cbxCurrencyDecimalSeparator, 
        lblCurrencyGroupSeparator, cbxCurrencyGroupSeparator,
        lblCurrencyPositivePattern, cbxCurrencyPositivePattern, 
        lblCurrencyNegativePattern, cbxCurrencyNegativePattern,
        lblCurrencySymbol, cbxCurrencySymbol };
      foreach (Control c in controls)
      {
        c.Enabled = !cbCurrencyUseLocale.Checked;
      }
      Change();
    }

    private void udCurrencyDecimalDigits_ValueChanged(object sender, EventArgs e)
    {
      CurrencyFormat.DecimalDigits = (int)udCurrencyDecimalDigits.Value;
      Change();
    }

    private void cbxCurrencyDecimalSeparator_TextChanged(object sender, EventArgs e)
    {
      CurrencyFormat.DecimalSeparator = cbxCurrencyDecimalSeparator.Text;
      Change();
    }

    private void cbxCurrencyGroupSeparator_TextChanged(object sender, EventArgs e)
    {
      if (cbxCurrencyGroupSeparator.Text == Res.Get("Misc,None"))
        CurrencyFormat.GroupSeparator = "";
      else
        CurrencyFormat.GroupSeparator = cbxCurrencyGroupSeparator.Text;
      Change();
    }

    private void cbxCurrencyPositivePattern_SelectedValueChanged(object sender, EventArgs e)
    {
      CurrencyFormat.PositivePattern = cbxCurrencyPositivePattern.SelectedIndex;
      Change();
    }

    private void cbxCurrencyNegativePattern_SelectedValueChanged(object sender, EventArgs e)
    {
      CurrencyFormat.NegativePattern = cbxCurrencyNegativePattern.SelectedIndex;
      Change();
    }

    private void cbxCurrencySymbol_TextChanged(object sender, EventArgs e)
    {
      CurrencyFormat.CurrencySymbol = cbxCurrencySymbol.Text;
      Change();
    }

    private void lbxDates_SelectedIndexChanged(object sender, EventArgs e)
    {
      DateFormat.Format = (string)lbxDates.SelectedItem;
      Change();
    }

    private void lbxTimes_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimeFormat.Format = (string)lbxTimes.SelectedItem;
      Change();
    }

    private void cbPercentUseLocale_CheckedChanged(object sender, EventArgs e)
    {
      PercentFormat.UseLocale = cbPercentUseLocale.Checked;
      if (cbPercentUseLocale.Checked)
      {
        NumberFormatInfo info = CultureInfo.CurrentCulture.NumberFormat;
        udPercentDecimalDigits.Value = info.PercentDecimalDigits;
        cbxPercentDecimalSeparator.Text = info.PercentDecimalSeparator;
        cbxPercentGroupSeparator.Text = info.PercentGroupSeparator;
        cbxPercentPositivePattern.SelectedIndex = info.PercentPositivePattern;
        cbxPercentNegativePattern.SelectedIndex = info.PercentNegativePattern;
        cbxPercentSymbol.Text = info.PercentSymbol;
      }
      Control[] controls = new Control[] { lblPercentDecimalDigits, udPercentDecimalDigits, 
        lblPercentDecimalSeparator, cbxPercentDecimalSeparator, 
        lblPercentGroupSeparator, cbxPercentGroupSeparator,
        lblPercentPositivePattern, cbxPercentPositivePattern, 
        lblPercentNegativePattern, cbxPercentNegativePattern,
        lblPercentSymbol, cbxPercentSymbol };
      foreach (Control c in controls)
      {
        c.Enabled = !cbPercentUseLocale.Checked;
      }
      Change();
    }

    private void udPercentDecimalDigits_ValueChanged(object sender, EventArgs e)
    {
      PercentFormat.DecimalDigits = (int)udPercentDecimalDigits.Value;
      Change();
    }

    private void cbxPercentDecimalSeparator_TextChanged(object sender, EventArgs e)
    {
      PercentFormat.DecimalSeparator = cbxPercentDecimalSeparator.Text;
      Change();
    }

    private void cbxPercentGroupSeparator_TextChanged(object sender, EventArgs e)
    {
      if (cbxPercentGroupSeparator.Text == Res.Get("Misc,None"))
        PercentFormat.GroupSeparator = "";
      else
        PercentFormat.GroupSeparator = cbxPercentGroupSeparator.Text;
      Change();
    }

    private void cbxPercentPositivePattern_SelectedValueChanged(object sender, EventArgs e)
    {
      PercentFormat.PositivePattern = cbxPercentPositivePattern.SelectedIndex;
      Change();
    }

    private void cbxPercentNegativePattern_SelectedValueChanged(object sender, EventArgs e)
    {
      PercentFormat.NegativePattern = cbxPercentNegativePattern.SelectedIndex;
      Change();
    }

    private void cbxPercentSymbol_TextChanged(object sender, EventArgs e)
    {
      PercentFormat.PercentSymbol = cbxPercentSymbol.Text;
      Change();
    }

    private void cbxBooleanFalse_TextChanged(object sender, EventArgs e)
    {
      BooleanFormat.FalseText = cbxBooleanFalse.Text;
      Change();
    }

    private void cbxBooleanTrue_TextChanged(object sender, EventArgs e)
    {
      BooleanFormat.TrueText = cbxBooleanTrue.Text;
      Change();
    }

    private void lbxCustom_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lbxCustom.SelectedIndex == -1)
        return;
      string format = (string)lbxCustom.SelectedItem;
      tbCustom.Text = format.Substring(format.IndexOf(';') + 1);
    }

    private void tbCustom_TextChanged(object sender, EventArgs e)
    {
      CustomFormat.Format = tbCustom.Text;
      Change();
    }

    private void cbxExpression_SelectedIndexChanged(object sender, EventArgs e)
    {
      FormatIndex = cbxExpression.SelectedIndex;
      pc1.Focus();
    }

    private void pc1_PageSelected(object sender, EventArgs e)
    {
      FSelectedFormatIndices[FFormatIndex] = pc1.ActivePageIndex;
      Change();
    }

    private void lbxDates_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      if (e.Index >= 0)
      {
        string format = (string)(sender as ListBox).Items[e.Index];
        string text = String.Format("{0:" + format + "}", new DateTime(2007, 11, 30, 13, 30, 0));
        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, e.ForeColor);
        TextRenderer.DrawText(e.Graphics, format, e.Font, e.Bounds, e.ForeColor, TextFormatFlags.Right);
      }
    }

    private void lbxCustom_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      if (e.Index >= 0)
      {
        string format = (string)lbxCustom.Items[e.Index];
        string text = format.Split(new char[] { ';' })[0];
        TextRenderer.DrawText(e.Graphics, text, e.Font, e.Bounds.Location, e.ForeColor);
      }
    }

    private void FormatEditorForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      lbxDates.Height = pnDate.Height - gbSample.Height - 32 - lbxDates.Top;
      lbxTimes.Height = pnDate.Height - gbSample.Height - 32 - lbxTimes.Top;
      lbxCustom.Height = pnDate.Height - gbSample.Height - 32 - lbxCustom.Top;
      lbxDates.ItemHeight = DrawUtils.DefaultItemHeight;
      lbxTimes.ItemHeight = DrawUtils.DefaultItemHeight;
      lbxCustom.ItemHeight = DrawUtils.DefaultItemHeight;
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,FormatEditor");
      Text = res.Get("");
      
      gbSample.Text = Res.Get("Misc,Sample");
      lblExpression.Text = res.Get("Expression");
      
      pnGeneral.Text = res.Get("General");
      pnNumber.Text = res.Get("Number");
      pnCurrency.Text = res.Get("Currency");
      pnDate.Text = res.Get("Date");
      pnTime.Text = res.Get("Time");
      pnPercent.Text = res.Get("Percent");
      pnBoolean.Text = res.Get("Boolean");
      pnCustom.Text = res.Get("Custom");
            
      cbNumberUseLocale.Text = res.Get("UseLocale");
      cbCurrencyUseLocale.Text = res.Get("UseLocale");
      cbPercentUseLocale.Text = res.Get("UseLocale");
      
      lblNumberDecimalDigits.Text = res.Get("DecimalDigits");
      lblNumberDecimalSeparator.Text = res.Get("DecimalSeparator");
      lblNumberGroupSeparator.Text = res.Get("GroupSeparator");
      lblNumberNegativePattern.Text = res.Get("NegativePattern");

      lblCurrencyDecimalDigits.Text = res.Get("DecimalDigits");
      lblCurrencyDecimalSeparator.Text = res.Get("DecimalSeparator");
      lblCurrencyGroupSeparator.Text = res.Get("GroupSeparator");
      lblCurrencyPositivePattern.Text = res.Get("PositivePattern");
      lblCurrencyNegativePattern.Text = res.Get("NegativePattern");
      lblCurrencySymbol.Text = res.Get("CurrencySymbol");

      lblPercentDecimalDigits.Text = res.Get("DecimalDigits");
      lblPercentDecimalSeparator.Text = res.Get("DecimalSeparator");
      lblPercentGroupSeparator.Text = res.Get("GroupSeparator");
      lblPercentPositivePattern.Text = res.Get("PositivePattern");
      lblPercentNegativePattern.Text = res.Get("NegativePattern");
      lblPercentSymbol.Text = res.Get("PercentSymbol");
      
      lblBooleanFalse.Text = res.Get("FalseText");
      lblBooleanTrue.Text = res.Get("TrueText");
      
      for (int i = 1; i < 20; i++)
      {
        if (!Res.StringExists("Formats,Date" + i.ToString()))
          break;
        lbxDates.Items.Add(Res.Get("Formats,Date" + i.ToString()));
      }
      for (int i = 1; i < 20; i++)
      {
        if (!Res.StringExists("Formats,Time" + i.ToString()))
          break;
        lbxTimes.Items.Add(Res.Get("Formats,Time" + i.ToString()));
      }
      for (int i = 1; i < 20; i++)
      {
        if (!Res.StringExists("Formats,BooleanF" + i.ToString()))
          break;
        cbxBooleanFalse.Items.Add(Res.Get("Formats,BooleanF" + i.ToString()));
        cbxBooleanTrue.Items.Add(Res.Get("Formats,BooleanT" + i.ToString()));
      }
      for (int i = 1; i < 20; i++)
      {
        if (!Res.StringExists("Formats,Custom" + i.ToString()))
          break;
        lbxCustom.Items.Add(Res.Get("Formats,Custom" + i.ToString()));
      }
      string[] decimalSeparators = new string[] { ".", ",", "-" };
      cbxCurrencyDecimalSeparator.Items.AddRange(decimalSeparators);
      cbxNumberDecimalSeparator.Items.AddRange(decimalSeparators);
      cbxPercentDecimalSeparator.Items.AddRange(decimalSeparators);
      
      string[] groupSeparators = new string[] { ",", " ", Res.Get("Misc,None") };
      cbxCurrencyGroupSeparator.Items.AddRange(groupSeparators);
      cbxNumberGroupSeparator.Items.AddRange(groupSeparators);
      cbxPercentGroupSeparator.Items.AddRange(groupSeparators);

      cbxCurrencyPositivePattern.Items.AddRange(new string[] { "$n", "n$", "$ n", "n $" });
      cbxCurrencyNegativePattern.Items.AddRange(new string[] { "($n)", "-$n", "$-n", "$n-", "(n$)", 
        "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)" });
      cbxNumberNegativePattern.Items.AddRange(new string[] { "(n)", "-n", "- n", "n-", "n -" });
      cbxPercentPositivePattern.Items.AddRange(new string[] { "n %", "n%", "%n", "% n" });
      cbxPercentNegativePattern.Items.AddRange(new string[] { "-n %", "-n%", "-%n", "%-n", "%n-", 
        "n-%", "n%-", "-%n", "n %-", "% n-", "% -n", "n- %" });

      cbxCurrencySymbol.Items.Add(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol);
      cbxCurrencySymbol.Items.Add("$");
      cbxCurrencySymbol.Items.Add("\u20AC");
      cbxCurrencySymbol.Items.Add("\u00A3");

      cbxPercentSymbol.Items.AddRange(new string[] { "%", "" });
    }
    
    public FormatEditorForm()
    {
      InitializeComponent();
      Localize();
    }
  }
}

