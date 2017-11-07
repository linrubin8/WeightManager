using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Design;
using FastReport.Utils;

namespace FastReport.Wizards
{
  /// <summary>
  /// The base class for all report wizards.
  /// </summary>
  /// <remarks>
  /// To create own wizard, use this class as a base. All you need is to override 
  /// the <see cref="Run"/> method. To register a wizard, use the 
  /// <see cref="RegisteredObjects.AddWizard(Type,Bitmap,string,bool)"/> method.
  /// </remarks>
  public abstract class WizardBase
  {
    /// <summary>
    /// Runs the wizard.
    /// </summary>
    /// <param name="designer">Report designer.</param>
    /// <returns><b>true</b> if wizard was executed succesfully.</returns>
    /// <remarks>
    /// This method is called when you select a wizard in the "Add New Item" window and
    /// click "Add" button. You should do the work in this method.
    /// </remarks>
    public abstract bool Run(Designer designer);
  }
}
