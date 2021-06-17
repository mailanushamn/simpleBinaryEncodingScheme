namespace BinaryEncodingApp.Models
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Utility;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Message : IMessage
    {   
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Payload { get; set; }
        private byte[] CheckSum;

        private int MaxPayloadSize = 256000;
        private int MaxHeaderSize = 1023;

        /// <summary>
        /// Returns the char representation of Message class.
        /// </summary>
        /// <returns></returns>
        public char GetObjectType()
        {
            return PacketCommandConstant.MessageRegistration;
        }

        /// <summary>
        /// Validates the message to be encoded.
        /// </summary>
        /// <returns></returns>
        public bool ValidateBeforeEncoding()
        {
            ValidateHeader();
            ValidatePayLoad();
            return true;
        }

        /// <summary>
        /// Reads the data from stream in the same written order.
        /// </summary>
        /// <param name="inputStream"></param>
        public void Read(IDataInputStream inputStream)
        {
            try
            {
                ReadHeaders(inputStream);
                ReadPayload(inputStream);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while encoding message:" + ex.Message);
            }
        }

        /// <summary>
        /// writes the object into stream in a order.First header count, headers,payload length and then payload
        /// </summary>
        /// <param name="outputStream"></param>
        public void Write(IDataOutputStream outputStream)
        {
            try
            {
                WriteHeaders(outputStream);
                WritePaylaod(outputStream);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while decoding message:" + ex.Message);
            }
        }

        /// <summary>
        /// Calculates checksum, writes the length of the checsum and checksum into the stream.
        /// </summary>
        /// <param name="outputStream"></param>
        public void WriteChecksum(IDataOutputStream outputStream)
        {
            var hash = Helper.CalculateChecksum(Payload);
            outputStream.Write(hash.Length);
            outputStream.Write(hash);
        }

        /// <summary>
        /// Reads checksum length and cecksum from the stream.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            var checksumCount = inputStream.ReadInt32();
            CheckSum= inputStream.ReadBytes(checksumCount);
            return CheckSum;
        }

        /// <summary>
        /// Validated the decoded message using checksum to ensure accuracy of the transmitted data.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAfterDecoding()
        {
            return Helper.ValidateChecksum(CheckSum, Payload);
        }
        private void ReadHeaders(IDataInputStream inputStream)
        {
            var headerCount = inputStream.ReadInt32();
            Headers = inputStream.ReadDictionary(headerCount);
        }
        private void ReadPayload(IDataInputStream inputStream)
        {
            var dataCount = inputStream.ReadInt32();
            Payload = inputStream.ReadBytes(dataCount);
        }
        private void WriteHeaders(IDataOutputStream outputStream)
        {
            outputStream.Write(Headers.Count);
            outputStream.Write(Headers);
        }
        private void WritePaylaod(IDataOutputStream outputStream)
        {
            outputStream.Write(Payload.Length);
            outputStream.Write(Payload);
        }
        private void ValidateHeader()
        {
            if (Headers.Count > 63)
            {
                throw new HeaderMaxLimitExceededException("Max allowed header limit is 63.");
            }
            foreach (var item in Headers)
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
        private void ValidatePayLoad()
        {
            if (Payload.Length > MaxPayloadSize)
            {
                throw new PayloadMaxSizeExceededException("Payload max limit of 256KB is exceeded");
            }
        }
       
    }
}
