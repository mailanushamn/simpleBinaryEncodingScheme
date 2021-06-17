namespace BinaryEncodingScheme
{
    public static class BinaryHelper
    {
        /// <summary>
        /// Gets the char which identifies the type of the encoded object
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        public static char GetCommandType(byte[] encodedData)
        {
            return (char)(encodedData[2]);
        }

    }
}
