using BinaryEncodingScheme.Impl;
using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using BinaryEncodingScheme.Utility;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BinaryEncodingScheme.Tests
{
    public class PacketTest
    {
        Mock<IService<Message>> _mockMessageService = new Mock<IService<Message>>();
        
        public PacketTest()
        {
            _mockMessageService.Setup(x => x.GetObjectType()).Returns('A');
        
        }
        [Fact]
        public void PacketWriteReadValidObject_ReturnsValue_Test()
        {
            //Arrange
            var message = GetValidMessage();
            _mockMessageService.Setup(x => x.ValidateMessage(It.IsAny<Message>())).Returns(true);
            _mockMessageService.Setup(x => x.ReadChecksum(It.IsAny<DataInputStream>())).Returns(new byte[1]);

            _mockMessageService.Setup(x => x.ValidateChecksum(It.IsAny<byte[]>(),It.IsAny<Message>())).Returns(true);

            _mockMessageService.Setup(x => x.Read(It.IsAny<DataInputStream>())).Returns(GetValidMessage());

            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);         
            var expectedDLE = PacketConstants.DLE;
            var expectedSTX = PacketConstants.STX;
            var packetToTest = new BinaryPacket<Message>(_mockMessageService.Object);

            //Act
            packetToTest.Write(dataOutputStream, message);

            var dataInputStream = new DataInputStream(new MemoryStream(outStream.ToArray()));
            packetToTest.Read(dataInputStream);

            //Assert
            Assert.Equal(expectedDLE, packetToTest.DLE);
            Assert.Equal(expectedSTX, packetToTest.STX);
        }

        [Fact]
        public void PacketWriteInvalidObject_ThrowsException_Test()
        {
            //Arrange
            _mockMessageService.Setup(x => x.ValidateMessage(It.IsAny<Message>())).Returns(false);
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);
           
            //Act and Assert
            var packetToTest = new BinaryPacket<Message>(_mockMessageService.Object);
            Assert.Throws<CustomErrorException>(()=>packetToTest.Write(dataOutputStream, null));         
        }
        [Fact]
        public void PacketReadInValidObject_ThrowsException_Test()
        {
            //Arrange
            _mockMessageService.Setup(x => x.ValidateMessage(It.IsAny<Message>())).Returns(true);
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);
            var packetToTest = new BinaryPacket<Message>(_mockMessageService.Object);
            packetToTest.Write(dataOutputStream,null);

            //Act
            var dataInputStream = new DataInputStream(new MemoryStream(outStream.ToArray()));
            Assert.Throws<CustomErrorException>(() => packetToTest.Read(dataInputStream));

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
