﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.DTOs.Auth;
using TaskManager.Core.Application.DTOs.JWT;
using TaskManager.Infraestructure.Identity.Interfaces.Services;

namespace TaskManager.WebApi.Controllers.v1.Auth
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IjwtService _jwtService;

        public AuthController(UserManager<IdentityUser> userManager, IjwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegisterResponseDTO()
                    {
                        Errors = new List<string>() { "Email already Registered" },
                        Success = false
                    });
                }

                IdentityUser newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.Username,
                };

                IdentityResult? created = await _userManager.CreateAsync(newUser, user.Password);
                if (created.Succeeded)
                {
                    AuthResult authResult = await _jwtService.GenerateToken(newUser);
                    return Ok(authResult);
                }
                else
                {
                    return BadRequest(new RegisterResponseDTO()
                    {
                        Errors = created.Errors.Select(e => e.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new RegisterResponseDTO()
            {
                Errors = new List<string>() { "Invalid payload" },
                Success = false
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginUserDTO user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegisterResponseDTO()
                    {
                        Errors = new List<string>() { "Email address is not registered." },
                        Success = false
                    });
                }

                bool isUserCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (isUserCorrect)
                {
                    AuthResult authResult = await _jwtService.GenerateToken(existingUser);
                    return Ok(authResult);
                }
                else
                {
                    return BadRequest(new RegisterResponseDTO()
                    {
                        Errors = new List<string>() { "Wrong password" },
                        Success = false
                    });
                }
            }

            return BadRequest(new RegisterResponseDTO()
            {
                Errors = new List<string>() { "Invalid payload" },
                Success = false
            });
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var verified = await _jwtService.VerifyToken(tokenRequest);
                if (!verified.Success)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = verified.Errors,
                        Success = false
                    });
                }

                var tokenUser = await _userManager.FindByIdAsync(verified.UserId);
                AuthResult authResult = await _jwtService.GenerateToken(tokenUser);
                return Ok(authResult);


            }

            return BadRequest(new AuthResult()
            {
                Errors = new List<string> { "invalid Payload" },
                Success = false
            });



        }
    }
}
