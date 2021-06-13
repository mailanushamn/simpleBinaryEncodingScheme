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

        public string ReadString(int count)
        {
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
                //ToDo: 1024 bytes for key or value so check if count can by 1=int 16 or iint32
                var keycount = ReadInt32();
                var key = ReadString(keycount);
                var valCount = ReadInt32();
                var value = ReadString(valCount);
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
