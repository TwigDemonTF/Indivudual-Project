using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions.Logic
{
    public class ReactorNotFoundLogicException : Exception
    {
        public ReactorNotFoundLogicException(string message) : base(message)
        {

        }
    }
}
