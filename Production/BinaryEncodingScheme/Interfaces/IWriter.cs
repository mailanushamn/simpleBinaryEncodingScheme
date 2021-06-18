using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for writing into stream.
    /// </summary>
    public interface IWriter
    {
        void Write(IDataOutputStream outputStream, Message message);
    }
}
