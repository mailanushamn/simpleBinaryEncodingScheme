namespace BinaryEncodingApp
{
    using BinaryEncodingApp.Models;
    using BinaryEncodingScheme;
    using System;
    public static class CreateInstance
    {
        public static Type GetTypeString(char command) 
        {
            switch (command)
            {
                case PacketCommandConstant.MessageRegistration:
                    return typeof(Message);
                case PacketCommandConstant.EmployeeRegistration:
                    return typeof(Employee);
                case '\0':
                    return null;                  

            }
            return null;
        }
        public static dynamic GetInstance(char command)
        {
            switch (command)
            {
                case PacketCommandConstant.MessageRegistration:
                    return new BinaryCodec<Message>();
                case PacketCommandConstant.EmployeeRegistration:
                    return new BinaryCodec<Employee>();
                case '\0':
                    return null;

            }
            return null;
        }
    }
}
