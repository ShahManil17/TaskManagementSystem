using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.DataModels;
using TaskManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Core.OtherServices
{
    public class Token
    {
        public static string generateAccessToken(Users userData, IConfiguration _configuration, ApplicationDbContext _context)
        {
            Console.WriteLine(_configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Setting Permissions in Claims
            var clm = new List<Claim>();
            List<string> permissions = _context.Database.SqlQuery<string>($"exec getPermissions {userData.Id}").ToList();
            if (permissions.Any())
            {
                foreach ( var item in permissions)
                {

                    clm.Add(new Claim(item, item));
                }
            }

            var Sectoken = new JwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims: clm,
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(Sectoken).ToString();
        }

        public static string generateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
