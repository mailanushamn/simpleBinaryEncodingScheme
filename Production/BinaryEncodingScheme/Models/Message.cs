using BinaryEncodingScheme.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Models
{
    /// <summary>
    /// Defines Message properties. 
    /// </summary>
    public class Message : IMessage
    {
        public Dictionary<string, string> Headers { get; set; }
        public byte[] Payload { get; set; }
    }
}
