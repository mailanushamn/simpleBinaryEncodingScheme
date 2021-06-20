namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Impl;
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using BinaryEncodingScheme.Utility;
    using System;

    /// <summary>
    /// Creates a packet in the format <DLE><Stx>|Identifier|Headers|Payload|Checksum|<DLE><Etx>
    /// </summary>
    internal class BinaryPacket<T> : IReader<T>, IWriter<T> 
    {
        public char STX { get; private set; }
        public char ETX { get; private set; }

        public char DLE { get; private set; }

        public IService<T> _messageService;

        public BinaryPacket(
            IService<T> service)

        {
            STX = PacketConstants.STX;
            ETX = PacketConstants.ETX;
            DLE = PacketConstants.DLE;
            _messageService = service;
        }

        /// <summary>
        /// Writes into stream in the order DLE,STX,Identifier,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="outputStream"></param>
        public void Write(IDataOutputStream outputStream, T message)
        {
            try
            {
                if (!_messageService.ValidateMessage(message))
                {
                    throw new CustomErrorException(400, "Validation failed.Invalid input.");
                }
                outputStream.Write(DLE);
                outputStream.Write(STX);
                var identifier = _messageService.GetObjectType();
                outputStream.Write(identifier);
                _messageService.Write(outputStream, message);
                _messageService.WriteChecksum(outputStream, message);
                outputStream.Write(DLE);
                outputStream.Write(ETX);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Reads from the stream in the order DLE,STX,Identifier,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="inputStream"></param>
        public T Read(IDataInputStream inputStream)
        {
            try
            {
                DLE = inputStream.ReadChar();
                STX = inputStream.ReadChar();
                var identifier = inputStream.ReadChar();
                var message = _messageService.Read(inputStream);
                var checksum = _messageService.ReadChecksum(inputStream);
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
     
        private void ValidatePacket(T message, byte[] checksum)
        {
            if ((STX != PacketConstants.STX) || (ETX != PacketConstants.ETX) || (DLE != PacketConstants.DLE) || (message == null) || (checksum.Length == 0)||!(_messageService.ValidateChecksum(checksum, message)))
            {
                throw new CustomErrorException(400, "Message is corrupted!");
            }
            return;
        }
    }
}
