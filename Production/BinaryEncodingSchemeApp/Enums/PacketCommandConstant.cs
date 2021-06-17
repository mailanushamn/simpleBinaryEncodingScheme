﻿namespace BinaryEncodingApp
{
    /// <summary>
    /// Represents the char representation of each model class which enables in identifying the type during decoding.
    /// </summary>
    public static class PacketCommandConstant
    {
        public const char MessageRegistration = 'M';
        public const char EmployeeRegistration = 'E';
    }
}
