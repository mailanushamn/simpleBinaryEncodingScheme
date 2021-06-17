namespace BinaryEncodingScheme
{
    using BinaryEncodingScheme.Interfaces;
    using BinaryEncodingScheme.Utility;
    using System;

    /// <summary>
    /// Creates a packet in the format <DLE><Stx>|Identifier|Headers|Payload|Checksum|<DLE><Etx>
    /// </summary>
    public class BinaryPacket : IReader, IWriter
    {
        public char STX { get; private set; }
        public char ETX { get; private set; }

        public char DLE { get; private set; }

        public IMessage Data { get; private set; }

        public int Identifier { get; private set; }

        public BinaryPacket(
            IMessage data)
            
        {
            Data = data;
            STX = PacketConstants.STX;
            ETX = PacketConstants.ETX;
            DLE = PacketConstants.DLE;
        }

        /// <summary>
        /// Writes into stream in the order DLE,STX,CMD,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="outputStream"></param>
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
                Identifier = Data.GetObjectType();
                outputStream.Write(Identifier);
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

        /// <summary>
        /// Reads from the stream in the order DLE,STX,CMD,Headers,Payload,DLE,ETX
        /// </summary>
        /// <param name="inputStream"></param>
        public void Read(IDataInputStream inputStream)
        {
            try
            {
                DLE = inputStream.ReadChar();
                STX = inputStream.ReadChar();
                Identifier = inputStream.ReadInt32();
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
