using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IUserRespository
    {
        UserDTO GetUser(int id);
        Task CreateUser(int id, string name, string email, string password);
    }
}
