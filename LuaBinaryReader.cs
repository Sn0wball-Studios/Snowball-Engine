using System;
using System.IO;
namespace Snowball
{
    public class LuaBinaryReader
    {
        BinaryReader reader;
        public LuaBinaryReader(byte[] data)
        {
            reader = new BinaryReader(new MemoryStream(data));
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public byte[] ReadBytes(int length)
        {
            return reader.ReadBytes(length);
        }

        public UInt32 ReadUInt32()
        {
            return reader.ReadUInt32();
        }

        public UInt16 ReadUInt16()
        {
            return reader.ReadUInt16();
        }

        public long Length
        {
            get
            {
                return reader.BaseStream.Length;
            }
        }

        public long Offset
        {
            get
            {
                return reader.BaseStream.Position;
            }
            set
            {
                reader.BaseStream.Position = value;
            }
        }
    

    }
}