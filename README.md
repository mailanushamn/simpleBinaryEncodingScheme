<snippet>
  <content><![CDATA[


<h2></h2>

## Simple Binary Encoding Scheme

Simple Binary Encoding Scheme(SBE) is a library built to encode and decode any given object into binary and viceversa using a custom protocol.
The custom protocol used is as below <br />
[DLE][STX][Identifier][Headers][PayloadLength][Payload][Checksum][DLE][ETX] <br />
[DLE][STX] - Represents the start of the text. <br />
[Identifier] - A single char which represents the type of the object. <br />
[Headers] - Header of the Object. <br />
[Payload length] - length of the payload bytes.<br />
[Payload] - Actual data being transmitted.<br />
[Checksum] - Encrypted payload which is sent by the sender (encoder) for receiver (decoder) to ensure the accuracy of the data by comparing the transmitted checksum with calculated checksum of payload at the receiver end.


## Usage

Any client application which uses SEB scheme to encode or decode an object should implement IMessage interface exposed by SEB(see the below example code snippet).
The BinaryPacket class exposed by the SEB takes the object which implements IMessage, creates a stream and adds the appropriate start and end tags.
The responsibility of writing the headers and payload into stream is given to the individual object class as each object is different. The methods to write int, string, byte, char are exposed by SEB.
The object class which implements IMessage can leverage the methods and write the data in the order of headers, payload length and payload. 
Writing and reading checksum is also the responisibility of individual object class giving the freedom to calculate its own checksum to keep a check on the checksum length.



     
    
    public class TestMessage : IMessage
    {
        public char GetObjectType()
        {
            throw new NotImplementedException();
        }

        public void Read(IDataInputStream inputStream)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAfterDecoding()
        {
            throw new NotImplementedException();
        }

        public bool ValidateBeforeEncoding()
        {
            throw new NotImplementedException();
        }

        public void Write(IDataOutputStream outputStream)
        {
            throw new NotImplementedException();
        }

        public void WriteChecksum(IDataOutputStream outputStream)
        {
            throw new NotImplementedException();
        }
    }
    
    

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## History

TODO: Write history

## Credits

TODO: Write credits

## License

TODO: Write license
]]></content>
  <tabTrigger>readme</tabTrigger>
</snippet>
