namespace BinaryEncodingScheme.Utility
{
    using System;
    using System.Linq;
    public static class Helper
    {
        /// <summary>
        /// Calculates the checksum on payload
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static  byte[] CalculateChecksum(byte[] payload)
        {
            byte[] hash;
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                md5.TransformFinalBlock(payload, 0, payload.Length);
                hash = md5.Hash;
            }
            return hash;
        }

        /// <summary>
        /// Compares the checksum received in the transmitted packet with the calculated checksum on decoded payload.
        /// </summary>
        /// <param name="checksum"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static bool ValidateChecksum(byte[] checksum, byte[] payload)
        {
            var calculatedChecksum = CalculateChecksum(payload);

           return calculatedChecksum.SequenceEqual(checksum);
        }

        /// <summary>
        /// Converts string to bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string value)
        {
           return  System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}
