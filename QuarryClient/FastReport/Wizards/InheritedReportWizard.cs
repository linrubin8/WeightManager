using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;
using FastReport.Utils;

namespace FastReport.Wizards
{
  /// <summary>
  /// Represents the "Inherited Report" wizard.
  /// </summary>
  public class InheritedReportWizard : WizardBase
  {
    /// <inheritdoc/>
    public override bool Run(Designer designer)
    {
      if (!designer.CreateEmptyReport())
        return false;

      OpenSaveDialogEventArgs e = new OpenSaveDialogEventArgs(designer);
      Config.DesignerSettings.OnCustomOpenDialog(designer, e);
      if (e.Cancel)
        return false;

      designer.Lock();
      try
      {
        designer.ActiveReport.BaseReport = e.FileName;
      }
      finally
      {
        designer.InitReport();
        designer.Unlock();
      }
      return true;
    }
  }
}
