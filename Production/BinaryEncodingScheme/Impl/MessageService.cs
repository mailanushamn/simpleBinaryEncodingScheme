namespace BinaryEncodingScheme.Impl
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using BinaryEncodingScheme.Utility;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MessageService : IService
    {
        private int MaxPayloadSize = 256000;
        private int MaxHeaderSize = 1023;

        /// <summary>
        /// Returns the char representation of Message class.
        /// </summary>
        /// <returns></returns>
        public char GetObjectType()
        {
            return PacketIdentifierConstant.MessageRegistration;
        }

        /// <summary>
        /// Validates the message to be encoded.
        /// </summary>
        /// <returns></returns>
        public bool ValidateMessage(Message message)
        {
            if (message == null)
            { return false; }
            ValidateHeader(message.Headers);
            ValidatePayLoad(message.Payload);
            return true;
        }

        /// <summary>
        /// Reads the data from stream in the same written order.
        /// </summary>
        /// <param name="inputStream"></param>
        public Message Read(IDataInputStream inputStream)
        {
            try
            {
                var message = new Message();
                message.Headers= ReadHeaders(inputStream);
                message.Payload=ReadPayload(inputStream);
                return message;
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while decoding message:" + ex.Message);
            }
        }

        /// <summary>
        /// writes the object into stream in a order.First header count, headers,payload length and then payload
        /// </summary>
        /// <param name="outputStream"></param>
        public void Write(IDataOutputStream outputStream, Message message)
        {
            try
            {
                outputStream.Write(message.Headers.Count);
                outputStream.Write(message.Headers);
                outputStream.Write(message.Payload.Length);
                outputStream.Write(message.Payload);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while encoding message:" + ex.Message);
            }
        }
     
        private Dictionary<string,string> ReadHeaders(IDataInputStream inputStream)
        {
            var headerCount = inputStream.ReadInt32();
            return inputStream.ReadDictionary(headerCount);
        }

        private byte[] ReadPayload(IDataInputStream inputStream)
        {
            var payloadLength = inputStream.ReadInt32();
            return inputStream.ReadBytes(payloadLength);
        }
          
        private void ValidateHeader(Dictionary<string,string> headers)
        {
            if (headers.Count > 63)
            {
                throw new HeaderMaxLimitExceededException("Max allowed header limit is 63.");
            }
            foreach (var item in headers)
            {
                if (Encoding.UTF8.GetByteCount(item.Key) > MaxHeaderSize)
                {
                    throw new HeaderMaxSizeExceededException(item.Key + " exceeds max size of 1023 bytes");
                }
                if (Encoding.UTF8.GetByteCount(item.Value) > MaxHeaderSize)
                {
                    throw new HeaderMaxSizeExceededException(item.Value + " exceeds max size of 1023 bytes");
                }
            }
        }

        private void ValidatePayLoad(byte[] payload)
        {
            if (payload.Length > MaxPayloadSize)
            {
                throw new PayloadMaxSizeExceededException("Payload max limit of 256KB is exceeded");
            }
        }
       
    }
}
