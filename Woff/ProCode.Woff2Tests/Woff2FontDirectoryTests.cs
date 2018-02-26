using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProCode.Woff2.Tests
{
    [TestClass()]
    public class Woff2FontDirectoryTests
    {
        [TestMethod()]
        public void Woff2FontDirectoryTest()
        {
            Woff2Header woff2Header;
            Woff2FontDirectory fontDirectory;
            using (var woff2Resource = new MemoryStream(Woff2Tests.Properties.Resources.arial_woff2))
            {
                woff2Header = new Woff2Header(woff2Resource);
                fontDirectory = new Woff2FontDirectory(woff2Resource, woff2Header.NumTables);
            }

            // Expected values. Stream arial_woff2 has 19 Table Directory Entries.
            Woff2FontDirectory expectedFontDirectory = new Woff2FontDirectory(new List<Woff2TableDirectoryEntry>()
            {
                new Woff2TableDirectoryEntry(0x3f, 0x4646544d, 0x1c1a81, 0x2e1ba1),
                new Woff2TableDirectoryEntry(0x48, 0x1c85641e, 0x1, 0x2e1ba1)
            });

            Assert.AreEqual(expectedFontDirectory.Count, fontDirectory.Count);
            for (int i = 0; i < expectedFontDirectory.Count; i++)
            {
                Assert.AreEqual(expectedFontDirectory[i].Flags, fontDirectory[i].Flags);
                Assert.AreEqual(expectedFontDirectory[i].Tag, fontDirectory[i].Tag);
                Assert.AreEqual(expectedFontDirectory[i].OrigLength, fontDirectory[i].OrigLength);
                Assert.AreEqual(expectedFontDirectory[i].TransformLength, fontDirectory[i].TransformLength);
            }
        }
    }
}