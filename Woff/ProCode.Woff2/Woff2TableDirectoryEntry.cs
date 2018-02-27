using System;
using System.IO;
using System.Linq;

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

                if (IsKnownTableTag())
                    tagValue = ((KnownTableTags)(flags & 0x3f)).Value(); 
                else
                    ReadTag(tableDirectoryEntryStream); // Read custom tag, because it is not known.

                ReadOrigLength(tableDirectoryEntryStream);

                if (IsNullTransform())
                    transformLength = origLength;
                else
                    ReadTransformLength(tableDirectoryEntryStream);
            }
            else
                throw new WoffUtility.CantReadStreamException(tableDirectoryEntryStream);
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
            this.tagValue = tag;
            this.origLength = origLength;
            this.transformLength = transformLength;
        }

        #endregion

        #region Public Properties

        public byte Flags { get { return flags; } }

        public UInt32 TagValue { get { return tagValue; } }

        public UInt32 OrigLength { get { return origLength; } }

        public UInt32 TransformLength { get { return transformLength; } }

        public byte TransformationVersion { get { return transformationVersion; } }

        public KnownTableTags Tag { get { return tag; } }

        public byte TagIndex { get { return tagIndex; } }

        #endregion

        #region Private Properties

        byte flags;
        UInt32 tagValue;
        UInt32 origLength;
        UInt32 transformLength;
        KnownTableTags tag;
        byte tagIndex;
        byte transformationVersion;

        #endregion

        #region Private Methods

        private void ReadTransformLength(Stream tableDirectoryEntryStream)
        {
            WoffUtility.Reader.ReadUIntBase128(tableDirectoryEntryStream, out transformLength);
        }

        private void ReadOrigLength(Stream tableDirectoryEntryStream)
        {
            WoffUtility.Reader.ReadUIntBase128(tableDirectoryEntryStream, out origLength);
        }

        private void ReadTag(Stream tableDirectoryEntryStream)
        {
            object outputValue = UInt32.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            tagValue = (UInt32)outputValue;
        }

        private void ReadFlags(Stream tableDirectoryEntryStream)
        {
            object outputValue = byte.MinValue;
            WoffUtility.Reader.ReadProperty(tableDirectoryEntryStream, ref outputValue);
            flags = (byte)outputValue;
            tagIndex = (byte)(flags & 0x3f);
            transformationVersion = (byte)(flags >> 6);
            tag = Enum.GetValues(typeof(KnownTableTags)).Cast<KnownTableTags>().ToList()[tagIndex];
        }

        private bool IsKnownTableTag()
        {
            return (flags & 0x3f) != 0x3f;
        }

        private bool IsNullTransform()
        {
            if (tag != KnownTableTags.Glyf && tag != KnownTableTags.Loca)
                return transformationVersion == 0;
            else
                return transformationVersion == 3;
        }

        #endregion

        #region Overriden Methods

        public override string ToString()
        {
            return $"{nameof(Flags)}: {flags.ToString("X")}, {nameof(Tag)}: {tag}, {nameof(OrigLength)}: {origLength}, {nameof(TransformLength)}: {transformLength}";
        }

        #endregion
    }
}
