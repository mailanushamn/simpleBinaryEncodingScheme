using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// interface for reading from output stream
    /// </summary>
    public interface IDataOutputStream
    {
        void Write(char value);
        void Write(int length, string value);
        void Write(int value);
        void Write(short value);
        void Write(byte[] value);
        void Write(Dictionary<string,string> value);
    }
}
