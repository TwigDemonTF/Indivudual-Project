using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserInterface
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository respository)
        {
            _repository = respository;
        }

        public Task<UserDTO> CreateUser(RegisterDTO registerDto)
        {
            return _repository.CreateUser(registerDto);
        }


        public UserDTO GetUser(int id)
        {
            return _repository.GetUser(id);
        }

        public UserDTO AuthenticateUser(LoginDTO loginDto)
        {
            return _repository.AuthenticateUser(loginDto);
        }

        public async Task<bool> BindReactorToUser(BindReactorDTO bindReactorDto)
        {
            if (bindReactorDto == null)
                throw new ArgumentNullException(nameof(bindReactorDto));

            return await _repository.BindReactorToUser(bindReactorDto);
        }

    }
}
