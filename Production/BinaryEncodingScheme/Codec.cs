namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Impl;
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using System.IO;

    public class BinaryCodec: IEncoder, IDecoder
    {
      
        public byte[] Encode(Message t)
        {
            var packet = new Packet(t);
            using (var stream = new MemoryStream())
            {
                using (var dataWriter = new DataOutputStream(stream))
                {
                    packet.Write(dataWriter);
                    return stream.ToArray();
                }
            }
        }

        public Message Decode(byte[] bytes)
        {
            var packet = new Packet();
            using var stream = new MemoryStream(bytes);
            using (var inputStream = new DataInputStream(stream))
            {
                packet.Read(inputStream);
                return packet.Message;
            }
        }

    }
}
