using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class CtlFieldHeaderPanel : UserControl
    {
        private int iMaxRowCount = 0;//最大行数
        private int iMaxColumnCount = 0;//最大列数
        private int iHorSpace = 20;//两侧的空间距离
        private int iControlHorSpace = 10;//横向控件之间的距离
        private List<FieldControl> lstField = new List<FieldControl>();

        public int LBRowCount
        {
            get
            {
                return iMaxRowCount;
            }
            set
            {
                iMaxRowCount = value;
            }
        }

        public int LBColumnCount
        {
            get
            {
                return iMaxColumnCount;
            }
            set
            {
                iMaxColumnCount = value;
            }
        }

        public int LBHorSpace
        {
            get
            {
                return iHorSpace;
            }
            set
            {
                iHorSpace = value;
            }
        }

        public int LBControlHorSpace
        {
            get
            {
                return iControlHorSpace;
            }
            set
            {
                iControlHorSpace = value;
            }
        }

        public CtlFieldHeaderPanel()
        {
            InitializeComponent();

            this.SizeChanged += CtlFieldHeaderPanel_SizeChanged;
        }

        private void CtlFieldHeaderPanel_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (FieldControl fc in this.lstField)
                {
                    //计算当前控件区域的位置
                    int iSingleDistrictWidth = (this.Width - LBHorSpace * 2 - LBControlHorSpace * (LBColumnCount - 1)) / LBColumnCount;
                    int iDistrictW = (this.Width - LBHorSpace * 2 - LBControlHorSpace * (LBColumnCount - 1)) / LBColumnCount * fc.SpanColumn + LBControlHorSpace * (fc.SpanColumn - 1);
                    int iDistrictH = this.Height / LBRowCount;
                    int iDistrictX = LBHorSpace + (iSingleDistrictWidth + LBControlHorSpace) * (fc.ColumnIndex - 1);
                    int iDistrictY = this.Height / LBRowCount * (fc.RowIndex - 1);

                    int iControlHeight = fc.control.Height;
                    int iCaptionWidth = 0;
                    int iCaptionHeight = 0;
                    int iMargin = 0;//标题与控件之间的距离
                    if (fc.lable != null)
                    {
                        fc.lable.Font = this.Font;
                        //iMargin = 5;
                        iCaptionWidth = fc.lable.Width;
                        iCaptionHeight = fc.lable.Height;
                    }
                    int iControlX = iDistrictX + iCaptionWidth + iMargin;
                    int iControlY = iDistrictY + (iDistrictH - iControlHeight) / 2;
                    int iControlW = iDistrictW - iCaptionWidth - iMargin;
                    int iCaptionY = iDistrictY + (iDistrictH - iCaptionHeight) / 2;

                    if (fc.lable != null)
                    {
                        fc.lable.Location = new Point(iDistrictX, iCaptionY);
                    }
                    fc.control.Location = new Point(iControlX, iControlY);
                    fc.control.Width = iControlW;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="control"></param>
        /// <param name="rowIndex">行号：由1开始</param>
        /// <param name="columnIndex">列号：由1开始</param>
        /// <param name="SpanColumn">占用列数</param>
        public void AddFiledControl(string strCaption,Control control,int rowIndex,int columnIndex,int SpanColumn)
        {
            if (rowIndex <= 0|| columnIndex<=0)
            {
                throw new Exception("行号或列号必须大于0");
            }
            if (rowIndex > iMaxRowCount || columnIndex > iMaxColumnCount)
            {
                throw new Exception("行号或列号不能大于最大行数和列数");
            }
            if (iMaxColumnCount- columnIndex < SpanColumn-1)
            {
                throw new Exception("控件占用列数不能大于剩余列数");
            }

            //计算当前控件区域的位置
            int iSingleDistrictWidth = (this.Width - LBHorSpace * 2 - LBControlHorSpace * (LBColumnCount - 1)) / LBColumnCount;
            int iDistrictW = (this.Width - LBHorSpace * 2- LBControlHorSpace *(LBColumnCount-1)) / LBColumnCount* SpanColumn+ LBControlHorSpace*(SpanColumn-1);
            int iDistrictH = this.Height / LBRowCount;
            int iDistrictX = LBHorSpace + (iSingleDistrictWidth + LBControlHorSpace) * (columnIndex - 1);
            int iDistrictY = this.Height / LBRowCount * (rowIndex - 1);
            
            int iControlHeight = control.Height;
            int iCaptionWidth = 0;
            int iCaptionHeight = 0;
            int iMargin = 0;//标题与控件之间的距离
            Label lable = null;
            if (strCaption != "")
            {
                lable = new Label();
                lable.Text = strCaption;
                lable.Font = this.Font;
                lable.AutoSize = true;
                this.Controls.Add(lable);
                //iMargin = 5;
                iCaptionWidth = lable.Width;
                iCaptionHeight = lable.Height;
            }
            int iControlX = iDistrictX + iCaptionWidth+ iMargin;
            int iControlY = iDistrictY + (iDistrictH - iControlHeight) / 2;
            int iControlW = iDistrictW - iCaptionWidth - iMargin;
            int iCaptionY= iDistrictY + (iDistrictH - iCaptionHeight) / 2;

            if (lable != null)
            {
                
                lable.Location = new Point(iDistrictX, iCaptionY);
            }
            this.Controls.Add(control);
            control.Location = new Point(iControlX, iControlY);
            control.Width = iControlW;

            FieldControl fc = new LB.Controls.FieldControl(lable, control, rowIndex, columnIndex, SpanColumn);
            this.lstField.Add(fc);
        }

        
    }

    public class FieldControl
    {
        public Label lable;
        public Control control;
        public int RowIndex;
        public int ColumnIndex;
        public int SpanColumn;
        public FieldControl(Label lable, Control control, int RowIndex, int ColumnIndex,int SpanColumn)
        {
            this.lable = lable;
            this.control = control;
            this.RowIndex = RowIndex;
            this.ColumnIndex = ColumnIndex;
            this.SpanColumn = SpanColumn;
        }
    }
}
