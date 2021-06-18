using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface to define the structure of an object which can be encoded using the custom binary protocol.
    /// </summary>
    public interface IService : IReader, IWriter, IValidator 
    {
        char GetObjectType();  
    }
}
