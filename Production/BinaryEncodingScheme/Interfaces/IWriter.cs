using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IWriter
    {
        void Write(IDataOutputStream outputStream);
    }
}
