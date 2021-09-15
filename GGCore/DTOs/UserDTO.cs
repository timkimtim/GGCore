using GGCore.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.DTOs
{

    public record LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Пароль должен содержать от {2} от {1} символов", MinimumLength = 8)]
        public string Password { get; set; }

    }

    public record RegisterUserDTO : LoginUserDTO
    {
        [Required]
        public string UserName { get; set; }

        public ICollection<string> Roles { get; set; }

#nullable enable
        public string? About { get; set; }
        public string? ImagePath { get; set; }
    #nullable disable
    }

    public class RegisterUserResponseDTO : AuthResult
    {

    }
}
