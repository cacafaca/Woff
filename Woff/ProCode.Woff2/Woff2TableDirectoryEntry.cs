using System;
using System.IO;

namespace ProCode.Woff2
{
    public class Woff2TableDirectoryEntry
    {
        #region Constructors

        /// <summary>
        /// Use this constructor to construct Table Directory Entry from stream.
        /// </summary>
        /// <param name="tableDirectoryEntryStream">Stream positioned on Table Directory Entry</param>
        public Woff2TableDirectoryEntry(Stream tableDirectoryEntryStream)
        {
            if (tableDirectoryEntryStream == null)
                throw new ArgumentNullException(nameof(tableDirectoryEntryStream));

            if (tableDirectoryEntryStream.CanRead)
            {
                ReadFlags(tableDirectoryEntryStream);
                ReadTag(tableDirectoryEntryStream);
                ReadOrigLength(tableDirectoryEntryStream);
                ReadTransformLength(tableDirectoryEntryStream);
            }
            else
                throw new WoffUtility.CantReadStreamException("Can't read.", tableDirectoryEntryStream);
        }

        /// <summary>
        /// Use this constructor to construct Table Directory Entry manually.
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="tag"></param>
        /// <param name="origLength"></param>
        /// <param name="transformLength"></param>
        public Woff2TableDirectoryEntry(byte flags, UInt32 tag, UInt32 origLength, UInt32 transformLength)
        {
            this.flags = flags;
            this.tag = tag;
            this.origLength = origLength;
            this.transformLength = transformLength;
        }

        #endregion

        #region Public Properties

        public byte Flags { get { return flags; } }
        public UInt32 Tag { get { return tag; } }
        public UInt32 OrigLength { get { return origLength; } }
        public UInt32 TransformLength { get { return transformLength; } }

        #endregion

        #region Private Properties

        byte flags;
        UInt32 tag;
        UInt32 origLength;
        UInt32 transformLength;

        #endregion

        #region Private Methods

        private void ReadTransformLength(Stream tableDirectoryEntryStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            transformLength = (UInt32)outputValue;
        }

        private void ReadOrigLength(Stream tableDirectoryEntryStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            origLength = (UInt32)outputValue;
        }

        private void ReadTag(Stream tableDirectoryEntryStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            tag = (UInt32)outputValue;
        }

        private void ReadFlags(Stream tableDirectoryEntryStream)
        {
            object outputValue = byte.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            flags = (byte)outputValue;
        }

        #endregion
    }
}
