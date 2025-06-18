using Logic.DTO_s;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactorAPI.Controllers
{
    /// <summary>
    /// Handles user-related HTTP requests such as login, registration, user info retrieval, and reactor binding.
    /// </summary>
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        /// <summary>
        /// Retrieves the details of a user by ID.
        /// </summary>
        [HttpGet("User/Details/{id}")]
        public ActionResult<UserDTO> Details(int id)
        {
            try
            {
                UserDTO? userDTO = _userService.GetUser(id);
                if (userDTO == null)
                {
                    return NotFound();
                }

                return Ok(userDTO); // 200 with JSON body
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        /// <summary>
        /// Authenticates a user with email and password.
        /// </summary>
        [HttpPost("User/Login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _userService.AuthenticateUser(loginDto);
            if (user == null) return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("minecraftUsername", user.minecraftUsername)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_1234567890123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = user
            });
        }

        [HttpPost("User/Register")]
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                UserDTO? userDto = await _userService.CreateUser(registerDto); // 👈 await async call
                return Ok(new { message = "Register Successful" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Register Failed");
            }
        }

        // GET: UserController/Edit/5
        /// <summary>
        /// Binds a user to a reactor using the provided reactor ID and user ID.
        /// </summary>
        [HttpPost("User/BindReactor")]
        public async Task<IActionResult> BindToReactor([FromBody] BindReactorDTO bindReactorDto)
        {
            try
            {
                // Get userId from session
                //if (!HttpContext.Session.TryGetValue("userId", out var userIdBytes))
                //    return Unauthorized("User not logged in");

                //bindReactorDto.userId = BitConverter.ToInt32(userIdBytes, 0);

                bool success = await _userService.BindReactorToUser(bindReactorDto);

                if (success)
                    return Ok(new { success = true });

                return BadRequest(new { success = false, message = "Failed to bind reactor." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
