using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

public class DAT1Reader
{
    public struct Header
    {
        //0x08 unknown bytes

        /// <summary>
        /// 0x04 Magic "DAT1" or "1TAD" if not reversed.
        /// </summary>
        public byte[] magic = new byte[4];

        /// <summary>
        /// In reality, it's a CRC, but in the modding context it's just a "section ID".
        /// </summary>
        public uint id;

        /// <summary>
        /// File length in bytes.
        /// </summary>
        public uint length;

        /// <summary>
        /// Count of sections in the file.
        /// </summary>
        public uint sections_count;

        /// <summary>
        /// The sections of the file.
        /// </summary>
        public Section[] sections;

        public Header(StreamHelper filestream)
        {
            filestream.Seek(0x0);
            magic           =   filestream.Peek(0x04, 0x00);
            id              =   filestream.ReadUInt32(0x04);
            length          =   filestream.ReadUInt32(0x08);
            sections_count  =   filestream.ReadUInt32(0x0C);
            sections = new Section[sections_count];
        }
    }

    public class Section
    {
        public uint id;
        public uint offset;
        public uint length;
        public uint End => offset + length;
    }

    public class ArchiveFileSection : Section
    {
        public string[] paths;
    }

    public static readonly Dictionary<Type, long> SectionHashes = new()
    {
        { typeof(ArchiveFileSection),   0x398ABFF0 },
        { typeof(ArchiveFileSection),   0x506D7B8A },
        { typeof(ArchiveFileSection),   0x65BCF461 },
        { typeof(ArchiveFileSection),   0xD101A6CC },
        { typeof(ArchiveFileSection),   0xEDE8ADA9 },
    };
}
