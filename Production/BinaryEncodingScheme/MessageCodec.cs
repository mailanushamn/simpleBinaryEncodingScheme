namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Impl;
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using System;
    using System.IO;

    public class MessageCodec<T> : IEncoder<T>, IDecoder<T>  where T: Message
    {
        private IService<T> _service;
        public MessageCodec()
        {
            _service = new MessageService<T>();
        }
        
        /// <summary>
        /// Encodes any input object of type IMessage
        /// </summary>
        /// <param name="message"></param
        /// <returns></returns>
        public byte[] Encode(T message)
        {
            try
            {
                 
                var packet = new BinaryPacket<T>(_service);
                using (var stream = new MemoryStream())
                {
                    using (var dataWriter = new DataOutputStream(stream))
                    {
                        packet.Write(dataWriter,message);
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
               
                var packet = new BinaryPacket<T>( _service);
                using var stream = new MemoryStream(bytes);
                using (var inputStream = new DataInputStream(stream))
                {
                    var decodedData=packet.Read(inputStream);
                    return (T)decodedData;
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
