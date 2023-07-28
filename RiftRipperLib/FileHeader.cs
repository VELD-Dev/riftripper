using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;


public struct ITADSection
{
    public uint hash;
    public uint offset;
    public uint length;

    public ITADSection(Stream stream, uint offset)
    {
        this.hash = 
    }
}


/// <summary>
/// 0x10 bytes long
/// </summary>
public class TADFileHeader
{
    /// <summary>
    /// Offset of the header. Technically 0.
    /// </summary>
    public required long offset;

    /// <summary>
    /// 0x04 bytes long
    /// <para>1TAD</para>
    /// </summary>
    public char[] magic = new char[4] { '1', 'T', 'A', 'D' }; // 1TAD

    /// <summary>
    /// 0x04 bytes long
    /// </summary>
    public required uint hash;

    /// <summary>
    /// 0x04 bytes long
    /// </summary>
    public required uint filesize;

    /// <summary>
    /// 0x02 bytes long
    /// </summary>
    public required ushort sections_count;

    /// <summary>
    /// 0x02 bytes long
    /// </summary>
    public required ushort unknown;

    public static TADFileHeader Read(FileStream stream)
    {
        byte[] magic = new byte[4];
        byte[] hash = new byte[4];
        byte[] filesize = new byte[4];
        byte[] sections_count = new byte[2];
        byte[] unk = new byte[2];
        long offset = stream.Position;

        stream.Seek(0x00, SeekOrigin.Begin);
        stream.Read(magic, 0x0, 0x04);

        stream.Seek(0x04, SeekOrigin.Current);
        stream.Read(hash, 0x0, 0x04);

        stream.Seek(0x04, SeekOrigin.Current);
        stream.Read(filesize, 0x0, 0x04);

        stream.Seek(0x04, SeekOrigin.Current);
        stream.Read(sections_count, 0x0, 0x02);

        stream.Seek(0x02, SeekOrigin.Current);
        stream.Read(unk, 0x0, 0x02);


        var res = new TADFileHeader()
        {
            offset = offset,
            hash = BitConverter.ToUInt32(hash),
            filesize = BitConverter.ToUInt32(filesize),
            sections_count = BitConverter.ToUInt16(sections_count),
            unknown = BitConverter.ToUInt16(unk)
        };

        var magicCharArray = Encoding.ASCII.GetString(magic).ToCharArray();
        if (res.magic != magicCharArray)
            throw new IOException($"The magic of the 1TAD IGFile is invalid. {res.magic} is not equal to {magicCharArray}");

        return res;
    }
}
