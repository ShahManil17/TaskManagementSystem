using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.UserServices
{
    public interface IUser
    {
        public Task<ReturnObject<Requests>> PostRequest(StatusRequest model);
    }
}
