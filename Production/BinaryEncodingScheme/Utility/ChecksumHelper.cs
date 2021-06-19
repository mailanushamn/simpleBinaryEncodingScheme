namespace BinaryEncodingScheme.Utility
{
    using System;
    using System.Linq;
    public static class ChecksumHelper
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
