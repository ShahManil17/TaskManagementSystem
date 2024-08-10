using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.DataModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagementSystem.Core.Repositories.AdminServices
{
    public class Admin : IAdmin
    {
        private readonly ApplicationDbContext _context;
        public Admin(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReturnObject<List<Roles>>> GetRoles()
        {
            try
            {
                List<Roles>? RoleData = await _context.Roles.FromSql($"exec getRoles").ToListAsync();
                return new ReturnObject<List<Roles>>()
                {
                    IsSuccess = true,
                    Result = RoleData != null && RoleData.Any() ? RoleData : null
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<List<Roles>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<Users>> AddUser(AddUserModel model)
        {
            try
            {
                Users insertedData = new Users()
                {
                    UserName = model.UserName,
                    Country = "India",
                    Password = model.Password,
                    IsDeleted = 0,
                    CreatedAt = DateTime.Now,
                    RoleId = model.RoleId
                };
                await _context.Users.AddAsync(insertedData);
                await _context.SaveChangesAsync();
                return new ReturnObject<Users>()
                {
                    IsSuccess = true,
                    Result = insertedData
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<Users>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<Users>>> GetUsers()
        {
            try
            {
                List<Users>? UserData = await _context.Users.FromSql($"exec getUsers").ToListAsync();
                return new ReturnObject<List<Users>>()
                {
                    IsSuccess = true,
                    Result = UserData != null && UserData.Any() ? UserData : null
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<Users>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<int?>> GetRoleId(int userId)
        {
            try
            {
                int? id = await _context.Users
                    .Where(x => x.Id == userId)
                    .Select(x => x.RoleId)
                    .FirstOrDefaultAsync();
                if(id == null)
                {
                    return new ReturnObject<int?>()
                    {
                        IsSuccess = true,
                        ErrorMessage = "Can't Find The User"
                    };
                }
                else
                {
                    return new ReturnObject<int?>()
                    {
                        IsSuccess = true,
                        Result = id
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<int?>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<Users>> ChangeRole(int? userId, int? roleId)
        {
            try
            {
                Users? userData = await _context.Users
                    .Where(x => x.Id == userId)
                    .FirstOrDefaultAsync();
                if(userData != null)
                {
                    userData.RoleId = roleId;
                    _context.Users.Update(userData);
                    await _context.SaveChangesAsync();
                    return new ReturnObject<Users>()
                    {
                        IsSuccess = true,
                        Result = userData
                    };
                }
                else
                {
                    return new ReturnObject<Users>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No User Found!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<Users>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<Permissions>>> GetPermissions()
        {
            try
            {
                List<Permissions>? PermissionData = await _context.Permissions.FromSql($"exec getAllPermissions").ToListAsync();
                if(PermissionData != null && PermissionData.Any())
                {
                    return new ReturnObject<List<Permissions>>()
                    {
                        IsSuccess = true,
                        Result = PermissionData
                    };
                }
                else
                {
                    return new ReturnObject<List<Permissions>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Please Enter Permissions!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<Permissions>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<int>>> GetRoleHasPermissions(int roleId)
        {
            try
            {
                List<int> permissionsData = await _context.Database.SqlQuery<int>($"exec getRoleHasPermissions {roleId}").ToListAsync();
                if (permissionsData.Any())
                {
                    return new ReturnObject<List<int>>()
                    {
                        IsSuccess = true,
                        Result = permissionsData
                    };
                }
                else
                {
                    return new ReturnObject<List<int>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Permissions Found!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<int>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<RoleHasPermissions>>> ChangePermissions(AssignPermissionModel model)
        {
            try
            {
                List<RoleHasPermissions> dataToDelete = await _context.RoleHasPermission
                    .Where(x => x.RoleId == model.RoleId)
                    .ToListAsync();
                List<RoleHasPermissions> dataToAdd = new List<RoleHasPermissions>();
                if(model.Permissions != null && model.Permissions.Any())
                {
                    _context.RoleHasPermission.RemoveRange(dataToDelete);
                    foreach (var item in model.Permissions)
                    {
                        RoleHasPermissions data = new RoleHasPermissions()
                        {
                            RoleId = model.RoleId,
                            PermissionId = item
                        };
                        await _context.RoleHasPermission.AddAsync(data);
                        dataToAdd.Add(data);
                    }
                }
                await _context.SaveChangesAsync();
                return new ReturnObject<List<RoleHasPermissions>>()
                {
                    IsSuccess = true,
                    Result = dataToAdd
                };
            }
            catch(Exception ex) 
            {
                return new ReturnObject<List<RoleHasPermissions>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
