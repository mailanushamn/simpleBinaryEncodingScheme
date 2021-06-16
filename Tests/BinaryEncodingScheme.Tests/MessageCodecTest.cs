//using BinaryEncodingScheme.Interfaces;
//using BinaryEncodingScheme.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace BinaryEncodingScheme.Tests
//{
//    public class CodecTests
//    {
//        private BinaryCodec<Message> binaryCodec;
//        public CodecTests()
//        {
//             binaryCodec = new BinaryCodec<Message>();
//        }
//        [Fact]
//        public void EncodeDecodeValidMessageTest()
//        {
//            //Arrange
//            var message = GetMessage();
            
//            //Act
//            var bytes = binaryCodec.Encode(message);
//            var decodeMessage = binaryCodec.Decode(bytes);

//            //Assert
//            Assert.Equal(message.Payload.Length, decodeMessage.Payload.Length);
//            Assert.Equal(message.Headers.Count, decodeMessage.Headers.Count);
//        }

//        [Fact]
//        public void EncodeDecodeValidEmployeeTest()
//        {
//            //Arrange
//            var employee = GetEmployee();
//            var binaryCodec = new BinaryCodec<Employee>();

//            //Act
//            var bytes = binaryCodec.Encode(employee);
//            var decodeMessage = binaryCodec.Decode(bytes);

//            //Assert
//            Assert.Equal(employee.Payload.Name, decodeMessage.Payload.Name);
//            Assert.Equal(employee.Payload.Id, decodeMessage.Payload.Id);
//            Assert.Equal(employee.Payload.Designation, decodeMessage.Payload.Designation);
//            Assert.Equal(employee.Header.Version, decodeMessage.Header.Version);
//        }

//        private Message GetMessage()
//        {
//            var message = new Message();
//            var dict = new Dictionary<string, string>();
//            dict.Add("packetid", "1992"); //packet id- 1992
//            dict.Add("type", "msg"); // type - msg
//            message.Payload = Encoding.UTF8.GetBytes("Message1:This is the text to be encoded.");
//            message.Headers = dict;
//            return message;
//        }

//        private Employee GetEmployee()
//        {
//            var employee = new Employee();
//            employee.Header = new Header();
//            employee.Payload = new Payload();
//            employee.Header.UnixTimeStamp = 1;
//            employee.Header.Version = 1;
//            employee.Payload.Name = "Surya";
//            employee.Payload.Id = 12345;
//            employee.Payload.Designation = "software engineer";

//            return employee;
//        }
//    }
//}
