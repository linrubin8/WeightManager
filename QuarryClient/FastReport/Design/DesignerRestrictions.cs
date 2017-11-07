using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FastReport.Design
{
  /// <summary>
  /// Represents a set of designer's restrictions.
  /// </summary>
  public class DesignerRestrictions
  {
    #region Fields
    private bool FDontLoadReport;
    private bool FDontSaveReport;
    private bool FDontCreateReport;
    private bool FDontPreviewReport;
    private bool FDontShowRecentFiles;
    private bool FDontEditCode;
    private bool FDontEditData;
    private bool FDontCreateData;
    private bool FDontChangeReportOptions;
    private bool FDontInsertObject;
    private bool FDontInsertBand;
    private bool FDontDeletePage;
    private bool FDontCreatePage;
    private bool FDontChangePageOptions;
    // if you add something new, don't forget to add it in the Assign method too!
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value that enables or disables the "Open" action.
    /// </summary>
    [DefaultValue(false)]
    public bool DontLoadReport
    {
      get { return FDontLoadReport; }
      set { FDontLoadReport = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Save/Save as" actions.
    /// </summary>
    [DefaultValue(false)]
    public bool DontSaveReport
    {
      get { return FDontSaveReport; }
      set { FDontSaveReport = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "New..." action.
    /// </summary>
    [DefaultValue(false)]
    public bool DontCreateReport
    {
      get { return FDontCreateReport; }
      set { FDontCreateReport = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Preview" action.
    /// </summary>
    [DefaultValue(false)]
    public bool DontPreviewReport
    {
      get { return FDontPreviewReport; }
      set { FDontPreviewReport = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the recent files list.
    /// </summary>
    [DefaultValue(false)]
    public bool DontShowRecentFiles
    {
      get { return FDontShowRecentFiles; }
      set { FDontShowRecentFiles = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Code" tab.
    /// </summary>
    [DefaultValue(false)]
    public bool DontEditCode
    {
      get { return FDontEditCode; }
      set { FDontEditCode = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Data" menu.
    /// </summary>
    [DefaultValue(false)]
    public bool DontEditData
    {
      get { return FDontEditData; }
      set { FDontEditData = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Data|Add New Data Source..." menu.
    /// </summary>
    [DefaultValue(false)]
    public bool DontCreateData
    {
      get { return FDontCreateData; }
      set { FDontCreateData = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Report|Options..." menu.
    /// </summary>
    [DefaultValue(false)]
    public bool DontChangeReportOptions
    {
      get { return FDontChangeReportOptions; }
      set { FDontChangeReportOptions = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables insertion of objects.
    /// </summary>
    [DefaultValue(false)]
    public bool DontInsertObject
    {
      get { return FDontInsertObject; }
      set { FDontInsertObject = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the insertion of bands.
    /// </summary>
    [DefaultValue(false)]
    public bool DontInsertBand
    {
      get { return FDontInsertBand; }
      set { FDontInsertBand = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Delete Page" action.
    /// </summary>
    [DefaultValue(false)]
    public bool DontDeletePage
    {
      get { return FDontDeletePage; }
      set { FDontDeletePage = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the creation of report/dialog pages.
    /// </summary>
    [DefaultValue(false)]
    public bool DontCreatePage
    {
      get { return FDontCreatePage; }
      set { FDontCreatePage = value; }
    }

    /// <summary>
    /// Gets or sets a value that enables or disables the "Page Setup" action.
    /// </summary>
    [DefaultValue(false)]
    public bool DontChangePageOptions
    {
      get { return FDontChangePageOptions; }
      set { FDontChangePageOptions = value; }
    }
    #endregion

    /// <summary>
    /// Copies the contents of another, similar object.
    /// </summary>
    /// <param name="source">Source object to copy the contents from.</param>
    public void Assign(DesignerRestrictions source)
    {
      DontLoadReport = source.DontLoadReport;
      DontSaveReport = source.DontSaveReport;
      DontCreateReport = source.DontCreateReport;
      DontPreviewReport = source.DontPreviewReport;
      DontShowRecentFiles = source.DontShowRecentFiles;
      DontEditCode = source.DontEditCode;
      DontEditData = source.DontEditData;
      DontCreateData = source.DontCreateData;
      DontChangeReportOptions = source.DontChangeReportOptions;
      DontInsertObject = source.DontInsertObject;
      DontInsertBand = source.DontInsertBand;
      DontDeletePage = source.DontDeletePage;
      DontCreatePage = source.DontCreatePage;
      DontChangePageOptions = source.DontChangePageOptions;
    }
    
    /// <summary>
    /// Creates exact copy of this object.
    /// </summary>
    /// <returns>The copy of this object.</returns>
    public DesignerRestrictions Clone()
    {
      DesignerRestrictions restrictions = new DesignerRestrictions();
      restrictions.Assign(this);
      return restrictions;
    }
  }
}
