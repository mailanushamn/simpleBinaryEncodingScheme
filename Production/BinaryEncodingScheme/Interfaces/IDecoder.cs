using BinaryEncodingScheme.Models;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IDecoder<T>
    {
        T Decode(byte[] bytes);
    }
}
