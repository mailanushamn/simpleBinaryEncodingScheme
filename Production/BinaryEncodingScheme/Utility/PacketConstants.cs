namespace BinaryEncodingScheme.Utility
{
    /// <summary>
    /// Defines the symbols for start and end of encoded message in the protocol.
    /// </summary>
    public static class PacketConstants
    {
        public static char STX { get { return '$'; } }
        public static char ETX { get { return '*'; } }

        public static char DLE { get { return '%'; } }
    }
}
