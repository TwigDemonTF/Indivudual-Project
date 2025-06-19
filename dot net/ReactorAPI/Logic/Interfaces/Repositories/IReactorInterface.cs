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
        Task AddReactorData(ReactorHistoryDTO reactorHistoryDto);
        ReactorValuesDTO GetReactorValues(int reactorId);
        List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId);
        Task<bool> UpdateReactorValues(int id, ReactorValuesDTO reactorValuesDto);
    }
}
