namespace BinaryEncodingScheme.Models
{
    using BinaryEncodingScheme.Interfaces;
    using System.Collections.Generic;
    public class Message : IReader, IWriter
    {
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Payload { get; set; }

        public void Read(IDataInputStream inputStream)
        {
            throw new System.NotImplementedException();
        }

        public void Write(IDataOutputStream outputStream)
        {
            outputStream.Write(Headers.Count);
            outputStream.Write(Headers);
            outputStream.Write(Payload.Length);
            outputStream.Write(Payload);
        }
    }
}
