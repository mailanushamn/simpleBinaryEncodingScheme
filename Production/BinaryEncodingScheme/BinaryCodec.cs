namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Impl;
    using BinaryEncodingScheme.Interfaces;
    using System;
    using System.IO;

    public class BinaryCodec<T> : IEncoder<T>, IDecoder<T>  where T: IMessage
    {
        
        /// <summary>
        /// Encodes any input object of type IMessage
        /// </summary>
        /// <param name="message"></param
        /// <returns></returns>
        public byte[] Encode(T message)
        {
            try
            {
                var packet = new BinaryPacket(message);
                using (var stream = new MemoryStream())
                {
                    using (var dataWriter = new DataOutputStream(stream))
                    {
                        packet.Write(dataWriter);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Decodes bytes into any output object of type IMessage
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public T Decode(byte[] bytes)
        {
            try
            {
                var message = Activator.CreateInstance<T>();
                var packet = new BinaryPacket(message);
                using var stream = new MemoryStream(bytes);
                using (var inputStream = new DataInputStream(stream))
                {
                    packet.Read(inputStream);
                    return (T)packet.Data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }

       
    }
    
}
