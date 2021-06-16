namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Utility;
    using System;

    /// <summary>
    /// Creates a packet in the format <DLE><Stx>|CMD|Headers|Payload|Checksum|<DLE><Etx>
    /// </summary>
    public class Packet : IReader, IWriter
    {
        public char STX { get; private set; }
        public char ETX { get; private set; }

        public char DLE { get; private set; }
        public byte[] CheckSum { get; private set; }
        public IMessage Data { get; private set; }

        public char CMD { get; private set; }

        public Packet(
            IMessage data)
            : this()
        {
            Data = data;
        }

        public Packet()
        {         
            STX = PacketConstants.STX;
            ETX = PacketConstants.ETX;
            DLE = PacketConstants.DLE;           
        }

        public void Write(IDataOutputStream outputStream)
        {
            try
            {               
                Data.ValidateBeforeEncoding();
                outputStream.Write(DLE);
                outputStream.Write(STX);
                WriteCommand(outputStream);
                WriteMessageData(outputStream);
                WriteChecksum(outputStream);
                outputStream.Write(DLE);
                outputStream.Write(ETX);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Read(IDataInputStream inputStream)
        {
            try
            {
                DLE = inputStream.ReadChar();
                STX = inputStream.ReadChar();
                CMD = inputStream.ReadChar();
                ReadMessageData(inputStream);
                ReadChecksum(inputStream);
                ValidatePacket();
                ETX = inputStream.ReadChar();
                DLE = inputStream.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteCommand(IDataOutputStream outputStream)
        {
            CMD = Data.GetType();
            outputStream.Write(CMD);
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
            if (STX != PacketConstants.STX || ETX != PacketConstants.ETX || DLE!=PacketConstants.DLE|| !Data.ValidateAfterDecoding())
            {
                throw new CustomErrorException(400, "Message is corrupted!");
            }
            return;
        }
    }
}
