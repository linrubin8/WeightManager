using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Map
{
    /// <summary>
    /// Represents a collection of <see cref="MapLayer"/> objects.
    /// </summary>
    public class LayerCollection : FRCollectionBase
    {
        #region Properties

        /// <summary>
        /// Gets a layer with specified index.
        /// </summary>
        /// <param name="index">Index of a layer.</param>
        /// <returns>The layer with specified index.</returns>
        public MapLayer this[int index]
        {
            get { return List[index] as MapLayer; }
            set { List[index] = value; }
        }

        #endregion // Properties

        internal LayerCollection(Base owner) : base(owner)
        {
        }
    }
}
