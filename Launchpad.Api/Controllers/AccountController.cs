using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client; //IdentityModel is a .NET standard helper library for claims-based identity, OAuth 2.0 and OpenID Connect
using Launchpad.App.Repositories.Interfaces;
using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Launchpad.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountController(SignInManager<User> signInManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]

        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            //validate model
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            //validate user login (AspNetCore.Identity does this)
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false).ConfigureAwait(false);

            //don't lockout user for now
            if (!result.Succeeded)
            {
                return BadRequest("Username/password invalid");
            }

            //get user profile
            var user = await _userRepository.GetUserByEmail(login.Email).ConfigureAwait(false);

            //get a token from identity server
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                //call identity server
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = authority + "/connect/token",
                    UserName = login.Email,
                    Password = login.Password,
                    ClientId = login.ClientId,
                    ClientSecret = "oVfHFTfwM3wjH826GN6fgIJUJ370cmzj",
                    Scope = "launchpadapi.scope"
                }).ConfigureAwait(false);

                if (tokenResponse.IsError)
                {
                    return BadRequest("Cannot grant access to user account");
                }

                return Ok(new LoginResponseVM(tokenResponse, user));

            }

        }
    }
}
