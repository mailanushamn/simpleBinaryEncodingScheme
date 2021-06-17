using BinaryEncodingApp.Models;
using BinaryEncodingScheme;
using BinaryEncodingScheme.Interfaces;
using System;
using System.Collections.Generic;

namespace BinaryEncodingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var msgBytes = EncodeMessage();
            dynamic decodeMessage = Decode(msgBytes);
            Console.WriteLine("decoded message headers:");
            foreach (var item in decodeMessage.Headers)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
            Console.WriteLine("decoded message payload: \n" + System.Text.Encoding.UTF8.GetString(decodeMessage.Payload)+"\n");

            var empBytes = EncodeEmployee();
            dynamic decodeEmployee = Decode(empBytes);
            Console.WriteLine("decode employee headers:\n UnixTimeStamp: " + decodeEmployee.Header.UnixTimeStamp + "\n Version: " + decodeEmployee.Header.Version);
            Console.WriteLine("decoded message payload \n Name: " + decodeEmployee.Payload.Name+"\n" +" Id: " + decodeEmployee.Payload.Id + "\n Designation: " + decodeEmployee.Payload.Designation);
            Console.ReadLine();
        }

        private static byte[] EncodeMessage()
        {
            var message = GetMessage();
            IEncoder<Message> msgEncoder = new BinaryCodec<Message>();
            var messageBytes = msgEncoder.Encode(message);
            Console.WriteLine("encoded message length: " + messageBytes.Length);
            return messageBytes;
        }

        private static IMessage Decode(byte[] bytes)
        {
            var identifier = BinaryHelper.GetIdentifier(bytes);
            var instance = CreateInstance.GetInstance(identifier);
            return instance.Decode(bytes);
        }

        private static byte[] EncodeEmployee()
        {
            var employee = GetEmployee();
            var empBinaryCodec = new BinaryCodec<Employee>();
            var employeeBytes = empBinaryCodec.Encode(employee);
            Console.WriteLine("encoded employee " + employeeBytes.Length);
            return employeeBytes;
        }

        private static Employee GetEmployee()
        {
           var employee= new Employee();
            employee.Header = new Header();
            employee.Payload = new Payload();
            employee.Header.UnixTimeStamp = 1623946782;
            employee.Header.Version = 1;
            employee.Payload.Name = "Mr.Developer";
            employee.Payload.Id = 12345;
            employee.Payload.Designation = "software engineer";
            return employee;
        }

        private static Message GetMessage()
        {
            var message = new Message();
            var dict = new Dictionary<string, string>();
            dict.Add("packetid", "1992"); //packet id- 1992
            dict.Add("version", "1.2"); // type - msg
            message.Payload = System.Text.Encoding.UTF8.GetBytes("sample text message which is encoded and decoded.");
            message.Headers = dict;
            return message;
        }
    }
}
