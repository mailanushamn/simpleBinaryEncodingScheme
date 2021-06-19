using BinaryEncodingScheme.Impl;
using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using BinaryEncodingScheme.Utility;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BinaryEncodingScheme.Tests
{
    public class MessageServiceTests
    {
        private IService<Message> messageService;
        public MessageServiceTests()
        {
            messageService = new MessageService<Message>();
        }

        [Fact]
        public void GetObjectTypeTest()
        {
            var type = messageService.GetObjectType();
            type.Should().Be('M');
        }
        [Fact]
        public void ValidateMessage_Valid()
        {
            var message = GetValidMessage();
            var result = messageService.ValidateMessage(message);
            Assert.True(result);
        }
        [Fact]
        public void ValidateMessage_withMaxCountExceedHeaders()
        {
            var message = GetMessageWithMaxCountHeaders();
           Assert.Throws<HeaderMaxLimitExceededException>(()=> messageService.ValidateMessage(message));
        }
        [Fact]
        public void ValidateMessage_withMaxSizeExceededHeaders()
        {
            var message = GetMessageWithMaxSizeHeaders();
            Assert.Throws<HeaderMaxSizeExceededException>(() => messageService.ValidateMessage(message));
        }

        [Fact]
        public void ReadWriteMessage_Valid()
        {
            var message = GetValidMessage();
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);
            messageService.Write(dataOutputStream, message);
            var dataInputStream = new DataInputStream(new MemoryStream(outStream.ToArray()));
            var result = messageService.Read(dataInputStream);
            result.Headers.ContainsKey("key");
            result.Headers.Count.Should().Be(1);
            result.Payload.Length.Should().Be(message.Payload.Length);

        }
        private Message GetMessageWithMaxCountHeaders()
        {
            var message = new Message();
            message.Headers = new Dictionary<string, string>();
            for (int i = 0; i < 65; i++)
            {
                message.Headers.Add(i.ToString(), i.ToString());
            }
            return message;
        }
        private Message GetMessageWithMaxSizeHeaders()
        {
            var message = new Message();
            message.Headers = new Dictionary<string, string>();
            message.Headers.Add("ewrwerwrqwerqwerqwerqwerqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaewrwerwrqwerqwerqwerqwerqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaewrwerwrqwerqwerqwerqwerqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaewrwerwrqwerqwerqwerqwerqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "value");
            return message;
        }
        private Message GetValidMessage()
        {
            var message = new Message();
            message.Headers = new Dictionary<string, string>();
            message.Headers.Add("key", "value");
            message.Payload = Encoding.ASCII.GetBytes("test payload");
            return message;
        }
    }
}