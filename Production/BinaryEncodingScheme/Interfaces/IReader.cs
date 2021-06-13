using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IReader
    {
        void Read(IDataInputStream inputStream);
    }
}
