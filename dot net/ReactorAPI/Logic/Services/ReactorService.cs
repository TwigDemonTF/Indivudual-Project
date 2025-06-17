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

        public Task AddReactorData(ReactorHistoryDTO reactorHistoryDto)
        {
            return _repository.AddReactorData(reactorHistoryDto);
        }

        public List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId)
        {
            return _repository.GetLatestReactorData(fromUtc, reactorId);
        }

        public ReactorDTO GetReactor(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
