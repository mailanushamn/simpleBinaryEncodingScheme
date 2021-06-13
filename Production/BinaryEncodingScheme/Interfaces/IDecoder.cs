using BinaryEncodingScheme.Models;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IDecoder
    {
        Message Decode(byte[] bytes);

    }
}
