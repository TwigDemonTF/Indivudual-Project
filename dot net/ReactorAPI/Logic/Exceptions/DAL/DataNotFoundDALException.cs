using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions.Dal
{
    public class DataNotFoundDALException : Exception
    {
        public DataNotFoundDALException(string message) : base(message)
        {

        }
    }
}
