namespace BinaryEncodingScheme.Interfaces
{
    using System.Collections.Generic;
    /// <summary>
    /// Interface for reading from the stream
    /// </summary>
    public interface IDataInputStream
    {
        string ReadString();
        char ReadChar();
        int ReadInt32();
        short ReadIn16();
        byte[] ReadBytes(int count);
        Dictionary<string, string> ReadDictionary(int count);
    }
}
