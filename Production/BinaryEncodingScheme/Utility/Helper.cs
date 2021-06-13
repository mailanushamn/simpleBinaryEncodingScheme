namespace BinaryEncodingScheme.Utility
{
    using System;
    using System.Linq;
    public static class Helper
    {
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

        public static bool ValidateChecksum(byte[] checksum, byte[] payload)
        {
            var calculatedChecksum = CalculateChecksum(payload);

           return calculatedChecksum.SequenceEqual(checksum);
        }      
    }
}
