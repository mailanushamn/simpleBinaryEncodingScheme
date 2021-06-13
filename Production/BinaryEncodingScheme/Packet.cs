namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Models;
    using BinaryEncodingScheme.Utility;
    using System;

    public class Packet : IReader, IWriter
    {
        public char Stx { get; private set; }
        public char Etx { get; private set; }
        public byte[] CheckSum { get; private set; }
        public Message Message { get; private set; }

        public Packet(
            Message message)
            : this()
        {
            Message = message;
        }

        public Packet()
        {
            Message = new Message();
            Stx = PacketConstants.Stx;
            Etx = PacketConstants.Etx;
        }

        public void Write(IDataOutputStream outputStream)
        {
            try
            {
                outputStream.Write(Stx);
                WritePacketData(outputStream);
                WriteChecksum(outputStream);
                outputStream.Write(Etx);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(400, "Failed to encode message!");
            }
        }

        public void Read(IDataInputStream inputStream)
        {
            try
            {
                Stx = inputStream.ReadChar();
                ReadPacketData(inputStream);
                ReadChecksum(inputStream);
                Etx = inputStream.ReadChar();
            }
            catch (Exception ex)
            {

            }
        }

        private void WritePacketData(IDataOutputStream outputStream)
        {
            outputStream.Write(Message.Headers.Count);
            outputStream.Write(Message.Headers);
            outputStream.Write(Message.Payload.Length);
            outputStream.Write(Message.Payload);
        }

        private void WriteChecksum(IDataOutputStream outputStream)
        {
            var hash = Helper.CalculateChecksum(Message.Payload);
            outputStream.Write(hash.Length);
            outputStream.Write(hash);
        }

        private void ReadPacketData(IDataInputStream inputStream)
        {
            var headerCount = inputStream.ReadInt32();
            Message.Headers = inputStream.ReadDictionary(headerCount);
            var dataCount = inputStream.ReadInt32();
            Message.Payload = inputStream.ReadBytes(dataCount);
            ValidatePacket();
        }

        private void ReadChecksum(IDataInputStream inputStream)
        {
            var checksumCount = inputStream.ReadInt32();
            CheckSum = inputStream.ReadBytes(checksumCount);
        }
        public void ValidatePacket()
        {
            if (Stx != PacketConstants.Stx || Etx != PacketConstants.Etx || !Helper.ValidateChecksum(CheckSum, Message.Payload))
            {
                throw new CustomErrorException(400, "Message is corrupted!");
            }
            return;
        }
    }
}
