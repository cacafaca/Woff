﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCode.Woff2
{
    /// <summary>
    /// 
    /// Header Structure:
    ///                             -=   Position   =-
    /// Type    Name                Address     Offset  Description
    /// ==============================================================================================
    /// UInt32  signature           00000000    0       0x774F4632 'wOF2'
    /// UInt32  flavor              00000000    4       The "sfnt version" of the input font.
    /// UInt32  length              00000000    8       Total size of the WOFF file.
    /// UInt16  numTables           00000000    c       Number of entries in directory of font tables.
    /// UInt16  reserved            00000000    d       Reserved; set to 0.
    /// UInt32  totalSfntSize       00000001    0       Total size needed for the uncompressed font data, including the sfnt header, directory, and font tables(including padding). 
    /// UInt32  totalCompressedSize 00000001    4       Total length of the compressed data block.
    /// UInt16  majorVersion        00000001    8       Major version of the WOFF file.
    /// UInt16  minorVersion        00000001    a       Minor version of the WOFF file.
    /// UInt32  metaOffset          00000001    c       Offset to metadata block, from beginning of WOFF file.
    /// UInt32  metaLength          00000002    0       Length of compressed metadata block.
    /// UInt32  metaOrigLength      00000002    4       Uncompressed size of metadata block.
    /// UInt32  privOffset          00000002    8       Offset to private data block, from beginning of WOFF file.
    /// UInt32  privLength          00000002    c       Length of private data block.    
    /// 
    /// Sum is 48 Bytes to read.
    /// 
    /// </summary>
    public class Woff2Header
    {
        #region Constructors

        public Woff2Header(Stream headerStream)
        {
            valid = false;

            if (headerStream == null)
                throw new ArgumentNullException(nameof(headerStream));

            if (headerStream.CanRead)
            {
                if (headerStream.Position > 0)
                    headerStream.Position = 0;

                ReadSignature(headerStream);            // UInt32 signature             0x774F4632 'wOF2'
                ReadFlavor(headerStream);               // UInt32 flavor                The "sfnt version" of the input font.
                ReadLength(headerStream);               // UInt32 length                Total size of the WOFF file.
                ReadNumTables(headerStream);            // UInt16 numTables             Number of entries in directory of font tables.
                ReadReserved(headerStream);             // UInt16 reserved              Reserved; set to 0.
                ReadTotalSfntSize(headerStream);        // UInt32 totalSfntSize         Total size needed for the uncompressed font data, including the sfnt header, directory, and font tables(including padding).
                ReadTotalCompressedSize(headerStream);  // UInt32 totalCompressedSize   Total length of the compressed data block.
                ReadMajorVersion(headerStream);         // UInt16 majorVersion          Major version of the WOFF file.
                ReadMinorVersion(headerStream);         // UInt16 minorVersion          Minor version of the WOFF file.
                ReadMetaOffset(headerStream);           // UInt32 metaOffset            Offset to metadata block, from beginning of WOFF file.
                ReadMetaLength(headerStream);           // UInt32 metaLength            Length of compressed metadata block.
                ReadMetaOrigLength(headerStream);       // UInt32 metaOrigLength        Uncompressed size of metadata block.
                ReadPrivOffset(headerStream);           // UInt32 privOffset            Offset to private data block, from beginning of WOFF file.
                ReadPrivLength(headerStream);           // UInt32 privLength            Length of private data block.    

                valid = true;
            }
            else
                throw new WoffUtility.CantReadStreamException(headerStream);
        }

        #endregion

        #region Public Properties

        public UInt32 Signature { get { return signature; } }
        public UInt32 Flavor { get { return flavor; } }
        public UInt32 Length { get { return length; } }
        public UInt16 NumTables { get { return numTables; } }
        public UInt16 Reserved { get { return reserved; } }
        public UInt32 TotalSfntSize { get { return totalSfntSize; } }
        public UInt32 TotalCompressedSize { get { return totalCompressedSize; } }
        public UInt16 MajorVersion { get { return majorVersion; } }
        public UInt16 MinorVersion { get { return minorVersion; } }
        public UInt32 MetaOffset { get { return metaOffset; } }
        public UInt32 MetaLength { get { return metaLength; } }
        public UInt32 MetaOrigLength { get { return metaOrigLength; } }
        public UInt32 PrivOffset { get { return privOffset; } }
        public UInt32 PrivLength { get { return privLength; } }

        public bool Valid { get { return valid; } }

        #endregion

        #region Public Constants

        /// <summary>
        /// Size of Header in Bytes.
        /// </summary>
        public static readonly int HeaderSize = 48;

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return $"{nameof(Signature)}: {signature}, {nameof(Flavor)}: {flavor}, {nameof(Length)}: {length}, " +
                $"{nameof(NumTables)}: {numTables}, {nameof(Reserved)}: {reserved}, {nameof(TotalSfntSize)}: {totalSfntSize}, " +
                $"{nameof(TotalCompressedSize)}: {totalCompressedSize}, {nameof(MajorVersion)}: {majorVersion}, " +
                $"{nameof(MinorVersion)}: {minorVersion}, {nameof(MetaOffset)}: {metaOffset}, {nameof(MetaLength)}: {metaLength}, " +
                $"{nameof(MetaOrigLength)}: {metaOrigLength}, {nameof(PrivOffset)}: {privOffset}, {nameof(PrivLength)}: {privLength}";
        }

        #endregion

        #region Private Properties

        UInt32 signature;
        UInt32 flavor;
        UInt32 length;
        UInt16 numTables;
        UInt16 reserved;
        UInt32 totalSfntSize;
        UInt32 totalCompressedSize;
        UInt16 majorVersion;
        UInt16 minorVersion;
        UInt32 metaOffset;
        UInt32 metaLength;
        UInt32 metaOrigLength;
        UInt32 privOffset;
        UInt32 privLength;

        bool valid;

        #endregion

        #region Private Methods


        #endregion

        #region Read Header Properties

        /// <summary>
        /// UInt32 signature            0x774F4632 'wOF2'
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadSignature(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            signature = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 flavor               The "sfnt version" of the input font.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadFlavor(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            flavor = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 length               Total size of the WOFF file.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadLength(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            length = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt16 numTables            Number of entries in directory of font tables.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadNumTables(Stream headerStream)
        {
            object outputValue = UInt16.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            numTables = (UInt16)outputValue;
        }

        /// <summary>
        /// UInt16 reserved             Reserved; set to 0.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadReserved(Stream headerStream)
        {
            object outputValue = UInt16.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            reserved = (UInt16)outputValue;
        }

        /// <summary>
        /// UInt32 totalSfntSize        Total size needed for the uncompressed font data, including the sfnt header, directory, and font tables(including padding).
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadTotalSfntSize(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            totalSfntSize = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 totalCompressedSize  Total length of the compressed data block.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadTotalCompressedSize(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            totalCompressedSize = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt16 majorVersion         Major version of the WOFF file.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadMajorVersion(Stream headerStream)
        {
            object outputValue = UInt16.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            majorVersion = (UInt16)outputValue;
        }

        /// <summary>
        /// UInt16 minorVersion         Minor version of the WOFF file.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadMinorVersion(Stream headerStream)
        {
            object outputValue = UInt16.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            minorVersion = (UInt16)outputValue;
        }

        /// <summary>
        /// UInt32 metaOffset           Offset to metadata block, from beginning of WOFF file.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadMetaOffset(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            metaOffset = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 metaLength           Length of compressed metadata block.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadMetaLength(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            metaLength = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 metaOrigLength       Uncompressed size of metadata block.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadMetaOrigLength(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            metaOrigLength = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 privOffset             Offset to private data block, from beginning of WOFF file.
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadPrivOffset(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            privOffset = (UInt32)outputValue;
        }

        /// <summary>
        /// UInt32 privLength             Length of private data block.    
        /// </summary>
        /// <param name="headerStream"></param>
        private void ReadPrivLength(Stream headerStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(headerStream, ref outputValue);
            privLength = (UInt32)outputValue;
        }

        #endregion
    }
}
