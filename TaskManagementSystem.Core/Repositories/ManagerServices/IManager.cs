using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.ManagerServices
{
    public interface IManager
    {
        public Task<ReturnObject<string>> AddTask(AddTaskModel model);
        public Task<ReturnObject<List<AddTaskModel>>> GetAllTasks();
        public Task<ReturnObject<AddTaskModel>> GetOneTasks(int? id);
        public Task<ReturnObject<string>> Updatetask(AddTaskModel model);
        public Task<ReturnObject<List<SubTask>>> GetSubTasks(int? id);
        public Task<ReturnObject<List<UserWithTaskModel>>> GetUserWithSubtask(int? id);
        public Task<ReturnObject<List<UserWithTaskModel>>> GetUserWithTask(int? id);
        public Task<ReturnObject<UserHasTask>> RemoveUser(int taskId);
        public Task<ReturnObject<UserHasTask>> AddUser(int taskId, int? userId);
        public Task<ReturnObject<List<SubTask>>> GetUserHasSubTasks(int id);
        public Task<ReturnObject<string>> GetSubStatus(int? taskId);
    }
}
