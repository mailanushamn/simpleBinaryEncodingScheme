using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IValidator
    {
        bool ValidateBeforeEncoding();
        bool ValidateAfterDecoding();
    }
}
