using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IMessage : IReader, IWriter, IValidator ,ICheckSumWriter, ICheckSumReader
    {
        char GetType();
      
    }
}
