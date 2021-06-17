using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for writing checksum.
    /// </summary>
    public interface ICheckSumWriter
    {
        void WriteChecksum(IDataOutputStream outputStream);
    }
}
