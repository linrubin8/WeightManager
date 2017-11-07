using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using LB.MI;
using LB.Page.Helper;
using LB.Common;
using LB.Controls.Report;
using LB.Controls.LBTextBox;

namespace LB.SysConfig.SysConfig
{
    public partial class frmReportView : LBUIPageBase
    {
        public frmReportView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BuildTree();

            this.tvReportType.AfterSelect += TvReportType_AfterSelect;
        }

        private void InitReport()
        {
            
        }

        private void TvReportType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (tvReportType.SelectedNode == null || tvReportType.SelectedNode.Tag == null)
                {
                    return;
                }
                this.pnlField.Controls.Clear();

                DataRow dr = tvReportType.SelectedNode.Tag as DataRow;
                long lReportTemplateID = dr["ReportTemplateID"] == DBNull.Value ?
                    0 : Convert.ToInt64(dr["ReportTemplateID"]);
                long lReportViewID = dr["ReportViewID"] == DBNull.Value ?
                    0 : Convert.ToInt64(dr["ReportViewID"]);

                DataTable dtView = ExecuteSQL.CallView(133, "", "ReportViewID=" + lReportViewID, "");
                foreach (DataRow drField in dtView.Rows)
                {
                    string strFieldName = drField["FieldName"].ToString().TrimEnd();
                    string strFieldText = drField["FieldText"].ToString().TrimEnd();
                    string strFieldFormat = drField["FieldFormat"].ToString().TrimEnd();
                    int FieldType = drField["FieldType"] == DBNull.Value ?
                        0 : Convert.ToInt32(drField["FieldType"]);

                    if (FieldType == 0 || FieldType == 1)//字符串或者数字
                    {
                        Controls.LBTextBox.CoolTextBox txtField = new LB.Controls.LBTextBox.CoolTextBox();
                        txtField.BackColor = System.Drawing.Color.Transparent;
                        txtField.BorderColor = System.Drawing.Color.LightSteelBlue;
                        txtField.CanBeEmpty = false;
                        txtField.Caption = strFieldText;
                        txtField.Font = new System.Drawing.Font("微软雅黑", 9.75F);
                        txtField.LBTitle = strFieldText;
                        txtField.LBTitleVisible = true;
                        txtField.Location = new System.Drawing.Point(346, 67);
                        txtField.Margin = new System.Windows.Forms.Padding(0);
                        txtField.Name = strFieldName;
                        txtField.PopupWidth = 120;
                        txtField.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
                        txtField.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
                        txtField.Size = new System.Drawing.Size(201, 28);
                        txtField.Width = this.pnlField.Width * 3 / 7;
                        txtField.TextBox.IsAllowNotExists = true;
                        this.pnlField.Controls.Add(txtField);
                    }
                    else if (FieldType == 2)//日期
                    {
                        LBDateTime txtBillDate = new LBDateTime();
                        if (strFieldFormat == "")
                        {
                            txtBillDate.TextBox.CustomFormat = "yyyy-MM-dd";
                            txtBillDate.TextBox.Format = DateTimePickerFormat.Short;
                        }
                        else
                        {
                            txtBillDate.TextBox.Format = DateTimePickerFormat.Custom;
                            txtBillDate.TextBox.CustomFormat = strFieldFormat;
                        }
                        txtBillDate.LBTitle = strFieldText;
                        txtBillDate.LBTitleVisible = true;
                        txtBillDate.Location = new System.Drawing.Point(107, 195);
                        txtBillDate.MinimumSize = new System.Drawing.Size(0, 30);
                        txtBillDate.Name = strFieldName;
                        txtBillDate.Size = new System.Drawing.Size(135, 30);
                        txtBillDate.Width = this.pnlField.Width * 3 / 7;
                        this.pnlField.Controls.Add(txtBillDate);
                    }
                    if (FieldType == 3 || FieldType == 4|| FieldType == 5)//关联客户
                    {
                        LB.Controls.LBTextBox.CoolTextBox txtField = new LB.Controls.LBTextBox.CoolTextBox();
                        txtField.BackColor = System.Drawing.Color.Transparent;
                        txtField.BorderColor = System.Drawing.Color.LightSteelBlue;
                        txtField.CanBeEmpty = false;
                        txtField.Caption = strFieldText;
                        txtField.Font = new System.Drawing.Font("微软雅黑", 9.75F);
                        txtField.LBTitle = strFieldText;
                        txtField.LBTitleVisible = true;
                        txtField.Location = new System.Drawing.Point(346, 67);
                        txtField.Margin = new System.Windows.Forms.Padding(0);
                        txtField.Name = strFieldName;
                        txtField.PopupWidth = 120;
                        txtField.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
                        txtField.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
                        txtField.Size = new System.Drawing.Size(201, 28);
                        txtField.Width = this.pnlField.Width * 3 / 7;
                        txtField.TextBox.IsAllowNotExists = true;

                        if (FieldType == 3)
                        {
                            txtField.TextBox.LBViewType = 110;
                            txtField.TextBox.LBSort = "CustomerName asc";
                            txtField.TextBox.IDColumnName = "CustomerID";
                            txtField.TextBox.TextColumnName = "CustomerName";
                        }
                        else if (FieldType == 4)
                        {
                            txtField.TextBox.LBViewType = 203;
                            txtField.TextBox.LBSort = "ItemName asc";
                            txtField.TextBox.IDColumnName = "ItemID";
                            txtField.TextBox.TextColumnName = "ItemName";
                        }
                        else if (FieldType == 5)
                        {
                            txtField.TextBox.LBViewType = 113;
                            txtField.TextBox.LBSort = "CarNum asc";
                            txtField.TextBox.IDColumnName = "CarID";
                            txtField.TextBox.TextColumnName = "CarNum";
                        }
                        this.pnlField.Controls.Add(txtField);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddReportView_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddReportView frmView = new MI.frmAddReportView(0);
                LBShowForm.ShowDialog(frmView);
                BuildTree();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeleteItemType_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvReportType.SelectedNode == null || this.tvReportType.SelectedNode.Tag == null)
                {
                    return;
                }
                DataRow dr = this.tvReportType.SelectedNode.Tag as DataRow;
                long lItemTypeID = LBConverter.ToInt64(dr["ReportViewID"]);
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除改决策分析报表？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (lItemTypeID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReportViewID", enLBDbType.Int64, lItemTypeID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14402, parmCol, out dsReturn, out dictValue);
                    }
                    BuildTree();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditItemType_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvReportType.SelectedNode == null || this.tvReportType.SelectedNode.Tag == null)
                {
                    return;
                }
                DataRow dr = this.tvReportType.SelectedNode.Tag as DataRow;
                frmAddReportView frm = new frmAddReportView(LBConverter.ToInt64(dr["ReportViewID"]));
                LBShowForm.ShowDialog(frm);
                BuildTree();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void BuildTree()
        {
            this.tvReportType.Nodes.Clear();
            DataTable dt = ExecuteSQL.CallView(132);
            TreeNode tnTop = new TreeNode("决策分析报表");
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode(dr["ReportViewName"].ToString().TrimEnd());
                tn.Tag = dr;
                tnTop.Nodes.Add(tn);
            }
            tvReportType.Nodes.Add(tnTop);
            tnTop.ExpandAll();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvReportType.SelectedNode == null || tvReportType.SelectedNode.Tag == null)
                {
                    return;
                }
                DataRow dr = tvReportType.SelectedNode.Tag as DataRow;
                long lReportTemplateID = dr["ReportTemplateID"] == DBNull.Value ?
                    0 : Convert.ToInt64(dr["ReportTemplateID"]);
                long lReportViewID = dr["ReportViewID"] == DBNull.Value ?
                    0 : Convert.ToInt64(dr["ReportViewID"]);

                if (lReportTemplateID == 0 || lReportViewID == 0)
                    return;

                DataTable dtField = ExecuteSQL.CallView(133, "", "ReportViewID=" + lReportViewID, "");
                DataView dvField = new DataView(dtField);

                DataTable dtFieldValue = new DataTable("Field");
                dtFieldValue.Columns.Add("FieldName", typeof(string));
                dtFieldValue.Columns.Add("FieldValue", typeof(string));

                DataRow drRecord = null;
                DataTable dtRecord = new DataTable("Field");

                foreach (Control ctl in this.pnlField.Controls)
                {
                    string strName = "";
                    string strValue = "";
                    if(ctl is CoolTextBox)
                    {
                        CoolTextBox txt = ctl as CoolTextBox;
                        strName = txt.Name;
                        strValue = txt.Text;
                    }
                    else if (ctl is LBDateTime)
                    {
                        LBDateTime txt = ctl as LBDateTime;
                        txt.Focus();
                        strName = txt.Name;
                        strValue = txt.TextBox.Text.ToString();
                    }

                    dvField.RowFilter = "FieldName='"+ strName + "'";
                    if (dvField.Count > 0)
                    {
                        DataRow drField = dtFieldValue.NewRow();
                        drField["FieldName"] = strName;
                        drField["FieldValue"] = strValue;
                        dtFieldValue.Rows.Add(drField);
                    }

                    if (!dtRecord.Columns.Contains(strName))
                    {
                        dtRecord.Columns.Add(strName, typeof(string));
                    }
                }

                drRecord = dtRecord.NewRow();
                foreach (DataRow drField in dtFieldValue.Rows)
                {
                    if (dtRecord.Columns.Contains(drField["FieldName"].ToString()))
                    {
                        drRecord[drField["FieldName"].ToString()] = drField["FieldValue"];
                    }
                }
                dtRecord.Rows.Add(drRecord);

                ReportRequestArgs args;
                args = new ReportRequestArgs(lReportTemplateID, 8, null, null);

                DataTable dtSPIN = new DataTable();
                dtSPIN.Columns.Add("ReportViewID", typeof(long));
                dtSPIN.Columns.Add("DTFieldValue", typeof(DataTable));
                DataRow drNew = dtSPIN.NewRow();
                drNew["ReportViewID"] = lReportViewID;
                drNew["DTFieldValue"] = dtFieldValue;
                dtSPIN.Rows.Add(drNew);
                dtSPIN.AcceptChanges();

                DataSet dsReturn;
                DataTable dtOut;
                ExecuteSQL.CallSP(14407, dtSPIN, out dsReturn, out dtOut);

                DataTable dtSource = null;
                if (dsReturn != null && dsReturn.Tables.Count > 0)
                {
                    dtSource = dsReturn.Tables[0].Copy();
                    dtSource.TableName = "T008";
                    DataSet dsSource = new DataSet("Report");
                    dsSource.Tables.Add(dtSource);
                    args.DSDataSource = dsSource;
                    args.ReportTemplateID = lReportTemplateID;
                    args.RecordDR = drRecord;

                    FastReport.Report report = ReportHelper.GetReport(args);
                    report.Preview = this.previewControl1;
                    report.Show();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
