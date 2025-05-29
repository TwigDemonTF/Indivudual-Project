using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public interface IReactorService
    {
        Task AddReactorData(int reactorId, int temperature, int fieldStrength, int energySaturation, int fuelExhaustion, DateTime timeStamp);
    }
}
