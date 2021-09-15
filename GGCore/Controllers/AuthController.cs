using AutoMapper;
using GGCore.DTOs;
using GGCore.Models;
using GGCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AuthController(UserManager<User> userManager,
            ILogger<AuthController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            //if (ModelState.IsValid)
            //{
            //    var existingUser = await _userManager.FindByEmailAsync(userDTO.Email);
            //    if (existingUser is not null)
            //    {
            //        return BadRequest(new RegisterUserResponseDTO()
            //        {
            //            Errors = new List<string>()
            //            {
            //                "Email already in use"
            //            },
            //            Success = false
            //        });
            //    }

            //    var newUser = new User()
            //    {
            //        Email = userDTO.Email,
            //        UserName = userDTO.UserName
            //    };
            //    var isCreated = await _userManager.CreateAsync(newUser, userDTO.Password);
            //    if (isCreated.Succeeded)
            //    {

            //        return Ok(new RegisterUserResponseDTO
            //        {
            //            Success = true
            //        });
            //    }
            //    else
            //    {
            //        return BadRequest(new RegisterUserResponseDTO()
            //        {
            //            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
            //            Success = false
            //        });
            //    }
            //}

            //return BadRequest(new RegisterUserResponseDTO()
            //{
            //    Errors = new List<string>()
            //    {
            //        "Invalid payload"
            //    },
            //    Success = false
            //});

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Smth went wrong in the {nameof(Register)}");
                return Problem("Internal Server Error. Please try again later.", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized(userDTO);
                }

                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Smth went wrong in the {nameof(Login)}");
                return Problem("Internal Server Error. Please try again later.", statusCode: 500);
            }
        }
    }
}
