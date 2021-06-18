namespace BinaryEncodingScheme.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEncoder<T>
    {
        byte[] Encode(T msg);
    }
}
