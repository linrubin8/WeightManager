using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;

namespace FastReport.Wizards
{
  /// <summary>
  /// Represents the "Blank Report" wizard.
  /// </summary>
  public class BlankReportWizard : WizardBase
  {
    /// <inheritdoc/>
    public override bool Run(Designer designer)
    {
      designer.CreateEmptyReport();
      return true;
    }
  }
}
