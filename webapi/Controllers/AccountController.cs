using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using webapi.DTOs;
using webapi.Entities;
using webapi.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        private readonly UserManager<AppUser> userManager;

        private readonly ITokenServices tokenServices;

        private readonly IMapper mapper;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenServices tokenServices, IMapper mapper)
        {
            this.signInManager = signInManager;

            this.userManager = userManager;

            this.tokenServices = tokenServices;

            this.mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await CheckUserExist(registerDto.UserName, registerDto.Email)) return BadRequest("Email or username is taken!");

            var user = mapper.Map<AppUser>(registerDto);

            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await userManager.AddToRoleAsync(user, "Member");

            if(!roleResult.Succeeded) return BadRequest(result.Errors);

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

    }
}

