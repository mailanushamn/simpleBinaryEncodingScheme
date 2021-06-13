using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IEncoder
    {
        byte[] Encode(Message msg);
    }
}
