using LB.Controls.Args;
using LB.Controls.Report;
using LB.Page.Helper;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBUIPageBase : UserControl
    {
        public event FormClosingEventHandler FormClosing;
        public event FormClosedEventHandler FormClosed;
        private TabPage _RefTabPage;
        public Dictionary<string, object> PageParms;
        private ToolStripReportArgs mToolStripReportArgs;
        private ToolStrip mToolStrip = null;
        /// <summary>
        /// ShowMainPage情况下，记录该页面所属的TabPage页签
        /// </summary>
        public TabPage RefTabPage
        {
            get
            {
                return _RefTabPage;
            }
            set
            {
                _RefTabPage = value;
            }
        }

        private bool _PageAutoSize = false;
        [Description("页签是否自定义大小")]
        public bool PageAutoSize
        {
            get
            {
                return _PageAutoSize;
            }
            set
            {
                _PageAutoSize = value;
            }
        }


        private string _LBPageTitle = "";
        [Description("页面标题")]//
        public string LBPageTitle
        {
            get
            {
                return _LBPageTitle;
            }
            set
            {
                _LBPageTitle = value;
            }
        }

        public LBUIPageBase()
        {
            InitializeComponent();
        }

        public LBUIPageBase(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mToolStripReportArgs = new ToolStripReportArgs();
            OnInitToolStripControl(mToolStripReportArgs);
        }

        /// <summary>
        /// 关闭页面
        /// </summary>
        public void Close()
        {
            if (FormClosing != null)
            {
                FormClosingEventArgs closingArgs = new FormClosingEventArgs(CloseReason.None, false);
                FormClosing(this, closingArgs);
                if (closingArgs.Cancel)
                {
                    return;
                }
            }
            if (FormClosed != null)
            {
                FormClosedEventArgs closedArgs = new FormClosedEventArgs(CloseReason.None);
                FormClosed(this, closedArgs);
            }

        }

        /// <summary>
        /// LBForm关闭时调用
        /// </summary>
        /// <param name="bolCancel"></param>
        public void StartClose(out bool bolCancel)
        {
            bolCancel = false;
            if (FormClosing != null)
            {
                FormClosingEventArgs closingArgs = new FormClosingEventArgs(CloseReason.None, false);
                FormClosing(this, closingArgs);
                bolCancel = closingArgs.Cancel;
            }
        }

        #region--校验控件是否为空 --
        /// <summary>
        /// 校验控件是否为空
        /// </summary>
        public void VerifyTextBoxIsEmpty()
        {
            List<ILBTextBox> lstControl = new List<ILBTextBox>();
            CollectionAllVisibleTextBox(this.Controls, ref lstControl);

            StringBuilder strMsg = new StringBuilder();
            foreach(ILBTextBox txtBox in lstControl)
            {
                strMsg.AppendLine("控件<"+txtBox.Caption+">值不能为空！");
            }
            
            if (strMsg.ToString() != "")
                throw new Exception(strMsg.ToString());
        }

        private void CollectionAllVisibleTextBox(ControlCollection controls, ref List<ILBTextBox> lstControl)
        {
            foreach(Control ctl in controls)
            {
                if(ctl.Visible && ctl is ILBTextBox)
                {
                    ILBTextBox txtBox = ctl as ILBTextBox;
                    if (!txtBox.CanBeEmpty)
                    {
                        if (txtBox.IsEmptyValue)
                        {
                            lstControl.Add(txtBox);
                        }
                    }
                }
                else
                {
                    CollectionAllVisibleTextBox(ctl.Controls, ref lstControl);
                }
            }
        }
        #endregion

        protected virtual void OnInitToolStripControl(ToolStripReportArgs args)
        {
            mToolStrip = args.LBToolStrip;
            if (args.LBToolStrip!=null && args.ReportTypeID > 0)
            {
                if (!args.LBToolStrip.Items.ContainsKey("btnReportEdit"))
                {
                    LBToolStripReportViewButton btnEditReport = new LB.Controls.LBToolStripReportViewButton();
                    btnEditReport.Image = Properties.Resources.btnConfig;
                    btnEditReport.ImageTransparentColor = System.Drawing.Color.Magenta;
                    btnEditReport.LBPermissionCode = "";
                    btnEditReport.Name = "btnReportEdit";
                    btnEditReport.ReportTypeID = args.ReportTypeID;
                    btnEditReport.Size = new System.Drawing.Size(60, 37);
                    btnEditReport.Text = "报表设计";
                    btnEditReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                    btnEditReport.ToolStripType = LB.Controls.Helper.enToolStripType.EditReportButton;
                    btnEditReport.Click += BtnEditReport_Click;
                    args.LBToolStrip.Items.Add(btnEditReport);
                }
                DataTable dtReportTemp = ReportHelper.GetReportTemplateRowByType(args.ReportTypeID);
                if (args.LBToolStrip.Items.ContainsKey("btnReportViewSingle"))
                {
                    args.LBToolStrip.Items.RemoveByKey("btnReportViewSingle");
                }
                if (args.LBToolStrip.Items.ContainsKey("btnReportViewMulit"))
                {
                    args.LBToolStrip.Items.RemoveByKey("btnReportViewMulit");
                }
                if (args.LBToolStrip.Items.ContainsKey("btnReportPrintSingle"))
                {
                    args.LBToolStrip.Items.RemoveByKey("btnReportPrintSingle");
                }
                if (args.LBToolStrip.Items.ContainsKey("btnReportPrintMutli"))
                {
                    args.LBToolStrip.Items.RemoveByKey("btnReportPrintMutli");
                }
                if (dtReportTemp.Rows.Count == 1)
                {
                    if (!args.LBToolStrip.Items.ContainsKey("btnReportViewSingle"))
                    {
                        string strReportTemplateID = dtReportTemp.Rows[0]["ReportTemplateID"].ToString();
                        LBToolStripReportViewButton btnReportViewSingle = new LB.Controls.LBToolStripReportViewButton();
                        btnReportViewSingle.Image = Properties.Resources.btnPreview;
                        btnReportViewSingle.ImageTransparentColor = System.Drawing.Color.Magenta;
                        btnReportViewSingle.LBPermissionCode = "";
                        btnReportViewSingle.Name = "btnReportViewSingle";
                        btnReportViewSingle.ReportTypeID = args.ReportTypeID;
                        btnReportViewSingle.Tag = dtReportTemp.Rows[0];
                        btnReportViewSingle.Size = new System.Drawing.Size(60, 37);
                        btnReportViewSingle.Text = "报表预览";
                        btnReportViewSingle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                        btnReportViewSingle.ToolStripType = LB.Controls.Helper.enToolStripType.EditReportButton;
                        btnReportViewSingle.Click += BtnReportViewSingle_Click;
                        args.LBToolStrip.Items.Add(btnReportViewSingle);
                    }

                    if (!args.LBToolStrip.Items.ContainsKey("btnReportPrintSingle"))
                    {
                        string strReportTemplateID = dtReportTemp.Rows[0]["ReportTemplateID"].ToString();
                        LBToolStripReportViewButton btnReportPrintSingle = new LB.Controls.LBToolStripReportViewButton();
                        btnReportPrintSingle.Image = Properties.Resources.btnPrint;
                        btnReportPrintSingle.ImageTransparentColor = System.Drawing.Color.Magenta;
                        btnReportPrintSingle.LBPermissionCode = "";
                        btnReportPrintSingle.Name = "btnReportPrintSingle";
                        btnReportPrintSingle.ReportTypeID = args.ReportTypeID;
                        btnReportPrintSingle.Tag = dtReportTemp.Rows[0];
                        btnReportPrintSingle.Size = new System.Drawing.Size(60, 37);
                        btnReportPrintSingle.Text = "报表打印";
                        btnReportPrintSingle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                        btnReportPrintSingle.ToolStripType = LB.Controls.Helper.enToolStripType.EditReportButton;
                        btnReportPrintSingle.Click += BtnReportPrintSingle_Click; ;
                        args.LBToolStrip.Items.Add(btnReportPrintSingle);
                    }
                }
                if (dtReportTemp.Rows.Count > 1)
                {
                    //添加预览按钮
                    if (!args.LBToolStrip.Items.ContainsKey("btnReportViewMutli"))
                    {
                        LBToolStripDropDownReportViewButton btnReportViewSingle = new LB.Controls.LBToolStripDropDownReportViewButton();
                        btnReportViewSingle.Image = Properties.Resources.btnPreview;
                        btnReportViewSingle.ImageTransparentColor = System.Drawing.Color.Magenta;
                        btnReportViewSingle.LBPermissionCode = "";
                        btnReportViewSingle.Name = "btnReportViewMutli";
                        btnReportViewSingle.ReportTypeID = args.ReportTypeID;
                        btnReportViewSingle.Size = new System.Drawing.Size(60, 37);
                        btnReportViewSingle.Text = "报表预览";
                        btnReportViewSingle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                        btnReportViewSingle.Click += BtnReportViewSingle_Click;
                        args.LBToolStrip.Items.Add(btnReportViewSingle);

                        foreach(DataRow drReport in dtReportTemp.Rows)
                        {
                            string strReportTemplateID = drReport["ReportTemplateID"].ToString();
                            string strReportTemplateName = drReport["ReportTemplateName"].ToString().TrimEnd();
                            System.Windows.Forms.ToolStripMenuItem toolstripItem = new ToolStripMenuItem();
                            toolstripItem.Tag = drReport;
                            toolstripItem.Name = "btnReportMunuItem"+ strReportTemplateID;
                            toolstripItem.Text = strReportTemplateName;
                            toolstripItem.Click += BtnReportView_Click;
                            btnReportViewSingle.DropDownItems.Add(toolstripItem);
                        }
                    }
                    //添加直接打印按钮
                    if (!args.LBToolStrip.Items.ContainsKey("btnReportPrintMutli"))
                    {
                        LBToolStripDropDownReportViewButton btnReportPrintMutli = new LB.Controls.LBToolStripDropDownReportViewButton();
                        btnReportPrintMutli.Image = Properties.Resources.btnPrint;
                        btnReportPrintMutli.ImageTransparentColor = System.Drawing.Color.Magenta;
                        btnReportPrintMutli.LBPermissionCode = "";
                        btnReportPrintMutli.Name = "btnReportPrintMutli";
                        btnReportPrintMutli.ReportTypeID = args.ReportTypeID;
                        btnReportPrintMutli.Size = new System.Drawing.Size(60, 37);
                        btnReportPrintMutli.Text = "报表预览";
                        btnReportPrintMutli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                        //btnReportPrintMutli.Click += BtnReportViewSingle_Click;
                        args.LBToolStrip.Items.Add(btnReportPrintMutli);

                        foreach (DataRow drReport in dtReportTemp.Rows)
                        {
                            string strReportTemplateID = drReport["ReportTemplateID"].ToString();
                            string strReportTemplateName = drReport["ReportTemplateName"].ToString().TrimEnd();
                            System.Windows.Forms.ToolStripMenuItem toolstripItem = new ToolStripMenuItem();
                            toolstripItem.Tag = drReport;
                            toolstripItem.Name = "btnReportPrintMunuItem" + strReportTemplateID;
                            toolstripItem.Text = strReportTemplateName;
                            toolstripItem.Click += BtnReportPrintSingle_Click;
                            btnReportPrintMutli.DropDownItems.Add(toolstripItem);
                        }
                    }
                }
            }
        }

        //直接打印
        private void BtnReportPrintSingle_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = null;
                long lReportTemplateID = 0;
                long lReportTypeID = 0;
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    System.Windows.Forms.ToolStripMenuItem toolstripItem = sender as System.Windows.Forms.ToolStripMenuItem;
                    dr = toolstripItem.Tag as DataRow;
                    lReportTemplateID = Convert.ToInt64(dr["ReportTemplateID"]);
                    lReportTypeID = Convert.ToInt64(dr["ReportTypeID"]);
                }
                else if (sender is LBToolStripReportViewButton)
                {
                    LBToolStripReportViewButton btnViewReport = sender as LBToolStripReportViewButton;
                    dr = btnViewReport.Tag as DataRow;
                    lReportTemplateID = Convert.ToInt64(dr["ReportTemplateID"]);
                    lReportTypeID = Convert.ToInt64(dr["ReportTypeID"]);
                }

                ReportRequestArgs args = new Report.ReportRequestArgs(lReportTemplateID, lReportTypeID, null, null);
                OnReportRequest(args);
                if (args.DSDataSource == null && args.RecordDR == null)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("未设置数据源，编辑报表失败！");
                }
                else
                {
                    ReportHelper.OpenReportDialog(enRequestReportActionType.DirectPrint,args);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void BtnReportView_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = null;
                long lReportTemplateID = 0;
                long lReportTypeID = 0;
                if (sender is System.Windows.Forms.ToolStripMenuItem)
                {
                    System.Windows.Forms.ToolStripMenuItem toolstripItem = sender as System.Windows.Forms.ToolStripMenuItem;
                    dr = toolstripItem.Tag as DataRow;
                    lReportTemplateID = Convert.ToInt64(dr["ReportTemplateID"]);
                    lReportTypeID = Convert.ToInt64(dr["ReportTypeID"]);
                }
                else if (sender is LBToolStripReportViewButton)
                {
                    LBToolStripReportViewButton btnViewReport = sender as LBToolStripReportViewButton;
                    dr = btnViewReport.Tag as DataRow;
                    lReportTemplateID = Convert.ToInt64(dr["ReportTemplateID"]);
                    lReportTypeID = Convert.ToInt64(dr["ReportTypeID"]);
                }

                ReportRequestArgs args = new Report.ReportRequestArgs(lReportTemplateID, lReportTypeID, null, null);
                OnReportRequest(args);
                if (args.DSDataSource == null && args.RecordDR == null)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("未设置数据源，编辑报表失败！");
                }
                else
                {
                    ReportHelper.OpenReportDialog(enRequestReportActionType.Preview,args);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void BtnReportViewSingle_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = null;
                long lReportTemplateID = 0;
                long lReportTypeID = 0;
                if (sender is LBToolStripReportViewButton)
                {
                    LBToolStripReportViewButton btnViewReport = sender as LBToolStripReportViewButton;
                    dr = btnViewReport.Tag as DataRow;
                    lReportTemplateID = Convert.ToInt64(dr["ReportTemplateID"]);
                    lReportTypeID = Convert.ToInt64(dr["ReportTypeID"]);
                }

                ReportRequestArgs args = new Report.ReportRequestArgs(lReportTemplateID, lReportTypeID, null, null);
                OnReportRequest(args);
                if (args.DSDataSource == null && args.RecordDR == null)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("未设置数据源，编辑报表失败！");
                }
                else
                {
                    ReportHelper.OpenReportDialog(enRequestReportActionType.Preview, args);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        protected virtual void OnReportRequest(ReportRequestArgs args)
        {

        }

        private void BtnEditReport_Click(object sender, EventArgs e)
        {
            try
            {
                LBToolStripReportViewButton btnEditReport = sender as LBToolStripReportViewButton;
                ReportRequestArgs args = new Report.ReportRequestArgs(0,(int)btnEditReport.ReportTypeID, null, null);
                OnReportRequest(args);
                if (args.DSDataSource == null && args.RecordDR == null)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("未设置数据源，编辑报表失败！");
                }
                else
                {
                    frmReport frm = new frmReport(args);
                    LBShowForm.ShowDialog(frm);

                    if (mToolStrip != null)
                    {
                        if (mToolStrip.Items.ContainsKey("btnReportEdit"))
                        {
                            mToolStrip.Items.RemoveByKey("btnReportEdit");
                        }
                        if (mToolStrip.Items.ContainsKey("btnReportViewSingle"))
                        {
                            mToolStrip.Items.RemoveByKey("btnReportViewSingle");
                        }
                        if (mToolStrip.Items.ContainsKey("btnReportViewMutli"))
                        {
                            mToolStrip.Items.RemoveByKey("btnReportViewMutli");
                        }
                        this.OnInitToolStripControl(mToolStripReportArgs);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
