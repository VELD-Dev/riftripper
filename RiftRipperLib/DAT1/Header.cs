using RiftRipperLib.DAT1.Sections;
using System.Reflection.PortableExecutable;
using System.Text;

namespace RiftRipperLib.DAT1;

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
    public Dictionary<uint, Section> sections;

    public Header(StreamHelper filestream)
    {
        magic = filestream.Peek(0x04, 0x00);
        string magicString = Encoding.ASCII.GetString(magic);
        Console.WriteLine("Magic read for DAT1 Header.");
        if (magicString != "1TAD")
            throw new IOException($"The stream is not reading the right place: The magic is incorrect: found '{magicString}' instead of '1TAD'. ({BitConverter.ToString(magic)})");

        id = filestream.ReadUInt32(0x04);
        Console.WriteLine($"DAT1 Header - ID: {id:X}");
        length = filestream.ReadUInt32(0x08);
        Console.WriteLine($"DAT1 Header - Length: {length} ({length:X})");
        sections_count = filestream.ReadUInt32(0x0C);
        Console.WriteLine($"DAT1 Header - Sections count: {sections_count} ({sections_count:X})");
        sections = new();
        filestream.Seek(0x10);
        for (int i = 0; i < sections_count; i++)
        {
            Section section = new(filestream);
            TryAddSection(sections, section);
            filestream.Jump(0x0C);
        }
    }

    public static bool TryAddSection(Dictionary<uint, Section> sectionsDict, Section section)
    {
        if (sectionsDict.ContainsKey(section.id))
            return false;

        switch(section.id)
        {
            case (uint)DAT1Reader.SectionHashes.ArchiveFileSection:
                sectionsDict.Add(section.id, (ArchiveFileSection)section);
            break;
            default:
                sectionsDict.Add(section.id, section);
            break;
        }
        Console.WriteLine($"Added section {section.id:X} to sections dictionary.");
        return true;
    }
}
