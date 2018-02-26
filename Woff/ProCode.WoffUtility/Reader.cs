using System;
using System.IO;
using System.Linq;

namespace ProCode.WoffUtility
{
    public class Reader
    {
        #region Public Static Methods

        /// <summary>
        /// Reads UIntBase128 Data Type as written in https://www.w3.org/TR/WOFF2/.
        /// </summary>
        /// <param name="encodedStream"></param>
        /// <param name="result"></param>
        public static void ReadUIntBase128(Stream encodedStream, out UInt32 result)
        {
            Stream decodedStream = new MemoryStream();
            result = UInt32.MinValue;

            if (encodedStream.CanRead)
            {
                UInt32 accum = 0;
                for (int i = 0; i < 5; i++)
                {
                    var dataByte = encodedStream.ReadByte();

                    // No leading 0's.  0x80 = 1000 0000
                    if (i == 0 && dataByte == 0x80)
                        throw new NoLeadingZerosException();

                    // If any of top 7 bits are set then << 7 would overflow. 0xfe000000 = 1111 1110 0000 0000 0000 0000 0000 0000.
                    if ((accum & 0xfe000000) > 0)
                        throw new UIntBase128OverflowException();

                    accum = (accum << 7) | ((UInt32)dataByte & 0x7f); // 0x7f = 0111 1111

                    // Spin until most significant bit of data byte is false.
                    if ((dataByte & 0x80) != 0)
                    {
                        result = accum;
                        return; 
                    }
                }
                // UIntBase128 sequence exceeds 5 bytes.
                throw new UIntBase128SequenceToLongException();
            }
            else
                throw new Exception("Can't read stream.");
        }

        public static void ReadProperty(Stream headerStream, ref object property)
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(property);
            byte[] propertyArray = new byte[size];
            headerStream.Read(propertyArray, 0, size);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(propertyArray);

            switch ((string)property.GetType().ToString().Split('.').Last())
            {
                case Constants.ByteName:
                    property = propertyArray.First();
                    break;
                case Constants.UInt16Name:
                    property = BitConverter.ToUInt16(propertyArray, 0);
                    break;
                case Constants.UInt32Name:
                    property = BitConverter.ToUInt32(propertyArray, 0);
                    break;
                default:
                    throw new ArgumentException("Unexpected type of property.");
            }

        }

        #endregion

    }
}
