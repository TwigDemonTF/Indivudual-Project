using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IReactorRepository
    {
        Task AddReactorData(ReactorHistoryDTO reactorHistoryDto);
        ReactorDTO GetReactor(int userId);
    }
}
