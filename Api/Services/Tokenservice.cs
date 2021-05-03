using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Entities;
using Api.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class Tokenservece : ITokenservice
    {
        private readonly SymmetricSecurityKey _key;
        public Tokenservece(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUsers users)
        {
           var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.NameIdentifier,users.UserName),
             new Claim(JwtRegisteredClaimNames.NameId,users.UserName)
           };

           var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
           
           var tokenDescription = new SecurityTokenDescriptor{
              Subject = new ClaimsIdentity(claims),
              Expires = DateTime.Now.AddDays(7),
              SigningCredentials = creds
           };

           var tokenHandler = new JwtSecurityTokenHandler();

           var token =tokenHandler.CreateToken(tokenDescription);

           return tokenHandler.WriteToken(token);
        }
    }
}