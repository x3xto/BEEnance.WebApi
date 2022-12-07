﻿using Expenses.Core;
using Expenses.Core.CustomExceptions;
using Expenses.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Expenses.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                var result = await _userService.SignUp(user);
                return Created("", result);
            }
            catch (UsernameAlreadyExistsException e)
            {
                return StatusCode(409, e.Message); // status code 409 - Conflict
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
        {
            try
            {
                var result = await _userService.SignIn(user);
                return Ok(result);
            }
            catch (InvalidUsernamePasswordException e)
            {
                return StatusCode(401, e.Message); // status code 401 - Unauthorized
            }
        }

        [HttpPost("google")] // ДОДАЙ ГУГЛ АЙДІ В LaunchSettings
        public async Task<ActionResult> GoogleSignIn([FromQuery] string token)
        {
            var payload = await ValidateAsync(token, new ValidationSettings
            {
                Audience = new[]
                {
                    Environment.GetEnvironmentVariable("CLIENT_ID")
                }
            });

            var result = await _userService.ExternalSignIn(new DB.User
            {
                Email = payload.Email,
                ExternalId = payload.Subject,
                ExternalType = "GOOGLE"
            });

            return Created("", result);
        }
    }
}
