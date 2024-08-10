using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.Logins
{
    public interface ILogin
    {
        public Task<ReturnObject<Users>> LoginAsync(LoginModel model);
        public Task<ReturnObject<List<string>>?> GetPermission(int id);
    }
}
