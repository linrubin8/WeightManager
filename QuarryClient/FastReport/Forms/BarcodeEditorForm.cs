using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Barcode.QRCode;
using System.Reflection;
using FastReport.Data;
using FastReport.Code;
using FastReport.Controls;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for barcode editor
    /// </summary>
    public partial class BarcodeEditorForm : Form
    {
        /// <summary>
        /// Generated text for barcode object
        /// </summary>
        public string Result;

        private Report report;
        private static List<string> expandedNodes;
        private string brackets;
        private TextBox prevFocus;        

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeEditorForm"/> class.
        /// </summary>
        /// <param name="data">Text data for parsing</param>
        /// <param name="report">Report object for nodes</param>
        /// <param name="Brackets">Brackets symbols</param>
        /// <param name="isRichBarcode">Editor for rich barcode?</param>
        public BarcodeEditorForm(string data, Report report, string Brackets, bool isRichBarcode)
        {
            InitializeComponent();
            Localize();
            this.FormClosed += QREdit_FormClosed;
            this.report = report;
            qrWifiEncryption.SelectedIndex = 0;
            qrTabs.Appearance = TabAppearance.FlatButtons;
            qrTabs.ItemSize = new Size(0, 1);
            qrTabs.SizeMode = TabSizeMode.Fixed;
            tvData.CreateNodes(report.Dictionary);
            brackets = Brackets;
            tvData.NodeMouseDoubleClick += tvData_NodeMouseDoubleClick;
            tvData.ItemDrag += tvData_ItemDrag;

            if (expandedNodes != null)
                tvData.ExpandedNodes = expandedNodes;

            foreach(Control control in GetAllControls(Controls))
            {
                TextBox tb = control as TextBox;

                if(tb != null)
                {
                    tb.GotFocus += prevFocus_GotFocus;
                    tb.DragOver += prevFocus_DragOver;
                    tb.DragDrop += prevFocus_DragDrop;
                }
            }

            if (isRichBarcode)
            {
                parse(data);
            }
            else
            {
                qrText.Text = data;
                qrSelectType.SelectedIndex = 0;

                qrSelectType.Visible = false;
                qrTabs.Location = new Point(4, 4);
                qrText.Height = panel1.Height;
                qrTabs.Height = qrText.Height + 50;
            }
        }

        private void Localize()
        {
            MyRes res = new MyRes("Forms,BarcodeEditor");
            Text = res.Get("");

            okButton.Text = res.Get("Save");
            cancelButton.Text = res.Get("Cancel");

            qrSelectType.Items[0] = res.Get("Text");
            qrSelectType.Items[1] = res.Get("vCard");
            qrSelectType.Items[2] = res.Get("URI");
            qrSelectType.Items[3] = res.Get("EmailAddress");
            qrSelectType.Items[4] = res.Get("EmailMessage");
            qrSelectType.Items[5] = res.Get("Geolocation");
            qrSelectType.Items[6] = res.Get("SMS");
            qrSelectType.Items[7] = res.Get("Call");
            qrSelectType.Items[8] = res.Get("Event");
            qrSelectType.Items[9] = res.Get("Wifi");

            label16.Text = res.Get("FirstName");
            label17.Text = res.Get("LastName");
            label18.Text = res.Get("Title");
            label20.Text = res.Get("Company");
            label21.Text = res.Get("Website");
            label25.Text = res.Get("EmailPersonal");
            label26.Text = res.Get("EmailBusiness");
            label19.Text = res.Get("PhoneMobile");
            label27.Text = res.Get("PhonePersonal");
            label28.Text = res.Get("PhoneBusiness");
            label31.Text = res.Get("Street");
            label22.Text = res.Get("ZipCode");
            label23.Text = res.Get("City");
            label24.Text = res.Get("Country");
            label30.Text = res.Get("URI");
            label29.Text = res.Get("Email");
            label1.Text = res.Get("Email");
            label2.Text = res.Get("Subject");
            label3.Text = res.Get("Text");
            label5.Text = res.Get("Latitude");
            label4.Text = res.Get("Longitude");
            label6.Text = res.Get("Height");
            label7.Text = res.Get("PhoneNumber");
            label8.Text = res.Get("Text");
            label9.Text = res.Get("PhoneNumber");
            label10.Text = res.Get("Description");
            label11.Text = res.Get("From");
            label12.Text = res.Get("To");
            label13.Text = res.Get("Encryption");
            label14.Text = res.Get("NetworkName");
            label15.Text = res.Get("Password");
            qrWifiHidden.Text = res.Get("WifiHidden");
        }

        private void QREdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            expandedNodes = tvData.ExpandedNodes;
        }

        private void tvData_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tvData.SelectedNode = e.Item as TreeNode;
            if (tvData.SelectedItem != "")
                tvData.DoDragDrop(e.Item, DragDropEffects.Move);
            else
                tvData.DoDragDrop(e.Item, DragDropEffects.None);
        }

        private void prevFocus_DragOver(object sender, DragEventArgs e)
        {
            TextBox tb = sender as TextBox;

            int index = tb.GetCharIndexFromPosition(tb.PointToClient(new Point(e.X, e.Y)));
            if (index == tb.Text.Length - 1)
                index++;
            tb.Focus();
            tb.Select(index, 0);
            e.Effect = e.AllowedEffect;
        }

        private void prevFocus_DragDrop(object sender, DragEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.SelectedText = GetTextWithBrackets();
            tb.Focus();
        }

        List<Control> GetAllControls(Control.ControlCollection collection)
        {
            List<Control> result = new List<Control>();

            foreach(Control control in collection)
            {
                result.Add(control);
                result.AddRange(GetAllControls(control.Controls));
            }

            return result;
        }

        private void prevFocus_GotFocus(object sender, EventArgs e)
        {
            prevFocus = sender as TextBox;
        }

        private void tvData_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tvData.SelectedItem != "" && prevFocus != null)
            {
                prevFocus.SelectedText = GetTextWithBrackets();
                prevFocus.Focus();
            }
        }

        private string GetTextWithBrackets()
        {
            string text = tvData.SelectedItem;
            string[] _brackets = brackets.Split(new char[] { ',' });
            // this check is needed if Brackets property is not "[,]"
            if (InsideBrackets(prevFocus.SelectionStart))
            {
                if (tvData.SelectedItemType == DataTreeSelectedItemType.Function ||
                  tvData.SelectedItemType == DataTreeSelectedItemType.DialogControl)
                    return text;
                return "[" + text + "]";
            }
            return _brackets[0] + text + _brackets[1];
        }

        private bool InsideBrackets(int pos)
        {
            string[] _brackets = brackets.Split(new char[] { ',' });
            FindTextArgs args = new FindTextArgs();
            args.Text = new FastString(prevFocus.Text);
            args.OpenBracket = _brackets[0];
            args.CloseBracket = _brackets[1];
            args.StartIndex = pos;
            return CodeUtils.IndexInsideBrackets(args);
        }

        private void parse(string data)
        {
            QRData qr = null;

            try
            {
                qr = QRData.Parse(data);
            }
            catch
            {
                try
                {
                    qr = new QRText(data);
                }
                catch
                {
                    MessageBox.Show("Can't parse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
            }

            if (qr is QRText)
            {
                qrText.Text = qr.Data;
                qrSelectType.SelectedIndex = 0;
            }
            else if (qr is QRvCard)
            {
                QRvCard _qr = qr as QRvCard;
                qrVcFN.Text = _qr.FirstName;
                qrVcLN.Text = _qr.LastName;
                qrVcTitle.Text = _qr.TITLE;
                qrVcCompany.Text = _qr.ORG;
                qrVcWebsite.Text = _qr.URL;
                qrVcEmailHome.Text = _qr.EMAIL_HOME_INTERNET;
                qrVcEmailWork.Text = _qr.EMAIL_WORK_INTERNET;
                qrVcPhone.Text = _qr.TEL_CELL;
                qrVcPhoneHome.Text = _qr.TEL_HOME_VOICE;
                qrVcPhoneWork.Text = _qr.TEL_WORK_VOICE;
                qrVcStreet.Text = _qr.Street;
                qrVcZip.Text = _qr.ZipCode;
                qrVcCity.Text = _qr.City;
                qrVcCountry.Text = _qr.Country;
                qrSelectType.SelectedIndex = 1;
            }
            else if (qr is QRURI)
            {
                qrURI.Text = qr.Data;
                qrSelectType.SelectedIndex = 2;
            }
            else if (qr is QREmailAddress)
            {
                qrEmail.Text = qr.Data;
                qrSelectType.SelectedIndex = 3;
            }
            else if (qr is QREmailMessage)
            {
                QREmailMessage _qr = qr as QREmailMessage;
                qrEmailTo.Text = _qr.TO;
                qrEmailSub.Text = _qr.SUB;
                qrEmailText.Text = _qr.BODY;
                qrSelectType.SelectedIndex = 4;
            }
            else if (qr is QRGeo)
            {
                QRGeo _qr = qr as QRGeo;
                qrGeoLatitude.Text = _qr.Latitude;
                qrGeoLongitude.Text = _qr.Longitude;
                qrGeoMeters.Text = _qr.Meters;
                qrSelectType.SelectedIndex = 5;
            }
            else if (qr is QRSMS)
            {
                QRSMS _qr = qr as QRSMS;
                qrSMSTo.Text = _qr.TO;
                qrSMSText.Text = _qr.TEXT;
                qrSelectType.SelectedIndex = 6;
            }
            else if (qr is QRCall)
            {
                QRCall _qr = qr as QRCall;
                qrCall.Text = _qr.Tel;
                qrSelectType.SelectedIndex = 7;
            }
            else if (qr is QREvent)
            {
                QREvent _qr = qr as QREvent;
                qrEventDesc.Text = _qr.SUMMARY;
                qrEventFrom.Value = _qr.DTSTART;
                qrEventTo.Value = _qr.DTEND;
                qrSelectType.SelectedIndex = 8;
            }
            else if (qr is QRWifi)
            {
                QRWifi _qr = qr as QRWifi;
                qrWifiEncryption.Text = _qr.Encryption;
                qrWifiName.Text = _qr.NetworkName;
                qrWifiPass.Text = _qr.Password;
                qrWifiHidden.Checked = _qr.Hidden;
                qrSelectType.SelectedIndex = 9;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (qrTabs.SelectedTab == qrTypeText)
            {
                QRText qr = new QRText();
                qr.Data = qrText.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeVcard)
            {
                QRvCard qr = new QRvCard();
                qr.FirstName = qrVcFN.Text;
                qr.LastName = qrVcLN.Text;
                qr.TITLE = qrVcTitle.Text;
                qr.ORG = qrVcCompany.Text;
                qr.URL = qrVcWebsite.Text;
                qr.EMAIL_HOME_INTERNET = qrVcEmailHome.Text;
                qr.EMAIL_WORK_INTERNET = qrVcEmailWork.Text;
                qr.TEL_CELL = qrVcPhone.Text;
                qr.TEL_HOME_VOICE = qrVcPhoneHome.Text;
                qr.TEL_WORK_VOICE = qrVcPhoneWork.Text;
                qr.Street = qrVcStreet.Text;
                qr.ZipCode = qrVcZip.Text;
                qr.City = qrVcCity.Text;
                qr.Country = qrVcCountry.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeURI)
            {
                QRURI qr = new QRURI();
                qr.Data = qrURI.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeEmailAddress)
            {
                QREmailAddress qr = new QREmailAddress();
                qr.Data = qrEmail.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeEmailMessage)
            {
                QREmailMessage qr = new QREmailMessage();
                qr.TO = qrEmailTo.Text;
                qr.SUB = qrEmailSub.Text;
                qr.BODY = qrEmailText.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeGeo)
            {
                QRGeo qr = new QRGeo();
                qr.Latitude = qrGeoLatitude.Text;
                qr.Longitude = qrGeoLongitude.Text;
                qr.Meters = qrGeoMeters.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeSMS)
            {
                QRSMS qr = new QRSMS();
                qr.TO = qrSMSTo.Text;
                qr.TEXT = qrSMSText.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeCall)
            {
                QRCall qr = new QRCall();
                qr.Tel = qrCall.Text;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeEvent)
            {
                QREvent qr = new QREvent();
                qr.SUMMARY = qrEventDesc.Text;
                qr.DTSTART = qrEventFrom.Value;
                qr.DTEND = qrEventTo.Value;
                Result = qr.Pack();
            }
            else if (qrTabs.SelectedTab == qrTypeWifi)
            {
                QRWifi qr = new QRWifi();
                qr.Encryption = qrWifiEncryption.Text;
                qr.NetworkName = qrWifiName.Text;
                qr.Password = qrWifiPass.Text;
                qr.Hidden = qrWifiHidden.Checked;
                Result = qr.Pack();
            }
        }

        private void qrSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            qrTabs.SelectedIndex = (sender as ComboBox).SelectedIndex;
        }

        private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool descrVisible = tvData.SelectedNode != null &&
                (tvData.SelectedNode.Tag is MethodInfo || tvData.SelectedNode.Tag is SystemVariable);
            expandableSplitter1.Visible = descrVisible;
            lblDescription.Visible = descrVisible;

            if (descrVisible)
                lblDescription.ShowDescription(report, tvData.SelectedNode.Tag);
        }
    }
}
