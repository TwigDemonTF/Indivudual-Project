using Logic.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        UserDTO GetUser(int id);

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="registerDto">The data required to register a new user.</param>
        Task<UserDTO> CreateUser(RegisterDTO registerDto);

        /// <summary>
        /// Authenticates a user using login credentials.
        /// </summary>
        /// <param name="loginDto">The user's login credentials.</param>
        UserDTO AuthenticateUser(LoginDTO loginDto);

        /// <summary>
        /// Binds a reactor to a user in the database.
        /// </summary>
        /// <param name="bindReactorDto">The binding information (user and reactor ID).</param>
        Task<bool> BindReactorToUser(BindReactorDTO bindReactorDto);
    }
}
