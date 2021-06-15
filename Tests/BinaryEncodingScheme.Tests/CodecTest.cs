using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryEncodingScheme.Tests
{
    public class CodecTests
    {
        public CodecTests()
        {
            
        }
        [Fact]
        public void EncodeDecodeValidMessageTest()
        {
            //Arrange
            var message = GetMessage();
            IEncoder<Message> binaryEncoder = new BinaryCodec();
            IDecoder<Message> binaryDecoder = new BinaryCodec();


            //Act
            var bytes = binaryEncoder.Encode(message);
            var decodeMessage = binaryDecoder.Decode(bytes);

            //Assert
            Assert.Equal(message.Payload.Length, decodeMessage.Payload.Length);
            Assert.Equal(message.Headers.Count, decodeMessage.Headers.Count);
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
