namespace RiftRipperLib.DAT1;

public class Section
{
    public readonly uint id;
    public uint offset;
    public uint length;
    public uint End { get { return offset + length; } }

    public Section(StreamHelper filestream)
    {
        Console.WriteLine($"Reading DAT1 section at offset {filestream.Position:X}");
        id = filestream.ReadUInt32(0x00);
        Console.WriteLine($"DAT1 Section - ID: {id:X}");
        offset = filestream.ReadUInt32(0x04);
        Console.WriteLine($"DAT1 Section - Offset: {offset:X} ({offset})");
        length = filestream.ReadUInt32(0x08);
        Console.WriteLine($"DAT1 Section - Length: {length:X} ({length})");
        Console.WriteLine($"DAT1 Section - End: {End:X} ({End})");
    }
}
