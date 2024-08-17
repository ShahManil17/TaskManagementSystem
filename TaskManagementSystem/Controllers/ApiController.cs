using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Collections.Generic;
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

        [HttpGet("getUserTask")]
        public async Task<ReturnObject<List<int?>>> GetUserTask(int userId)
        {
            try
            {
                List<int?> counts = new List<int?>();

                List<string> statuses = new List<string>() { "todo", "in-progress", "bug", "error", "completed" };
                foreach (var item in statuses)
                {
                    ReturnObject<int?> response = await _manager.GetStatusCount(userId, item);
                    if(response.IsSuccess)
                    {
                        counts.Add(response.Result);
                    }
                    else
                    {
                        return new ReturnObject<List<int?>>()
                        {
                            IsSuccess = false,
                            ErrorMessage = response.ErrorMessage != null ? response.ErrorMessage : "Something Went Wrong Getting the Task Status!"
                        };
                    }
                }
                return new ReturnObject<List<int?>>()
                {
                    IsSuccess = true,
                    Result = counts
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<List<int?>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpGet("getDesplayDetails")]
        public async Task<ReturnObject<TaskDisplayModel>> GetDesplayDetails(int taskId)
        {
            return await _manager.GetDesplayDetails(taskId);
        }

        [HttpGet("getFilterTasks")]
        public async Task<ReturnObject<List<AddTaskModel>>> GetFilterTasks(string column, string argument)
        {
            ReturnObject<List<AddTaskModel>> taskResponse = await _manager.GetAllTasks();
            if (!taskResponse.IsSuccess)
            {
                return taskResponse;
            }
            else
            {
                if(taskResponse.Result == null || taskResponse.Result.Count  == 0)
                {
                    return new ReturnObject<List<AddTaskModel>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Tasks Found!"
                    };
                }
                else if(column == "status")
                {
                    List<AddTaskModel> returnResult = taskResponse.Result
                        .Where(x => x.Status?.ToLower() == argument)
                        .ToList();
                    if (returnResult == null || returnResult.Count == 0)
                    {
                        return new ReturnObject<List<AddTaskModel>>()
                        {
                            IsSuccess = false,
                            ErrorMessage = $"No Data Found With The Status : {argument}!"
                        };
                    }
                    else
                    {
                        return new ReturnObject<List<AddTaskModel>>()
                        {
                            IsSuccess = true,
                            Result = returnResult
                        };
                    }
                }
                else
                {
                    List<AddTaskModel> returnResult = taskResponse.Result
                        .Where(x => x.TaskName==null ? x.TaskName=="" : x.TaskName.ToLower().Contains(argument))
                        .ToList();
                    if (returnResult == null || returnResult.Count == 0)
                    {
                        return new ReturnObject<List<AddTaskModel>>()
                        {
                            IsSuccess = false,
                            ErrorMessage = $"No Task Found With The Name : {argument}!"
                        };
                    }
                    else
                    {
                        return new ReturnObject<List<AddTaskModel>>()
                        {
                            IsSuccess = true,
                            Result = returnResult
                        };
                    }
                }
            }
        }

    }
}
