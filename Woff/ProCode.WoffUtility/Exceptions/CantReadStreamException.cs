using System;
using System.IO;

namespace ProCode.WoffUtility
{
    public class CantReadStreamException : WoffBaseException
    {
        #region Constructors

        public CantReadStreamException(string message, Stream stream)
            : base(message)
        {
            NoReadStream = stream;
        }

        /// <summary>
        /// Sends default message "Can't read stream."
        /// </summary>
        /// <param name="stream"></param>
        public CantReadStreamException(Stream stream)
            : base("Can't read stream.")
        {

        }

        #endregion

        #region Public Properties

        public Stream NoReadStream { get; private set; }

        #endregion
    }
}
