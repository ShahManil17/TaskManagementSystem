using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.AdminServices
{
    public interface IAdmin
    {
        public Task<ReturnObject<List<Roles>>> GetRoles();
        public Task<ReturnObject<Users>> AddUser(AddUserModel model);
        public Task<ReturnObject<List<Users>>> GetUsers();
        public Task<ReturnObject<int?>> GetRoleId(int userId);
        public Task<ReturnObject<Users>> ChangeRole(int? userId, int? roleId);
        public Task<ReturnObject<List<Permissions>>> GetPermissions();
        public Task<ReturnObject<List<int>>> GetRoleHasPermissions(int roleId);
        public Task<ReturnObject<List<RoleHasPermissions>>> ChangePermissions(AssignPermissionModel model);
    }
}
