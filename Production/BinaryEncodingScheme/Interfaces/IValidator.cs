namespace BinaryEncodingScheme.Interfaces
{
    public interface IValidator
    {
        bool ValidateBeforeEncoding();
        bool ValidateAfterDecoding();
    }
}
