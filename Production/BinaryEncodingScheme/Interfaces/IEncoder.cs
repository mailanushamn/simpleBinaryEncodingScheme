namespace BinaryEncodingScheme.Interfaces
{
    public interface IEncoder<T>
    {
        byte[] Encode(T msg);
    }
}
