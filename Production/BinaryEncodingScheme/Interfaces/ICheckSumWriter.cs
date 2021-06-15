using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface ICheckSumWriter
    {
        void WriteChecksum(IDataOutputStream outputStream);
    }
}
