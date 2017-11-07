using FastReport.Design;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Forms
{
    internal partial class SavingPageOptions : DesignerOptionsPage
    {
        private Designer designer;

        public SavingPageOptions(Designer designer) : base()
        {
            InitializeComponent();
            Localize();

            this.designer = designer;
            nudMinutes.Left = cbAutoSave.Right;
        }

        private void Localize()
        {
            MyRes res = new MyRes("Forms,SavingPageOptions");

            tab1.Text = res.Get("");
            cbAutoSave.Text = res.Get("EnableAutoSave");
        }

        public override void Init()
        {
            //cbAutoSave.Checked = designer.AutoSaveTimer.Enabled;
            //nudMinutes.Value = designer.AutoSaveTimer.Interval / 60000;

            XmlItem xi = Config.Root.FindItem("Designer").FindItem("Saving");
            int minutes = 0;
            if (!int.TryParse(xi.GetProp("AutoSaveMinutes"), out minutes))
                minutes = 5;
            nudMinutes.Value = minutes;
            cbAutoSave.Checked = xi.GetProp("EnableAutoSave") != "0";
            cbAutoSave.Enabled = !designer.IsPreviewPageDesigner;
            nudMinutes.Enabled = !designer.IsPreviewPageDesigner;
        }

        public override void Done(DialogResult result)
        {
            if (result == DialogResult.OK)
            {
                designer.AutoSaveTimer.Enabled = cbAutoSave.Checked;
                designer.AutoSaveTimer.Interval = (int)nudMinutes.Value * 60000;

                XmlItem xi = Config.Root.FindItem("Designer").FindItem("Saving");
                xi.SetProp("EnableAutoSave", cbAutoSave.Checked ? "1" : "0");
                xi.SetProp("AutoSaveMinutes", "" + nudMinutes.Value);
            }
        }
    }
}
