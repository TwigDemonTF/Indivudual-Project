using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    /// <summary>
    /// Defines methods for accessing and managing reactor-related data.
    /// </summary>
    public interface IReactorRepository
    {
        /// <summary>
        /// Adds historical data for a reactor to the database.
        /// </summary>
        /// <param name="reactorHistoryDto">The reactor data to be stored.</param>
        Task AddReactorData(ReactorHistoryDTO reactorHistoryDto);

        /// <summary>
        /// Retrieves a reactor by the associated user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose reactor is requested.</param>
        ReactorDTO GetReactor(int userId);

        List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId);

        Task<int?> GetUserIdByReactorIdAsync(int reactorId);
    }
}
