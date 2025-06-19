using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions.Logic
{
    public class CouldNotConnectToDatabaseLogicException : Exception
    {
        public CouldNotConnectToDatabaseLogicException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
