using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for writing into stream.
    /// </summary>
    public interface IWriter<T>
    {
        void Write(IDataOutputStream outputStream, T message);
    }
}
