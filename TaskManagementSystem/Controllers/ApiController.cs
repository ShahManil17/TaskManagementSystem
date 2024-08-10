using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Repositories.AdminServices;
using TaskManagementSystem.Core.Repositories.ManagerServices;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Controllers
{
    [Route("[controller]")]
    public class ApiController : Controller
    {
        private readonly IAdmin _admin;
        private readonly IManager _manager;
        public ApiController(IAdmin admin, IManager manager)
        {
            _admin = admin;
            _manager = manager;
        }

        [HttpGet("getRoleId")]
        public async Task<ReturnObject<int?>> GetRoleId(int userId)
        {
            return await _admin.GetRoleId(userId);
        }

        [HttpGet("getRoleHasPermissions")]
        public async Task<ReturnObject<List<int>>> GetRoleHasPermissions(int roleId)
        {
            return await _admin.GetRoleHasPermissions(roleId);
        }

        [HttpGet("updateUser")]
        public async Task<ReturnObject<UserHasTask>> UpdateUser(int taskId, int? userId)
        {
            if (userId == null)
            {
                return await _manager.RemoveUser(taskId);
            }
            else
            {
                return await _manager.AddUser(taskId, userId);
            }
        }
    }
}
