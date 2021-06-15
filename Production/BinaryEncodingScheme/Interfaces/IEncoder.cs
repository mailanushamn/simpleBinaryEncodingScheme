﻿using BinaryEncodingScheme.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryEncodingScheme.Interfaces
{
    public interface IEncoder<T>
    {
        byte[] Encode(T msg);
    }
}
