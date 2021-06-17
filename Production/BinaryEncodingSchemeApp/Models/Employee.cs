using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingApp.Models
{
    public class Employee : IMessage
    {
        public Header Header { get; set; }
        public Payload Payload { get; set; }
        private byte[] CheckSum;

        /// <summary>
        /// Reads headers and payload from stream.
        /// </summary>
        /// <param name="inputStream"></param>
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

        /// <summary>
        /// Reads checksum from the stream.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            var checksumCount = inputStream.ReadInt32();
            CheckSum = inputStream.ReadBytes(checksumCount);
            return CheckSum;
        }

        /// <summary>
        /// Validates decoded object using checksum to ensure accuracy.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAfterDecoding()
        {
            var payloadBytes = Helper.GetBytes(Payload.Name + Payload.Id);
            return Helper.ValidateChecksum(CheckSum, payloadBytes);
        }

        /// <summary>
        /// Validates object before encoding.
        /// </summary>
        /// <returns></returns>
        public bool ValidateBeforeEncoding()
        {
            return true;
        }

        /// <summary>
        /// Writes the object into stream in the order of headers, payload length and payload.
        /// </summary>
        /// <param name="outputStream"></param>
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

        /// <summary>
        ///Calculates checksum and writes it to stream along with length.
        /// </summary>
        /// <param name="outputStream"></param>
        public void WriteChecksum(IDataOutputStream outputStream)
        {
            var payloadBytes = Helper.GetBytes(Payload.Name + Payload.Id);
            var hash = Helper.CalculateChecksum(payloadBytes);
            outputStream.Write(hash.Length);
            outputStream.Write(hash);
        }

        public char GetObjectType()
        {
            return(char) PacketCommandConstant.EmployeeRegistration;
        }
    }

    /// <summary>
    /// Header of Employee object.
    /// </summary>
    public class Header
    {
        public int Version { get; set; }
        public int UnixTimeStamp { get; set; }
    }

    /// <summary>
    /// Payload of Employee object.
    /// </summary>
    public class Payload
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Designation { get; set; }
    }
}
