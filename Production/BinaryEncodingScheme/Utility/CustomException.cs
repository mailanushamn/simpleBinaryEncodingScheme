namespace BinaryEncodingScheme.Utility
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomErrorException : Exception
    {
        public string Error { get; }

        public int StatusCode { get; }

        public CustomErrorException(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            Error = errorMessage;
        }
    }
}
