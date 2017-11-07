using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.TypeEditors
{
    /// <summary>
    /// Provides a user interface for editing a text outline.
    /// </summary>
    public class OutlineEditor : UITypeEditor
    {
        #region Public Methods

        /// <inheritdoc/>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context.Instance is object[])
                return base.GetEditStyle(context);
            return UITypeEditorEditStyle.Modal;
        }

        /// <inheritdoc/>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            TextOutline outline = Value as TextOutline;
            if (outline != null)
                return Editors.EditOutline(outline);
            return Value;
        }

        #endregion // Public Methods
    }
}
