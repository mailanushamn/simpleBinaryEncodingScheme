namespace BinaryEncodingScheme.Utility
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
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

    public class HeaderMaxLimitExceededException : Exception
    {
        public string Error { get; }

        public HeaderMaxLimitExceededException(string errorMessage)
        {
            Error = errorMessage;
        }

    }
    public class HeaderMaxSizeExceededException : Exception
    {
        public string Error { get; }

        public HeaderMaxSizeExceededException(string errorMessage)
        {
            Error = errorMessage;
        }

    }
    public class PayloadMaxSizeExceededException : Exception
    {
        public string Error { get; }

        public PayloadMaxSizeExceededException(string errorMessage)
        {
            Error = errorMessage;
        }

    }
}
