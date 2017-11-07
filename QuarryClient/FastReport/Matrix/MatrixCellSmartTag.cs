using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;
using FastReport.Table;

namespace FastReport.Matrix
{
  internal class MatrixCellSmartTag : DataColumnSmartTag
  {
    private MatrixObject FMatrix;
    private MatrixDescriptor FDescriptor;
    
    protected override DataSourceBase FindRootDataSource()
    {
      return FMatrix.DataSource;
    }

    protected override void ItemClicked()
    {
      FDescriptor.Expression = DataColumn == "" ? "" : "[" + DataColumn + "]";
      FMatrix.BuildTemplate();
      base.ItemClicked();
    }
    
    public MatrixCellSmartTag(MatrixObject matrix, MatrixDescriptor descriptor) : base(descriptor.TemplateCell)
    {
      FMatrix = matrix;
      FDescriptor = descriptor;
      
      string expression = descriptor.Expression;
      if (expression.StartsWith("[") && expression.EndsWith("]"))
        expression = expression.Substring(1, expression.Length - 2);
      DataColumn = expression;
    }
  }
}
