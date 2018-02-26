using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ProCode.Woff2
{
    public class Woff2FontDirectory : List<Woff2TableDirectoryEntry>
    {
        #region Constructors

        /// <summary>
        /// Use this constructor to construct Font Directory (a list of Table Directory Entries) out of stream.
        /// </summary>
        /// <param name="fontDirectoryStream">Stream positioned on start of Font Directory.</param>
        /// <param name="numTables">Number of Table Directory Entries or Tables.</param>
        public Woff2FontDirectory(Stream fontDirectoryStream, UInt16 numTables)
        {
            if (fontDirectoryStream.CanRead)
                for (int tabIndex = 0; tabIndex < numTables; tabIndex++)
                {
                    Add(new Woff2TableDirectoryEntry(fontDirectoryStream));
                }
            else
                throw new WoffUtility.CantReadStreamException(fontDirectoryStream);
        }

        /// <summary>
        /// Use this constructor to construct manually Font Directory.
        /// </summary>
        public Woff2FontDirectory(List<Woff2TableDirectoryEntry> fontDirectory)
        {
            AddRange(fontDirectory);
        }

        #endregion

        #region Private Properties


        #endregion
    }
}
