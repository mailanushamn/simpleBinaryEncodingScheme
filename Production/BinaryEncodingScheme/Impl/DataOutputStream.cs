using BinaryEncodingScheme.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryEncodingScheme.Impl
{
    public class DataOutputStream : IDataOutputStream, IDisposable
    {
        private readonly BinaryWriter _writer;

        public DataOutputStream(Stream stream)
        {
            _writer = new BinaryWriter(stream);
        }

        public void Write(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            Write(bytes);
        }

        public void Write(byte value)
        {
            Write(new byte[] { value });
        }

        public void Write(char value)
        {
            // In this protocol a char represents one byte however GetBytes of bitconverter treats the byte as unicode
            var bytes = BitConverter.GetBytes(value);
            Write(new byte[] { bytes[0] });
        }

        public void Write(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            Write(bytes);
        }

        public void Write(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            Write(bytes);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        public void Write(byte[] value)
        {
            WriteBytes(value);
        }

        private void WriteBytes(byte[] bytes)
        {
            _writer.Write(bytes);
        }

        public void Write(Dictionary<string, string> values)
        {
            foreach (var item in values)
            {
                //length of key - key - length of value - value
                var keyLenBytes = BitConverter.GetBytes(item.Key.Length);
                WriteBytes(keyLenBytes);
                Write(item.Key);
                var valLenBytes = BitConverter.GetBytes(item.Value.Length);
                WriteBytes(valLenBytes);
                Write(item.Value);
            }
        }

    }
}
