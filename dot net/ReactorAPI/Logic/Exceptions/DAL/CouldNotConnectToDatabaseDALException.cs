﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions.Dal
{
    public class CouldNotConnectToDatabaseDALException : Exception
    {
        public CouldNotConnectToDatabaseDALException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
