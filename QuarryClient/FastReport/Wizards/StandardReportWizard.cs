using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Forms;

namespace FastReport.Wizards
{
  /// <summary>
  /// Represents the "Standard Report" wizard.
  /// </summary>
  public class StandardReportWizard : WizardBase
  {
    /// <inheritdoc/>
    public override bool Run(Designer designer)
    {
      if (!designer.CreateEmptyReport())
        return false;

      using (StandardReportWizardForm form = new StandardReportWizardForm())
      {
        form.InitWizard(designer.ActiveReport);
        return form.ShowDialog() == DialogResult.OK;
      }
    }
  }
}
