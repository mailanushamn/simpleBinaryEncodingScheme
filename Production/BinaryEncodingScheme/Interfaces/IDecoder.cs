
namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// Interface for decoder.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDecoder<T>
    {
        T Decode(byte[] bytes);
    }
}
