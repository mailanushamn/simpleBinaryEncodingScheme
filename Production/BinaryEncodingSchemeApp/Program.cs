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

            var msgCodec = new BinaryCodec<Message>();
            var bytes = msgCodec.Encode(message);
            Console.WriteLine("encoded meassge " + bytes.ToString());          
            var decodeMessage = msgCodec.Decode(bytes);
            Console.WriteLine("decoded message " + System.Text.Encoding.UTF8.GetString(decodeMessage.Payload));
           

            var employee = new Employee();          
            employee.Header= new Header();
            employee.Payload = new Payload();

            employee.Header.UnixTimeStamp = 1;
            employee.Header.Version = 1;
            employee.Payload.Name = "Surya";
            employee.Payload.Id = 12345;
            employee.Payload.Designation = "software engineer";


           var empBinaryCodec = new BinaryCodec<Employee>();
            var bytes1 = empBinaryCodec.Encode(employee);
            Console.WriteLine("encoded meassge " + bytes1.Length);

            var name = "BinaryEncodingScheme.Models.Employee";


            var decodeMessage1 = (Employee)empBinaryCodec.Decode(bytes1);
            Console.WriteLine("decoded message: Name " + decodeMessage1.Payload.Name +"Id" + decodeMessage1.Payload.Id );
            Console.ReadLine();
        }
    }
}
