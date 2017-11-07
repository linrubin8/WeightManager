using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Table;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Matrix
{
  internal class MatrixCellMenuBase : TableMenuBase
  {
    private MatrixObject FMatrix;
    private MatrixElement FElement;
    private MatrixDescriptor FDescriptor;

    public ButtonItem miEdit;
    public ButtonItem miFormat;
    public ButtonItem miHyperlink;
    public ButtonItem miDelete;

    public MatrixObject Matrix
    {
      get { return FMatrix; }
    }
    
    public MatrixElement Element
    {
      get { return FElement; }
    }

    public MatrixDescriptor Descriptor
    {
      get { return FDescriptor; }
    }

    public TableCell Cell
    {
      get { return Designer.SelectedObjects[0] as TableCell; }
    }

    private void miEdit_Click(object sender, EventArgs e)
    {
      Matrix.HandleCellDoubleClick(Cell);
    }

    private void miFormat_Click(object sender, EventArgs e)
    {
      using (FormatEditorForm form = new FormatEditorForm())
      {
        form.TextObject = Cell;
        if (form.ShowDialog() == DialogResult.OK)
        {
          SelectedTextBaseObjects components = new SelectedTextBaseObjects(Designer);
          components.Update();
          components.SetFormat(form.Formats);
          Change();
        }
      }
    }

    private void miHyperlink_Click(object sender, EventArgs e)
    {
      using (HyperlinkEditorForm form = new HyperlinkEditorForm())
      {
        form.ReportComponent = Cell;
        if (Element == MatrixElement.Cell)
          form.IsMatrixHyperlink = true;
           
        if (form.ShowDialog() == DialogResult.OK)
        {
          SelectedReportComponents components = new SelectedReportComponents(Designer);
          components.Update();
          components.SetHyperlink(form.Hyperlink, form.ModifyAppearance, false);
          Change();
        }
      }
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      if ((Element == MatrixElement.Column || Element == MatrixElement.Row) &&
        (Descriptor as MatrixHeaderDescriptor).TemplateTotalCell == Cell)
      {  
        (Descriptor as MatrixHeaderDescriptor).Totals = false;
      }
      else
      {  
        switch (Element)
        {
          case MatrixElement.Column:
            Matrix.Data.Columns.Remove(Descriptor as MatrixHeaderDescriptor);
            break;
            
          case MatrixElement.Row:
            Matrix.Data.Rows.Remove(Descriptor as MatrixHeaderDescriptor);
            break;
            
          case MatrixElement.Cell:
            Matrix.Data.Cells.Remove(Descriptor as MatrixCellDescriptor);
            break;
        }
      }
      
      Change();
    }
    
    protected override void Change()
    {
      Matrix.BuildTemplate();
      Designer.SetModified(Matrix, "Change");
    }

    public MatrixCellMenuBase(MatrixObject matrix, MatrixElement element, MatrixDescriptor descriptor) :
      base(matrix.Report.Designer)
    {
      FMatrix = matrix;
      FElement = element;
      FDescriptor = descriptor;

      miEdit = CreateMenuItem(null, Res.Get("ComponentMenu,Component,Edit"), new EventHandler(miEdit_Click));
      miFormat = CreateMenuItem(Res.GetImage(168), Res.Get("ComponentMenu,TextObject,Format"), new EventHandler(miFormat_Click));
      miHyperlink = CreateMenuItem(Res.GetImage(167), Res.Get("ComponentMenu,ReportComponent,Hyperlink"), new EventHandler(miHyperlink_Click));
      miDelete = CreateMenuItem(Res.GetImage(51), Res.Get("Designer,Menu,Edit,Delete"), new EventHandler(miDelete_Click));
      miDelete.BeginGroup = true;

      Items.AddRange(new BaseItem[] { 
        miEdit, miFormat, miHyperlink, 
        miDelete });
      
      bool enabled = Designer.SelectedObjects.Count == 1;
      miEdit.Enabled = enabled;
      miDelete.Enabled = enabled && descriptor != null && !matrix.IsAncestor;
    }
  }
}
