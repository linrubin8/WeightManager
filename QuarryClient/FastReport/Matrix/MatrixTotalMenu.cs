using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Matrix
{
  internal class MatrixTotalMenu : MatrixCellMenuBase
  {

    public MatrixTotalMenu(MatrixObject matrix, MatrixElement element, MatrixDescriptor descriptor)
      : base(matrix, element, descriptor)
    {
    }
  }
}
