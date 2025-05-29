using DataAccess.MSSQL;
using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReactorRepository : BaseRepository, IReactorRepository
    {
        public Task AddReactorData(int reactorId, int temperature, int fieldStrength, int energySaturation, int fuelExhaustion, DateTime timeStamp)
        {
            throw new NotImplementedException();
        }
    }
}
