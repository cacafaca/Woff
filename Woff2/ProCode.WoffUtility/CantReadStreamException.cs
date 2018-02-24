using System;
using System.IO;

namespace ProCode.WoffUtility
{
    public class CantReadStreamException : Exception
    {
        #region Constructors

        public CantReadStreamException(string message, Stream stream)
            : base(message)
        {
            NoReadStream = stream;
        }

        #endregion

        #region Public Properties

        public Stream NoReadStream { get; private set; }

        #endregion
    }
}
