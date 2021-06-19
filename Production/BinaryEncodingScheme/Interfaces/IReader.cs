using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for reader which reads from stream.
    /// </summary>
    public interface IReader<T>
    {
        T Read(IDataInputStream inputStream);
    }
}
