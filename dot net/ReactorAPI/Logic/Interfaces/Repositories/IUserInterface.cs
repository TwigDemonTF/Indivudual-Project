using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IUserInterface
    {
        UserDTO GetUser(int id);
        Task<UserDTO> CreateUser(RegisterDTO registerDto);
        UserDTO AuthenticateUser(LoginDTO loginDto);
    }
}
