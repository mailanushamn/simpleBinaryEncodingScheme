namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Impl;
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using BinaryEncodingScheme.Utility;
    using System;

    /// <summary>
    /// Creates a packet in the format <DLE><Stx>|Headers|Payload|Checksum|<DLE><Etx>
    /// </summary>
    public class BinaryPacket :IBinaryPacket
    {
        public char STX { get; private set; }
        public char ETX { get; private set; }

        public char DLE { get; private set; }

        public IService _messageService;

        public BinaryPacket(
            IService service)

        {
            STX = PacketConstants.STX;
            ETX = PacketConstants.ETX;
            DLE = PacketConstants.DLE;
            _messageService = service;
        }

        /// <summary>
        /// Writes into stream in the order DLE,STX,CMD,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="outputStream"></param>
        public void Write(IDataOutputStream outputStream, Message message)
        {
            try
            {
                if (!_messageService.ValidateMessage(message))
                {
                    throw new CustomErrorException(400, "Validation failed.Invalid input.");
                }
                outputStream.Write(DLE);
                outputStream.Write(STX);
                _messageService.Write(outputStream, message);
                WriteChecksum(outputStream, message);
                outputStream.Write(DLE);
                outputStream.Write(ETX);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Reads from the stream in the order DLE,STX,CMD,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="inputStream"></param>
        public Message Read(IDataInputStream inputStream)
        {
            try
            {
                DLE = inputStream.ReadChar();
                STX = inputStream.ReadChar();
                var message = _messageService.Read(inputStream);
                var checksum = ReadChecksum(inputStream);
                ValidatePacket(message, checksum);
                DLE = inputStream.ReadChar();
                ETX = inputStream.ReadChar();
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Calculates checksum, writes the length of the checsum and checksum into the stream.
        /// </summary>
        /// <param name="outputStream"></param>
        private void WriteChecksum(IDataOutputStream outputStream, Message message)
        {
            try
            {
                if (message != null)
                {
                    var hash = ChecksumHelper.CalculateChecksum(message?.Payload);
                    outputStream.Write(hash.Length);
                    outputStream.Write(hash);
                }
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(400, "Error while writing checksum");
            }
        }

        /// <summary>
        /// Reads checksum length and cecksum from the stream.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        private byte[] ReadChecksum(IDataInputStream inputStream)
        {

            try
            {
                var checksumCount = inputStream.ReadInt32();
                return inputStream.ReadBytes(checksumCount);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(400, "Error while reading checksum");
            }
        }

        private void ValidatePacket(Message message, byte[] checksum)
        {
            if ((STX != PacketConstants.STX) || (ETX != PacketConstants.ETX) || (DLE != PacketConstants.DLE) || (message == null) || (checksum.Length == 0)||!(ChecksumHelper.ValidateChecksum(checksum, message.Payload)))
            {
                throw new CustomErrorException(400, "Message is corrupted!");
            }
            return;
        }
    }
}
