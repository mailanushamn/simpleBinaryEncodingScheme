using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryEncodingScheme.Tests
{
    public class CodecTest
    {
        [Fact]
        public void EncodeValidMessageTest()
        {
            //Arrange
            var message = GetMessage();
            IEncoder binaryEncoder = new BinaryCodec();

            //Act
            var bytes = binaryEncoder.Encode(message);
            Console.WriteLine("encoded meassge " + bytes.ToString());

            //Assert
            Assert.True(string.Equals(expectedResult, result.DataSource));
        }

        private Message GetMessage()
        {
            var message = new Message();
            var dict = new Dictionary<string, string>();
            dict.Add("packetid", "1992"); //packet id- 1992
            dict.Add("type", "msg"); // type - msg
            message.Payload = Encoding.UTF8.GetBytes("Message1:This is the text to be encoded.");
            message.Headers = dict;
            return message;
        }
    }
}
