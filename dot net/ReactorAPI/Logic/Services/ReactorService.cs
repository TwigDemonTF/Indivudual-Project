using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ReactorService : IReactorInterface
    {
        private readonly IReactorRepository? _repository;

        public ReactorService(IReactorRepository repository)
        {
            _repository = repository;
        }

        public Task AddReactorData(int reactorId, int temperature, int fieldStrength, int energySaturation, int fuelExhaustion, DateTime timeStamp)
        {
            return _repository.AddReactorData(reactorId, temperature, fieldStrength, energySaturation, fuelExhaustion, timeStamp);
        }

        public ReactorDTO GetReactor(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
