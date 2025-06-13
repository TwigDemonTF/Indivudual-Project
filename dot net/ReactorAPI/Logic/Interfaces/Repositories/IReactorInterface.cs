using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IReactorInterface
    {
        Task AddReactorData(int reactorId, int temperature, int fieldStrength, int energySaturation, int fuelExhaustion, DateTime timeStamp);
        ReactorDTO GetReactor(int userId);
    }
}
