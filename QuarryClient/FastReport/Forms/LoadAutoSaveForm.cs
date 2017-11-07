using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Forms
{
    internal partial class LoadAutoSaveForm : Form
    {
        public LoadAutoSaveForm(string filename)
        {
            InitializeComponent();

            MyRes res = new MyRes("Forms,LoadAutoSave");
            Text = res.Get("");
            btnRestore.Text = res.Get("Restore");
            btnCancel.Text = Res.Get("Buttons,Cancel");
            lblMessage.Text = res.Get("Message") + (string.IsNullOrEmpty(filename) ? "" : "\n\n\"" + filename + "\"");
            picture.Image = ResourceLoader.GetBitmap("autosave.png");
        }
    }
}
