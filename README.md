<snippet>
  <content><![CDATA[


<h2></h2>

## Simple Binary Encoding Scheme

Simple Binary Message Encoding Scheme(SBME) is a library built to encode and decode any given object into binary and viceversa using a custom protocol.
The custom protocol used is as below <br />
### **[DLE][STX][Identifier][HeaderCount][Headers][PayloadLength][Payload][ChecksunLength][Checksum][DLE][ETX]** <br />
[DLE][STX] - Represents the start of the text. <br />
[Identifier] - A single char which represents the type of the object in this case its always M. <br />
[HeaderCount]- Number of headers. <br />
[Headers] - Header of the Object. <br />
[PayloadLength] - Length of the payload bytes.<br />
[Payload] - Actual data being transmitted.<br />
[ChecksunLength] - Length of checksum.<br />
[Checksum] - Encrypted payload which is sent by the sender (encoder) for receiver (decoder) to ensure the accuracy of the data by comparing the transmitted checksum with calculated checksum of payload at the receiver end.
[DLE][ETX] - Represents the end of text.

## Usage

Any client application which uses SEB scheme to encode or decode an object should convert their data into `Message` class  
 
    public class Message 
    {
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Payload { get; set; }
    }

The SBME exposes encode and decode methods to the client which can be used to encode or decode(see the below code snippet).
    
       var codec = new MessageCodec<Message>();
       var encodedBytes=  codec.Encode(message);
       var decodedMessage = codec.Decode(encodedBytes);

The sample code on how to use this can be found [here](https://github.com/mailanushamn/simpleBinaryEncodingScheme/blob/master/Production/BinaryEncodingSchemeApp/Program.cs) 
 

</content>
</snippet>
