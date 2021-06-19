using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface to define the structure of an object which can be encoded using the custom binary protocol.
    /// </summary>
    public interface IService<T> : IReader<T>, IWriter<T>, IValidator<T>
    {
        char GetObjectType();
        void WriteChecksum(IDataOutputStream outputStream, T message);
        byte[] ReadChecksum(IDataInputStream inputStream);
    }
}
