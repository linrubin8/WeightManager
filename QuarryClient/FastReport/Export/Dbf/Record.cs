using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Export.Dbf
{
    /// <summary>
    /// Represents the record.
    /// </summary>
    public class Record
    {
        #region Constants

        private const int MAX_RECORD_CHARS = 4000;
        private const int MAX_RECORD_FIELDS = 255;
        internal const int MAX_FIELD_CHARS = 254;
        internal const int MAX_FIELD_NAME_CHARS = 10;

        #endregion // Constants

        #region Fields

        private List<StringBuilder> fields;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the field with a specified index.
        /// </summary>
        public StringBuilder this[int index]
        {
            get { return fields[index]; }
            set { fields[index] = value; }
        }

        /// <summary>
        /// Gets the count of a fields.
        /// </summary>
        public int Count
        {
            get { return fields.Count; }
        }

        /// <summary>
        /// Gets the size of a fields.
        /// </summary>
        public int Size
        {
            get { return GetSize(); }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        public Record()
        {
            fields = new List<StringBuilder>();
        }

        #endregion // Constructors

        #region Private Methods

        private int GetSize()
        {
            int size = 0;
            foreach (StringBuilder field in fields)
            {
                size += field.Length;
            }
            return size;
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Adds the new item into a list of fields.
        /// </summary>
        public void Add(StringBuilder item)
        {
            if (Count < MAX_RECORD_FIELDS)
            {
                if (item.Length > MAX_FIELD_CHARS)
                {
                    item.Remove(MAX_FIELD_CHARS, item.Length - MAX_FIELD_CHARS);
                }
                if (Size + item.Length > MAX_RECORD_CHARS)
                {
                    item.Remove(MAX_RECORD_CHARS - Size, item.Length - (MAX_RECORD_CHARS - Size));
                }
                fields.Add(item);
            }
        }

        /// <summary>
        /// Clears the list of fields.
        /// </summary>
        public void Clear()
        {
            fields.Clear();
        }

        #endregion // Public Methods
    }
}
