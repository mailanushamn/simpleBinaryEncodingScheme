using BinaryEncodingScheme.Models;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for validating object before and after decoding.
    /// </summary>
    public interface IValidator
    {
        bool ValidateMessage(Message message);
    }
}
