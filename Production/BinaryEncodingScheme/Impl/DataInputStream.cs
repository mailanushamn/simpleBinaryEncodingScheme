namespace BinaryEncodingScheme.Impl
{
    using BinaryEncodingScheme.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    public class DataInputStream : IDataInputStream, IDisposable
    {
        private readonly BinaryReader _reader;

        public DataInputStream(Stream stream)
        {
            _reader = new BinaryReader(stream, System.Text.Encoding.UTF8);
        }

        public string ReadString()
        {
            var count = _reader.ReadInt32();
            return new string(_reader.ReadChars(count));
        }

        public char ReadChar()
        {
            return _reader.ReadChar();
        }

        public int ReadInt32()
        {
            return _reader.ReadInt32();
        }

        public short ReadIn16()
        {
            return _reader.ReadInt16();
        }

        public byte[] ReadBytes(int count)
        {
            return _reader.ReadBytes(count);
        }
        public Dictionary<string, string> ReadDictionary(int count)
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < count; i++)
            {                            
                var key = ReadString();              
                var value = ReadString();
                dict.Add(key, value);            
            }
            return dict;          
        }
        public void Dispose()
        {
            _reader.Dispose();
        }

    }
}
