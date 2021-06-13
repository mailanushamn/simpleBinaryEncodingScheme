using BinaryEncodingScheme;
using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;

namespace simpleBinaryEncodingScheme
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = new Message();
            var dict = new Dictionary<string, string>();
            dict.Add("packetid", "1992"); //packet id- 1992
            dict.Add("type", "msg"); // type - msg
            message.Payload= System.Text.Encoding.UTF8.GetBytes("anusha is awesome");
            message.Headers = dict;

             IEncoder binaryEncoder = new BinaryCodec();
            var bytes= binaryEncoder.Encode(message);
            Console.WriteLine("encoded meassge " + bytes.ToString());

            IDecoder binaryDecoder = new BinaryCodec();
            var decodeMessage = binaryDecoder.Decode(bytes);
            Console.WriteLine("decoded message " + System.Text.Encoding.UTF8.GetString(decodeMessage.Payload));
            Console.ReadLine();
        }
    }
}
