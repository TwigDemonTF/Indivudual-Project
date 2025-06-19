using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO_s
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public string? minecraftUsername { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? reactorId { get; set; }
    }
}
