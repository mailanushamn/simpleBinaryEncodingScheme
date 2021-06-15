using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface ICheckSumReader
    {
        byte[] ReadChecksum(IDataInputStream inputStream);
    }
}
