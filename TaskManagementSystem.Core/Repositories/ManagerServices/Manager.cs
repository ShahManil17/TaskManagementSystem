using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.ManagerServices
{
    public class Manager : IManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Manager(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReturnObject<string>> AddTask(AddTaskModel model)
        {
            int? taskId = null;
            try
            {
                Tasks insertData = new Tasks()
                {
                    TaskName = model.TaskName,
                    HasSubTask = model.HasSubTask,
                    Status = "Todo",
                    CreatedById = Convert.ToInt32(_httpContextAccessor.HttpContext?.Request.Cookies["userId"]),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _context.Tasks.AddAsync(insertData);
                await _context.SaveChangesAsync();
                taskId = insertData.Id;
                if (model.SubTasks != null && model.SubTasks.Any())
                {
                    foreach (var item in model.SubTasks)
                    {
                        SubTask subTask = new SubTask()
                        {
                            TaskId = insertData.Id,
                            Name = item.Name,
                            Status = item.Status
                        };
                        await _context.SubTask.AddAsync(subTask);
                    }
                }
                await _context.SaveChangesAsync();
                return new ReturnObject<string>()
                {
                    IsSuccess = true
                };
            }
            catch(Exception ex)
            {
                if(taskId != null)
                {
                    try
                    {
                        Tasks? data = await _context.Tasks
                        .Where(x => x.Id == taskId)
                        .FirstOrDefaultAsync();
                        if (data != null)
                        {
                            _context.Tasks.Remove(data);
                            await _context.SaveChangesAsync();
                        }
                        return new ReturnObject<string>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "Something Went Wrong Inserting the SubTask!"
                        };
                    }
                    catch(Exception innerEx)
                    {
                        return new ReturnObject<string>()
                        {
                            IsSuccess = false,
                            ErrorMessage = innerEx.Message
                        };
                    }
                    
                }
                return new ReturnObject<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<AddTaskModel>>> GetAllTasks()
        {
            try
            {
                List<string>? stringData = await _context.Database.SqlQuery<string>($"exec getAllTaskDetails").ToListAsync();
                List<AddTaskModel>? allTaskData = JsonSerializer.Deserialize<List<AddTaskModel>>(stringData.First());
                if (allTaskData != null && allTaskData.Any())
                {
                    return new ReturnObject<List<AddTaskModel>>()
                    {
                        IsSuccess = true,
                        Result = allTaskData
                    };
                }
                else
                {
                    return new ReturnObject<List<AddTaskModel>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Data Found!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<AddTaskModel>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<AddTaskModel>> GetOneTasks(int? id)
        {
            try
            {
                List<string>? stringData = await _context.Database.SqlQuery<string>($"exec getOneTask {id}").ToListAsync();
                List<AddTaskModel>? data = JsonSerializer.Deserialize<List<AddTaskModel>>(stringData.First());
                if(data != null)
                {
                    if (data[0] != null)
                    {
                        return new ReturnObject<AddTaskModel>()
                        {
                            IsSuccess = true,
                            Result = data[0]
                        };
                    }
                    else
                    {
                        return new ReturnObject<AddTaskModel>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "Can't Find The Task With She Specified Id!"
                        };
                    }
                }
                else
                {
                    return new ReturnObject<AddTaskModel>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Can't Find The Task With She Specified Id!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<AddTaskModel>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<string>> Updatetask(AddTaskModel model)
        {
            try
            {
                Tasks? taskInsert = await _context.Tasks
                    .Where(x => x.Id == model.Id)
                    .FirstOrDefaultAsync();
                if(taskInsert == null)
                {
                    return new ReturnObject<string>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Task Data Entered!"
                    };
                }
                else
                {
                    taskInsert.TaskName = model.TaskName;
                    taskInsert.Status = model.Status;
                    taskInsert.UpdatedAt = DateTime.Now;
                    _context.Tasks.Update(taskInsert);
                }
                if (model.DeleteIds != null && model.DeleteIds.Any())
                {
                    foreach (var item in model.DeleteIds)
                    {
                        SubTask? dataToDelete = await _context.SubTask
                            .Where(x => x.Id == item)
                            .FirstOrDefaultAsync();
                        if (dataToDelete != null)
                        {
                            _context.SubTask.Remove(dataToDelete);
                        }
                    }
                }
                if (model.SubTasks != null && model.SubTasks.Any())
                {
                    foreach (var item in model.SubTasks)
                    {
                        if (item.Id != null)
                        {
                            SubTask? updateData = await _context.SubTask
                                .Where(x => x.Id == item.Id)
                                .FirstOrDefaultAsync();
                            if(updateData != null)
                            {
                                updateData.Name = item.Name;
                                updateData.Status = item.Status;
                                _context.SubTask.Update(updateData);
                            }
                            else
                            {
                                return new ReturnObject<string>()
                                {
                                    IsSuccess = false,
                                    ErrorMessage = "Invalid data entered in SubTask entered!"
                                };
                            }
                        }
                        else
                        {
                            SubTask insertData = new SubTask()
                            {
                                TaskId = model.Id,
                                Name = item.Name,
                                Status = item.Status
                            };
                            await _context.SubTask.AddAsync(insertData);
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return new ReturnObject<string>()
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<SubTask>>> GetSubTasks(int? id)
        {
            try
            {
                if(id != null)
                {
                    List<SubTask>? data =  await _context.SubTask.FromSql($"exec getSubTasks {id}").ToListAsync();
                    if(data != null && data.Any())
                    {
                        return new ReturnObject<List<SubTask>>()
                        {
                            IsSuccess = true,
                            Result = data
                        };
                    }
                    else
                    {
                        return new ReturnObject<List<SubTask>>()
                        {
                            IsSuccess = false,
                            ErrorMessage = "Invalid Task Id Got!"
                        };
                    }
                }
                else
                {
                    return new ReturnObject<List<SubTask>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Task Id Got!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<SubTask>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<UserWithTaskModel>>> GetUserWithSubtask(int? id)
        {
            try
            {
                List<string>? stringData = await _context.Database.SqlQuery<string>($"exec getUserWithSubTask {id}").ToListAsync();
                List<UserWithTaskModel>? allTaskData = JsonSerializer.Deserialize<List<UserWithTaskModel>>(stringData.First());
                return new ReturnObject<List<UserWithTaskModel>>()
                {
                    IsSuccess = true,
                    Result = allTaskData
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<UserWithTaskModel>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<UserWithTaskModel>>> GetUserWithTask(int? id)
        {
            try
            {
                List<string>? stringData = await _context.Database.SqlQuery<string>($"exec getUserWithTask {id}").ToListAsync();
                List<UserWithTaskModel>? allTaskData = JsonSerializer.Deserialize<List<UserWithTaskModel>>(stringData.First());
                return new ReturnObject<List<UserWithTaskModel>>()
                {
                    IsSuccess = true,
                    Result = allTaskData
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<List<UserWithTaskModel>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<UserHasTask>> RemoveUser(int taskId)
        {
            try
            {
                UserHasTask? subTask = await _context.UserHasTask
                    .Where(x => x.Id == taskId)
                    .FirstOrDefaultAsync();
                if (subTask == null)
                {
                    return new ReturnObject<UserHasTask>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid Task Details Entered!"
                    };
                }
                _context.UserHasTask.Remove(subTask);
                await _context.SaveChangesAsync();
                return new ReturnObject<UserHasTask>()
                {
                   IsSuccess = true,
                   Result = subTask
                };

            }
            catch(Exception ex)
            {
                return new ReturnObject<UserHasTask>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<UserHasTask>> AddUser(int taskId, int? userId)
        {
            try
            {
                UserHasTask userInsertData = new UserHasTask()
                {
                    UserId = userId,
                    TaskId = taskId
                };
                await _context.UserHasTask.AddAsync(userInsertData);
                await _context.SaveChangesAsync();
                return new ReturnObject<UserHasTask>()
                {
                    IsSuccess = true,
                    Result = userInsertData
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<UserHasTask>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<List<SubTask>>> GetUserHasSubTasks(int id)
        {
            try
            {
                List<SubTask>? data = await _context.SubTask.FromSql($"exec getUserHasSubTasks {id}").ToListAsync();
                if(data != null && data.Any())
                {
                    return new ReturnObject<List<SubTask>>()
                    {
                        IsSuccess = true,
                        Result = data
                    };
                }
                else
                {
                    return new ReturnObject<List<SubTask>>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Tasks Assigned!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<SubTask>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<string>> GetSubStatus(int? taskId)
        {
            try
            {
                string? status = await _context.SubTask
                    .Where(x => x.Id == taskId)
                    .Select(x => x.Status)
                    .FirstOrDefaultAsync();
                if(status != null)
                {
                    return new ReturnObject<string>()
                    {
                        IsSuccess = true,
                        Result = status
                    };
                }
                else
                {
                    return new ReturnObject<string>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Invalid SubTask Id!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<int?>> GetStatusCount(int userId, string status)
        {
            try
            {
                List<int> data = await _context.Database.SqlQuery<int>($"exec getStatusCount {userId}, {status}")
                    .ToListAsync();
                if (data.Any())
                {
                    return new ReturnObject<int?>()
                    {
                        IsSuccess = true,
                        Result = data[0]
                    };
                }
                else
                {
                    return new ReturnObject<int?>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Something Went Wrong Getting Status Counts!"
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

        public async Task<ReturnObject<TaskDisplayModel>> GetDesplayDetails(int taskId)
        {
            try
            {
                List<string>? stringData = await _context.Database.SqlQuery<string>($"exec getDesplayDetails {taskId}").ToListAsync();
                List<TaskDisplayModel>? data = JsonSerializer.Deserialize<List<TaskDisplayModel>>(stringData.First());
                if (data == null)
                {
                    return new ReturnObject<TaskDisplayModel>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Data Found!"
                    };
                }
                return new ReturnObject<TaskDisplayModel>()
                {
                    IsSuccess = true,
                    Result = data[0]
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<TaskDisplayModel>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
