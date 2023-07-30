using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

public class StreamHelper : BinaryReader
{
    public long Position { get { return BaseStream.Position; } set { BaseStream.Position = value; } }

    public StreamHelper(Stream input) : base(input) { }

    public StreamHelper(Stream input, Encoding encoding) : base(input, encoding) { }

    public StreamHelper(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

    public new byte ReadByte() => ReadByte((uint)BaseStream.Position);

    public byte[] Read(int size)
    {
        var buffer = new byte[size];
        BaseStream.Read(buffer, (int)Position, size);
        return buffer;
    }

    public byte[] Read(int size, long offset, bool relative = true)
    {
        var buffer = new byte[size];
        BaseStream.Read(buffer, (int)offset, size);
        return buffer;
    }

    /// <summary>
    /// Seek, moves, to the offset in the stream. It's absolute.
    /// </summary>
    /// <param name="offset">Offset where you want to go in the stream.</param>
    public void Seek(long offset) => BaseStream.Seek(offset, SeekOrigin.Begin);

    /// <summary>
    /// Jumps from the current position to current position + size
    /// </summary>
    /// <param name="offset"></param>
    public void Jump(int size) => BaseStream.Seek(size, SeekOrigin.Current);

    /// <summary>
    /// Reads, but without changing the position in the stream. It just peeks at offset or peek the next byte.
    /// </summary>
    /// <param name="size">Amount of bytes to read.</param>
    /// <param name="offset">Offset where you want to peek.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>Buffer of peeked bytes.</returns>
    public byte[] Peek(int size = 0x0, long offset = 0x0, bool relative = true)
    {
        var previous_offset = Position;
        byte[] buffer = Read(size, offset, relative);
        Seek(previous_offset);
        return buffer;
    }

    /// <summary>
    /// Also called Boolean. Bool is a two state value, it can be either 1 (true), or 0 (false).
    /// </summary>
    /// <param name="offset">Offset to the boolean.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The boolean.</returns>
    public bool ReadBool(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x1, offset, relative);
        return BitConverter.ToBoolean(buffer, 0);
    }

    /// <summary>
    /// Also called uint. UInt32 is a 32 bits long unsigned integer.
    /// </summary>
    /// <param name="offset">Offset to the uint.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The unsigned integer.</returns>
    public uint ReadUInt32(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x04, offset, relative);
        return BitConverter.ToUInt32(buffer, 0);
    }

    /// <summary>
    /// Also called ushort. UInt16 is a 16 bits long unsigned integer.
    /// </summary>
    /// <param name="offset">Offset to the ushort.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The unsigned short integer.</returns>
    public ushort ReadUInt16(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x02, offset, relative);
        return BitConverter.ToUInt16(buffer, 0);
    }

    /// <summary>
    /// Also called ulong. UInt64 is a 64 bits long unsigned integer.
    /// </summary>
    /// <param name="offset">Offset to the ulong.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The unsigned long integer.</returns>
    public ulong ReadUInt64(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x08, offset, relative);
        return BitConverter.ToUInt64(buffer, 0);
    }

    /// <summary>
    /// Byte is a 8 bits long unsigned integer.
    /// </summary>
    /// <param name="offset">Offset to the byte</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The unsigned integer.</returns>
    public byte ReadByte(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x1, offset, relative);
        return buffer[0];
    }

    /// <summary>
    /// Also called int. Int32 is a 32 bits long signed integer.
    /// </summary>
    /// <param name="offset">Offset to the int.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The signed integer.</returns>
    public int ReadInt32(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x04, offset, relative);
        return BitConverter.ToInt32(buffer, 0);
    }

    /// <summary>
    /// Also called short. Int16 is a 16 bits long signed integer.
    /// </summary>
    /// <param name="offset">Offset to the short.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The signed integer.</returns>
    public short ReadInt16(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x02, offset, relative);
        return BitConverter.ToInt16(buffer, 0);
    }

    /// <summary>
    /// Also called long. Int64 is a 64 bits long signed integer.
    /// </summary>
    /// <param name="offset">Offset to the long.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The signed integer.</returns>
    public long ReadInt64(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x08, offset, relative);
        return BitConverter.ToInt64(buffer, 0);
    }

    /// <summary>
    /// Also called a single. Float32 is a 32 bits long floating number.
    /// </summary>
    /// <param name="offset">Offset to the float.</param>
    /// <param name="relative">Wether this offset is relative or absolute.</param>
    /// <returns>The floating number.</returns>
    public float ReadFloat32(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x04, offset, relative);
        return BitConverter.ToSingle(buffer, 0);
    }

    /// <summary>
    /// Also called double. Float64 is a 64 bits long floating number.
    /// </summary>
    /// <param name="offset">Offset to the double.</param>
    /// <param name="relative">Wether the offset is relative or absolute.</param>
    /// <returns>The floating number.</returns>
    public double ReadFloat64(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x08, offset, relative);
        return BitConverter.ToDouble(buffer, 0);
    }

    /// <summary>
    /// Also called half. Float16 is a 16 bits long floating number.
    /// </summary>
    /// <param name="offset">Offset to the half.</param>
    /// <param name="relative">Wether the offset is relative or absolute.</param>
    /// <returns>The floating number.</returns>
    public Half ReadFloat16(long offset = 0x0, bool relative = true)
    {
        byte[] buffer = Peek(0x02, offset, relative);
        return BitConverter.ToHalf(buffer, 0);
    }

    /// <summary>
    /// Reads a string of size <c>size</c> or reads until there's no string to read anymore (until it sees a <c>0x00</c> byte)
    /// </summary>
    /// <param name="size">Size of the string. Leave it at -1 if you want to read a string until it sees a byte <c>0x00</c>.</param>
    /// <param name="offset">Offset where it should read the string.</param>
    /// <param name="relative">Wether this offset should be relative or absolute.</param>
    /// <returns>The string decoded as ANSI.</returns>
    public string ReadString(long offset = 0x0, bool relative = true, int size = -1)
    {
        var sb = new StringBuilder();

        if(size > 0)
            for(int i = 0; i < size; i++)
            {
                var nextByte = Peek(
                    offset: offset + i,
                    relative: relative
                    )[0];
                if (nextByte == 0x0) break;
                sb.Append(nextByte);
            }
        else
            for(int i = 0; Peek(0x1, offset + i, relative)[0] != 0x0; i++)
            {
                var nextByte = Peek(
                    offset: offset + i,
                    relative: relative
                    )[0];
                if (nextByte == 0x00) break;
                sb.Append(nextByte);
            }

        return sb.ToString();
    }

    public Vector2 ReadVector2Float32(long offset = 0x0, bool relative = true)
    {
        float x = ReadFloat32(offset + 0x00, relative);
        float y = ReadFloat32(offset + 0x04, relative);
        return new Vector2(x, y);
    }

    public Vector2 ReadVector2Float16(long offset = 0x0, bool relative = true)
    {
        Half x = ReadFloat16(offset + 0x00, relative);
        Half y = ReadFloat16(offset + 0x02, relative);
        return new Vector2((float)x, (float)y);
    }

    public Vector2 ReadVector2Float64(long offset = 0x0, bool relative = true)
    {
        double x = ReadFloat64(offset + 0x00, relative);
        double y = ReadFloat64(offset + 0x08, relative);
        return new Vector2((float)x, (float)y);
    }

    public Vector3 ReadVector3Float32(long offset = 0x0, bool relative = true)
    {
        float x = ReadFloat32(offset + 0x00, relative);
        float y = ReadFloat32(offset + 0x04, relative);
        float z = ReadFloat32(offset + 0x08, relative);
        return new Vector3(x, y, z);
    }

    public Vector3 ReadVector3Float16(long offset = 0x0, bool relative = true)
    {
        Half x = ReadFloat16(offset + 0x00, relative);
        Half y = ReadFloat16(offset + 0x02, relative);
        Half z = ReadFloat16(offset + 0x04, relative);
        return new Vector3((float)x, (float)y, (float)z);
    }

    public Vector3 ReadVector3Float64(long offset = 0x0, bool relative = true)
    {
        double x = ReadFloat64(offset + 0x00, relative);
        double y = ReadFloat64(offset + 0x08, relative);
        double z = ReadFloat64(offset + 0x10, relative);
        return new Vector3((float)x, (float)y, (float)z);
    }

    public Matrix4x4 ReadMatrix4x4(long offset = 0x0, bool relative = true)
    {
        return new Matrix4x4(
            ReadFloat32(offset+0x00, relative), ReadFloat32(offset+0x10, relative), ReadFloat32(offset+0x20, relative), ReadFloat32(offset+0x30, relative),
            ReadFloat32(offset+0x04, relative), ReadFloat32(offset+0x14, relative), ReadFloat32(offset+0x24, relative), ReadFloat32(offset+0x34, relative),
            ReadFloat32(offset+0x08, relative), ReadFloat32(offset+0x18, relative), ReadFloat32(offset+0x28, relative), ReadFloat32(offset+0x38, relative),
            ReadFloat32(offset+0x0C, relative), ReadFloat32(offset+0x1C, relative), ReadFloat32(offset+0x2C, relative), ReadFloat32(offset+0x3C, relative)
        );
    }
}
