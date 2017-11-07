using System;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using FastReport.Barcode;

namespace FastReport.TypeEditors
{
    internal class BarcodeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;

        private void lb_Click(object sender, EventArgs e)
        {
            edSvc.CloseDropDown();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            string name = (Value as BarcodeBase).Name;

            ListBox listBox = new ListBox();
            listBox.Items.AddRange(Barcodes.GetDisplayNames());
            listBox.SelectedItem = name;
            listBox.BorderStyle = BorderStyle.None;
            listBox.Height = listBox.ItemHeight * listBox.Items.Count;
            listBox.Click += new EventHandler(lb_Click);
            edSvc.DropDownControl(listBox);

            if (listBox.SelectedIndex == -1)
                return Value;

            string[] symbologyNames = Barcodes.GetSymbologyNames();
            string selectedName = symbologyNames[listBox.SelectedIndex];
            if (selectedName != name)
                return Activator.CreateInstance(Barcodes.GetType(selectedName));

            return Value;
        }
    }
}
