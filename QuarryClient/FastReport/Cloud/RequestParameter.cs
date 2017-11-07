using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Cloud
{
    /// <summary>
    /// Represents the parameter of http request.
    /// </summary>
    public class RequestParameter
    {
        #region Fields

        private string name;
        private string value;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the name of a request parameter.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the value of a request parameter.
        /// </summary>
        public string Value
        {
            get { return this.value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a naw instance of the <see cref="RequestParameter"/> class with a specified parameters.
        /// </summary>
        /// <param name="name">The name of a request parameter.</param>
        /// <param name="value">The value of a request paramter.</param>
        public RequestParameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        #endregion // Constructors
    }

    /// <summary>
    /// Comparer class for comparing request parameters.
    /// </summary>
    public class RequestParameterComparer : IComparer<RequestParameter>
    {
        #region IComparer<RequestParameter> Members

        /// <inheritdoc/>
        public int Compare(RequestParameter x, RequestParameter y)
        {
            return ((x.Name != y.Name) ? String.Compare(x.Name, y.Name) : String.Compare(x.Value, y.Value));
        }

        #endregion // IComparer<RequestParameter> Members
    }
}
