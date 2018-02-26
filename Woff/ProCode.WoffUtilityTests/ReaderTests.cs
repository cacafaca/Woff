using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ProCode.WoffUtility.Tests
{
    [TestClass()]
    public class ReaderTests
    {
        [TestMethod()]
        public void ReadUIntBase128_Valid_Sequence()
        {
            Stream stream = new MemoryStream(new byte[6] { 0x1c, 0x1a, 0x81, 0x2e, 0x1b, 0xa1 });
            UInt32 expected = 0x070d01;
            UInt32 actual;
            Reader.ReadUIntBase128(stream, out actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadUIntBase128_Invalid_Sequence()
        {
            Stream stream = new MemoryStream(new byte[6] { 0x1c, 0x1a, 0x2e, 0x1b, 0xa1, 0x81 });
            UInt32 actual;
            try
            {
                Reader.ReadUIntBase128(stream, out actual);
                Assert.Fail();
            }
            catch (WoffUtility.WoffBaseException) { }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}