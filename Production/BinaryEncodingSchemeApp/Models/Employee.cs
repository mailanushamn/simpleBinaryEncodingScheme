using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingSchemeApp.Models
{
    public class Employee : IMessage
    {
        public Header Header { get; set; }
        public Payload Payload { get; set; }
        private byte[] CheckSum;

        public void Read(IDataInputStream inputStream)
        {
            try
            {
                //read headers
                Header = new Header();
                Header.UnixTimeStamp = inputStream.ReadInt32();
                Header.Version = inputStream.ReadInt32();

                //read payload
                Payload = new Payload();
                Payload.Name = inputStream.ReadString();
                Payload.Id = inputStream.ReadInt32();
                Payload.Designation = inputStream.ReadString();
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while decoding employee object:"+ ex.Message);
            }
        }

        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            var checksumCount = inputStream.ReadInt32();
            CheckSum = inputStream.ReadBytes(checksumCount);
            return CheckSum;
        }

        public bool ValidateAfterDecoding()
        {
            var payloadBytes = Helper.GetBytes(Payload.Name + Payload.Id);
            return Helper.ValidateChecksum(CheckSum, payloadBytes);
        }

        public bool ValidateBeforeEncoding()
        {
            return true;
        }

        public void Write(IDataOutputStream outputStream)
        {
            try
            {
                //write headers
                outputStream.Write(Header.UnixTimeStamp);
                outputStream.Write(Header.Version);

                //write payload           
                outputStream.Write(Payload.Name.Length, Payload.Name);
                outputStream.Write(Payload.Id);
                outputStream.Write(Payload.Designation.Length, Payload.Designation);
            }
            catch (Exception ex)
            {
                throw new CustomErrorException(500, "Error while encoding employee object" + ex.Message);
            }

        }

        public void WriteChecksum(IDataOutputStream outputStream)
        {
            var payloadBytes = Helper.GetBytes(Payload.Name + Payload.Id);
            var hash = Helper.CalculateChecksum(payloadBytes);
            outputStream.Write(hash.Length);
            outputStream.Write(hash);
        }

        public char GetObjectType()
        {
            return PacketCommandConstant.EmployeeRegistration;
        }
    }
    public class Header
    {
        public int Version { get; set; }
        public int UnixTimeStamp { get; set; }
    }

    public class Payload
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Designation { get; set; }
    }
}
