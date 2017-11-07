using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;

namespace FastReport.Wizards
{
  /// <summary>
  /// Represents the "New Page" wizard.
  /// </summary>
  public class NewPageWizard : WizardBase
  {
    /// <inheritdoc/>
    public override bool Run(Designer designer)
    {
      if (designer.Restrictions.DontCreatePage)
        return false;
      
      designer.cmdNewPage.Invoke();
      return true;
    }
  }
}
