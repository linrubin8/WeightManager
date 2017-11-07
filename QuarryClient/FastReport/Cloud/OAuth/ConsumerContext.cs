using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Cloud.OAuth
{
    /// <summary>
    /// Represents the consumer.
    /// </summary>
    public class ConsumerContext
    {
        #region Fields

        private string consumerKey;
        private string consumerSecret;
        private string signatureMethod;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the consumer key.
        /// </summary>
        public string ConsumerKey
        {
            get { return consumerKey; }
        }

        /// <summary>
        /// Gest the consumer secret.
        /// </summary>
        public string ConsumerSecret
        {
            get { return consumerSecret; }
        }

        /// <summary>
        /// Gets the consumer's signature method.
        /// </summary>
        public string SignatureMethod
        {
            get { return signatureMethod; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerContext"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public ConsumerContext(string consumerKey, string consumerSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            signatureMethod = OAuth.SignatureMethod.HmacSha1;
        }

        #endregion // Constructors
    }
}
