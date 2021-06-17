using BinaryEncodingScheme.Impl;
using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Utility;
using Moq;
using System.IO;
using Xunit;

namespace BinaryEncodingScheme.Tests
{
    public class PacketTest
    {
        Mock<IMessage> _mockMessage = new Mock<IMessage>();
        

        public PacketTest()
        {           
            _mockMessage.Setup(x => x.GetObjectType()).Returns('A');
            _mockMessage.Setup(x => x.ReadChecksum(It.IsAny<DataInputStream>())).Returns(new byte[1]);

        }
        [Fact]
        public void PacketWriteReadValidObject_ReturnsValue_Test()
        {
            //Arrange
            _mockMessage.Setup(x => x.ValidateBeforeEncoding()).Returns(true);
            _mockMessage.Setup(x => x.ValidateAfterDecoding()).Returns(true);
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);         
            var expectedDLE = PacketConstants.DLE;
            var expectedSTX = PacketConstants.STX;
            var exceptedIdentifier = 'A';
            var packetToTest = new BinaryPacket(_mockMessage.Object);

            //Act
            packetToTest.Write(dataOutputStream);

            var dataInputStream = new DataInputStream(new MemoryStream(outStream.ToArray()));
            packetToTest.Read(dataInputStream);

            //Assert
            Assert.Equal(expectedDLE, packetToTest.DLE);
            Assert.Equal(expectedSTX, packetToTest.STX);
            Assert.Equal(exceptedIdentifier, packetToTest.Identifier);
        }

        [Fact]
        public void PacketWriteInvalidObject_ThrowsException_Test()
        {
            //Arrange
            _mockMessage.Setup(x => x.ValidateBeforeEncoding()).Returns(false);
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);
           
            //Act and Assert
            var packetToTest = new BinaryPacket(_mockMessage.Object);
            Assert.Throws<CustomErrorException>(()=>packetToTest.Write(dataOutputStream));
           
        }
        [Fact]
        public void PacketReadInValidObject_ThrowsException_Test()
        {
            //Arrange
            _mockMessage.Setup(x => x.ValidateBeforeEncoding()).Returns(true);
            _mockMessage.Setup(x => x.ValidateAfterDecoding()).Returns(false);
            var outStream = new MemoryStream();
            IDataOutputStream dataOutputStream = new DataOutputStream(outStream);
            var packetToTest = new BinaryPacket(_mockMessage.Object);
            packetToTest.Write(dataOutputStream);

            //Act
            var dataInputStream = new DataInputStream(new MemoryStream(outStream.ToArray()));
            Assert.Throws<CustomErrorException>(() => packetToTest.Read(dataInputStream));

        }
    }
}
