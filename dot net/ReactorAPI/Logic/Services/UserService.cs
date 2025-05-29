using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _repository;
        public UserService(IUserRespository respository)
        {
            _repository = respository;
        }

        public Task CreateUser(int id, string name, string email, string password)
        {
            return _repository.CreateUser(id, name, email, password);
        }

        public UserDTO GetUser(int id)
        {
            return _repository.GetUser(id);
        }
    }
}
