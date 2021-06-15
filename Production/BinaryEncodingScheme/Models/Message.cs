namespace BinaryEncodingScheme.Models
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Utility;
    using System.Collections.Generic;
    using System.Text;

    public class Message : IMessage
    {
     
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Payload { get; set; }

        private byte[] CheckSum;

        public bool ValidateBeforeEncoding()
        {
            ValidateHeader();
            ValidatePayLoad();
            return true;
        }

        public void Read(IDataInputStream inputStream)
        {
            ReadHeaders(inputStream);
            ReadPayload(inputStream);
        }

        public void Write(IDataOutputStream outputStream)
        {
            WriteHeaders(outputStream);
            WritePaylaod(outputStream);            
        }
        public void WriteChecksum(IDataOutputStream outputStream)
        {
            var hash = Helper.CalculateChecksum(Payload);
            outputStream.Write(hash.Length);
            outputStream.Write(hash);
        }
        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            var checksumCount = inputStream.ReadInt32();
            CheckSum= inputStream.ReadBytes(checksumCount);
            return CheckSum;
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
                if (Encoding.UTF8.GetByteCount(item.Key) > 1023)
                {
                    throw new HeaderMaxSizeExceededException(item.Key + " exceeds max size of 1023 bytes");
                }
                if (Encoding.UTF8.GetByteCount(item.Value) > 1023)
                {
                    throw new HeaderMaxSizeExceededException(item.Value + " exceeds max size of 1023 bytes");
                }
            }
        }
        private void ValidatePayLoad()
        {
            if (Payload.Length > 256000)
            {
                throw new PayloadMaxSizeExceededException("Payload max limit of 256KB is exceeded");
            }
        }

        public bool ValidateAfterDecoding()
        {
           return Helper.ValidateChecksum(CheckSum, Payload);
        }
    }
}
