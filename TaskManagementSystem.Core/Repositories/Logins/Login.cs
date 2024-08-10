using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.OtherServices;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.Logins
{
    public class Login : ILogin
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpCotext;
        public Login(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _configuration = configuration;
            _httpCotext = httpContext;
        }
        public async Task<ReturnObject<Users>> LoginAsync(LoginModel model)
        {
            try
            {
                Users? users = await _context.Users
                    .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                    .FirstOrDefaultAsync();
                if(users != null)
                {
                    string token = Token.generateAccessToken(users, _configuration, _context);
                    _httpCotext.HttpContext?.Response.Cookies.Append("Token", token);
                    _httpCotext.HttpContext?.Response.Cookies.Append("userId", users.Id.ToString());
                    string refreshToken = Token.generateRefreshToken();
                    try
                    {
                        var tokenData = new RefreshTokens
                        {
                            UserId = users.Id,
                            RefreshToken = refreshToken,
                            ExpiryTime = DateTime.Now.AddMinutes(120)
                        };
                        _context.RefreshTokens.Add(tokenData);
                        _context.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        return new ReturnObject<Users>()
                        {
                            IsSuccess = false,
                            ErrorMessage = ex.Message,
                        };
                    }
                    return new ReturnObject<Users>()
                    {
                        IsSuccess = true,
                        Result = users
                    };
                }
                else
                {
                    return new ReturnObject<Users>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Username OR Password",
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<Users>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public async Task<ReturnObject<List<string>>?> GetPermission(int id)
        {
            try
            {
                List<string>? permissionData = await _context.Database.SqlQuery<String>($"exec getPermissions {id}").ToListAsync();
                return new ReturnObject<List<string>>()
                {
                    IsSuccess = true,
                    Result = permissionData
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<string>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
