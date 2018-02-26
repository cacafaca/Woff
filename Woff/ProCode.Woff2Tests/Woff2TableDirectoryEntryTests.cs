using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ProCode.Woff2.Tests
{
    [TestClass()]
    public class Woff2TableDirectoryEntryTests
    {
        /// <summary>
        /// Test not completed yet.
        /// </summary>
        [TestMethod()]
        public void Woff2TableDirectoryEntryTest()
        {
            Woff2Header woff2Header;
            Woff2TableDirectoryEntry tabDirEntry;
            using (var woff2Resource = new MemoryStream(Woff2Tests.Properties.Resources.arial_woff2))
            {
                woff2Header = new Woff2Header(woff2Resource);
                tabDirEntry = new Woff2TableDirectoryEntry(woff2Resource);
            }

            // Expected values.
            byte expectedFlags = 0x3f;
            UInt32 expectedTag = 0x4646544d;
            //UInt32 
        }
    }
}