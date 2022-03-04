using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly SignInManager<AppUser> signInManager;

        private readonly UserManager<AppUser> userManager;

        private readonly ITokenServices tokenServices;

        private readonly IMapper mapper;

        private readonly IConfiguration configuration;

        public AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        ITokenServices tokenServices, IMapper mapper,
        IConfiguration configuration)
        {
            this.signInManager = signInManager;

            this.userManager = userManager;

            this.tokenServices = tokenServices;

            this.mapper = mapper;

            this.configuration = configuration;
        }

        [HttpPost("regist")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await CheckUserExist(registerDto.UserName, registerDto.Email)) return BadRequest("Email or username is taken!");

            var user = mapper.Map<AppUser>(registerDto);

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                UserName = user.UserName,
                Gender = user.Gender,
                NickName = user.NickName,
                Token = await tokenServices.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Account);

            if (user == null) return Unauthorized("User is not exist");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Login failed");

            return new UserDto
            {
                UserName = user.UserName,
                Gender = user.Gender,
                NickName = user.NickName,
                Token = await tokenServices.CreateToken(user)
            };
        }

        private async Task<bool> CheckUserExist(string username, string useremail)
        {
            var hasUser = await userManager.Users.AnyAsync(n => n.UserName == username);

            var hasEmail = await userManager.Users.AnyAsync(n => n.Email == useremail);

            return hasUser || hasEmail;
        }

        [HttpPost("extra-signin")]
        public async Task<ActionResult<UserDto>> SignInGoogle(ExternalAuthDto externalAuthDto)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { configuration["Authentication:Google:ClientId"] } 
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuthDto.IdToken, settings);

            if (payload == null) return BadRequest("Auth fail");

            var info = new UserLoginInfo(externalAuthDto.Provider, payload.Subject, externalAuthDto.Provider);

            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if(user == null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);

                if(user == null)
                {
                    user = new AppUser { Email = payload.Email, UserName = payload.Email, Gender = "", NickName = "" };

                    await userManager.CreateAsync(user);
                    
                    await userManager.AddToRoleAsync(user, "Member");

                    await userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null) return BadRequest("Auth fail");

            return new UserDto
            {
                UserName = user.UserName,
                Gender = user.Gender == null ? "" : user.Gender,
                NickName = user.NickName == null ? "" : user.NickName,
                Token = await tokenServices.CreateToken(user)
            };
        }

    }
}

