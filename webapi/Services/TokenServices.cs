using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using webapi.Interfaces;

namespace webapi.Service
{
    public class TokenServices: ITokenServices
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public TokenServices(IConfiguration configuration)
        {
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT"]));
        }
    }
}