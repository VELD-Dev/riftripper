using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

public class StreamHelper : BinaryReader
{
    public enum Endianness
    {
        Little,
        Big,
    }

    public Endianness _endianness = Endianness.Little;
    public byte bitPosition = 0x00;

    public StreamHelper(Stream input) : base(input) { }

    public StreamHelper(Stream input, Encoding encoding) : base(input, encoding) { }

    public StreamHelper(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen) { }

    public StreamHelper(Stream input, Endianness endianness) : base(input)
    {
        _endianness = endianness;
    }

    public StreamHelper(Stream input, Encoding encoding, Endianness endianness) : base(input, encoding)
    {
        _endianness = endianness;
    }

    public StreamHelper(Stream input, Encoding encoding, bool leaveOpen, Endianness endianness) : base(input, encoding, leaveOpen)
    {
        _endianness = endianness;
    }


    public void Seek(long offset, SeekOrigin origin = SeekOrigin.Begin) => BaseStream.Seek(offset, origin);

    public override string ReadString()
    {
        var sb = new StringBuilder();
        while(true)
        {
            var nextByte = ReadByte();
            if (nextByte == 0x00) break;
            sb.Append(nextByte);
        }
        return sb.ToString();
    }

    public string ReadUnicodeString()
    {
        var sb = new StringBuilder();
        while(true)
        {
            byte nextByte;
            byte nextByte2;

            try
            {
                nextByte = ReadByte();
                nextByte2 = ReadByte();
            }
            catch(EndOfStreamException)
            {
                break;
            }

            if (nextByte == 0x00 && nextByte2 == 0x00) break;
            string convertedChar;
            if (_endianness == Endianness.Big)
                convertedChar = Encoding.Unicode.GetString(new byte[] { nextByte2, nextByte });
            else
                convertedChar = Encoding.Unicode.GetString(new byte[] { nextByte, nextByte2 });
            sb.Append(convertedChar);
        }
        return sb.ToString();
    }

    public string ReadUnicodeString(uint size)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < size; i++)
        {
            byte newByte;
            byte newByte2;

            try
            {
                newByte = ReadByte();
                newByte2 = ReadByte();
            }
            catch (EndOfStreamException)
            {
                break;
            }
            if (newByte == 0x00 && newByte2 == 0x00) break;
            string convertedChar;
            if (_endianness == Endianness.Big) convertedChar = Encoding.Unicode.GetString(new byte[] { newByte2, newByte });
            else convertedChar = Encoding.Unicode.GetString(new byte[] { newByte, newByte2 });
            sb.Append(convertedChar);
        }
        return sb.ToString();
    }

    public new byte ReadByte() => ReadByte((uint)BaseStream.Position);
    public byte ReadByte(uint offset)
    {
        BaseStream.Seek(offset, SeekOrigin.Begin);
        byte[] buffer = new byte[1];
        BaseStream.Read(buffer, 0x00, 0x01);
        return buffer[0];
    }

    public string ReadString(uint offset)
    {
        BaseStream.Seek(offset, SeekOrigin.Begin);
        return ReadString();
    }

    public byte[] ReadBytes(uint count) => ReadBytes((int)count);
    public byte[] ReadFromOffset(int count, uint offset)
    {
        BaseStream.Seek(ofset, SeekOrigin.Begin);
    }
}
