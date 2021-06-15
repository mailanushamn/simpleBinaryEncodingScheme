namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Utility;
    using System;

    public class Packet : IReader, IWriter
    {
        public char Stx { get; private set; }
        public char Etx { get; private set; }
        public byte[] CheckSum { get; private set; }
        public IMessage Data { get; private set; }

        public Packet(
            IMessage data)
            : this()
        {
            Data = data;
        }

        public Packet()
        {
            
            Stx = PacketConstants.Stx;
            Etx = PacketConstants.Etx;
        }

        public void Write(IDataOutputStream outputStream)
        {
            try
            {
               
                Data.ValidateBeforeEncoding();
                outputStream.Write(Stx);
                WriteMessageData(outputStream);
                WriteChecksum(outputStream);
                outputStream.Write(Etx);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(400, "Error while encoding message."+ ex.Message);
            }
        }

        public void Read(IDataInputStream inputStream)
        {
            try
            {
                Stx = inputStream.ReadChar();
                ReadMessageData(inputStream);
                ReadChecksum(inputStream);
                ValidatePacket();
                Etx = inputStream.ReadChar();
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(400,"Error while decoding."+ ex.Message);
            }
        }

        private void WriteMessageData(IDataOutputStream outputStream)
        {
            Data.Write(outputStream);
        }

        private void WriteChecksum(IDataOutputStream outputStream)
        {
            Data.WriteChecksum(outputStream);
        }

        private void ReadMessageData(IDataInputStream inputStream)
        {
            Data.Read(inputStream);
            
        }

        private void ReadChecksum(IDataInputStream inputStream)
        {
            CheckSum = Data.ReadChecksum(inputStream);
        }

        private void ValidatePacket()
        {
            if (Stx != PacketConstants.Stx || Etx != PacketConstants.Etx || !Data.ValidateAfterDecoding())
            {
                throw new CustomErrorException(400, "Message is corrupted!");
            }
            return;
        }
    }
}
