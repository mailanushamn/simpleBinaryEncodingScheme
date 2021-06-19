using BinaryEncodingApp.Models;
using BinaryEncodingScheme;
using BinaryEncodingScheme.Impl;
using BinaryEncodingScheme.Interfaces;
using BinaryEncodingScheme.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryEncodingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = GetEmployeeMessage();
            Console.WriteLine("Payload length before encoding: " + message.Payload.Length);
            Console.WriteLine("Header count before encoding: " + message.Headers.Count);

            var service = new MessageService<Message>();
            var codec = new MessageCodec<Message>(service);

            var encodedBytes=  codec.Encode(message);
            Console.WriteLine("Length of encoded bytes: " + encodedBytes.Length);

            var decodedMessage = codec.Decode(encodedBytes);
            Console.WriteLine("Header count after decoding: "+ decodedMessage.Headers.Count);

            foreach (var item in decodedMessage.Headers)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
            Console.WriteLine("payload length after decoding: " + decodedMessage.Payload.Length);

            var employee = GetObject(decodedMessage.Payload);

            Console.WriteLine("Name: " + employee.Name);
            Console.WriteLine("Id: " + employee.Id);
            Console.WriteLine("Designation: " + employee.Designation);

            Console.ReadLine();
        }

        private static Employee GetEmployee()
        {
            var employee = new Employee();
            employee.Name = "Mr.Developer";
            employee.Id = 12345;
            employee.Designation = "software engineer";
            return employee;
        }

        private static Message GetEmployeeMessage()
        {
            var employee = GetEmployee();
            var empMessage= new Message();
            empMessage.Headers = new Dictionary<string, string>();
            empMessage.Headers.Add("Version", "1.2");
            empMessage.Headers.Add("Id", "1234");
            empMessage.Headers.Add("Datetime", "18/6/2020");
            empMessage.Headers.Add("Type", "Employee");
            empMessage.Payload = GetBytes(employee);
            return empMessage;
        }
        private static byte[] GetBytes(Employee employee)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, employee);
                return ms.ToArray();
            }
        }
        private static Employee GetObject(byte[] bytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                object obj = bf.Deserialize(ms);
                return (Employee)obj;
            }
        }
    }
}
