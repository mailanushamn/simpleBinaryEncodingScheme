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
                if (!Data.ValidateBeforeEncoding())
                {
                    throw new CustomErrorException(400, "Validation failed.Invalid input.");
                }
                outputStream.Write(DLE);
                outputStream.Write(STX);
                CMD = Data.GetObjectType();
                outputStream.Write(CMD);
                Data.Write(outputStream);
                Data.WriteChecksum(outputStream);
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
                Data.Read(inputStream);
                Data.ReadChecksum(inputStream);
                ValidatePacket();               
                DLE = inputStream.ReadChar();
                ETX = inputStream.ReadChar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
