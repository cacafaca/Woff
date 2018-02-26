using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCode.Woff2
{
    /// <summary>
    /// Reference: https://www.w3.org/TR/WOFF2/
    /// </summary>
    public class Woff2
    {
        #region Constructor

        public Woff2(Stream inputData)
        {
            streamLength = inputData.Length;

            // Process Header.
            header = new Woff2Header(inputData);
            ValidateHeader();

            // Process Font Directory.
            fontDirectory = new Woff2FontDirectory(inputData, header.NumTables);
            ValidateTableDirectoryEntry();
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Properties

        public Woff2Header Header { get; }
        public float CompressionRatio { get { return header.MetaOrigLength / streamLength; } }

        public Woff2FontDirectory FontDirectory { get { return fontDirectory; } }

        #endregion

        #region Private Properties

        Woff2Header header;
        long streamLength;
        Woff2FontDirectory fontDirectory;

        #endregion

        #region PrivateMethods

        private void ValidateHeader()
        {
            if (CompressionRatio > maxCompressionRatio)
                throw new Exception("Bad uncompressed size.");
        }

        private void ValidateTableDirectoryEntry()
        {

        }

        #endregion

        #region Private Constants

        const uint maxCompressionRatio = 100;

        #endregion
    }
}
