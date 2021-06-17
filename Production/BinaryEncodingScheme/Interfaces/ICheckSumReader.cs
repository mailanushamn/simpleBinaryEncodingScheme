using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for reading checksum.
    /// </summary>
    public interface ICheckSumReader
    {
        byte[] ReadChecksum(IDataInputStream inputStream);
    }
}
