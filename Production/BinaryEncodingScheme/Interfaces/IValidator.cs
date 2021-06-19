using BinaryEncodingScheme.Models;

namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for validating object before and after decoding.
    /// </summary>
    public interface IValidator<T>
    {
        bool ValidateMessage(T message);
        bool ValidateChecksum(byte[] checksum, T message);

    }
}
