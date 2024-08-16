using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.QAServices
{
    public class QA : IQA
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public QA(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public async Task<ReturnObject<List<RequestViewModel>?>> GetRequests(int testerId)
        {
            try
            {
                List<RequestViewModel>? data = await _context.Database.SqlQuery<RequestViewModel>($"exec getRequests {testerId}").ToListAsync();
                if (data != null && data.Any())
                {
                    return new ReturnObject<List<RequestViewModel>?>()
                    {
                        IsSuccess = true,
                        Result = data
                    };
                }
                else
                {
                    return new ReturnObject<List<RequestViewModel>?>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "No Pending Requests!"
                    };
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<List<RequestViewModel>?>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ReturnObject<SubTask>?> ChangeStatus(int requestId, int? subTaskId, string? status)
        {
            try
            {
                Requests? requestData = await _context.Requests
                    .Where(x => x.Id == requestId)
                    .FirstOrDefaultAsync();
                if (requestData == null)
                {
                    return new ReturnObject<SubTask>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Something Went Wrong Getting The Request Data!"
                    };
                }
                else
                {
                    requestData.IsOpen = 0;
                    requestData.UpdatedAt = DateTime.Now;
                    if (subTaskId == null)
                    {
                        // Reject Request
                        _context.Requests.Update(requestData);
                        await _context.SaveChangesAsync();
                        return new ReturnObject<SubTask>()
                        {
                            IsSuccess = true
                        };
                    }
                    else 
                    {
                        // Accept Request
                        requestData.AcceptedId = Convert.ToInt32(_contextAccessor.HttpContext?.Request.Cookies["userId"]);

                        SubTask? subTaskData = await _context.SubTask
                            .Where(x => x.Id == subTaskId)
                            .FirstOrDefaultAsync();
                        if(subTaskData == null)
                        {
                            return new ReturnObject<SubTask>()
                            {
                                IsSuccess = false,
                                ErrorMessage = "Can't Find The Task"
                            };
                        }
                        subTaskData.Status = status;
                        _context.SubTask.Update(subTaskData);
                        _context.Requests.Update(requestData);
                        await _context.SaveChangesAsync();
                        return new ReturnObject<SubTask>
                        {
                            IsSuccess = true,
                            Result = subTaskData
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                return new ReturnObject<SubTask>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
