﻿using BinaryEncodingScheme.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Tests
{
    public class TestMessage : IMessage
    {
        public char GetObjectType()
        {
            throw new NotImplementedException();
        }

        public void Read(IDataInputStream inputStream)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadChecksum(IDataInputStream inputStream)
        {
            throw new NotImplementedException();
        }

        public bool ValidateAfterDecoding()
        {
            throw new NotImplementedException();
        }

        public bool ValidateBeforeEncoding()
        {
            throw new NotImplementedException();
        }

        public void Write(IDataOutputStream outputStream)
        {
            throw new NotImplementedException();
        }

        public void WriteChecksum(IDataOutputStream outputStream)
        {
            throw new NotImplementedException();
        }
    }
}