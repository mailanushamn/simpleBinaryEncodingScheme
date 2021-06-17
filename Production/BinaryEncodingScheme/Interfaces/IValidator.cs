namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for validating object before and after decoding.
    /// </summary>
    public interface IValidator
    {
        bool ValidateBeforeEncoding();
        bool ValidateAfterDecoding();
    }
}
