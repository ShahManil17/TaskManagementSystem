using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.QAServices
{
    public interface IQA
    {
        public Task<ReturnObject<List<RequestViewModel>?>> GetRequests(int testerId);
        public Task<ReturnObject<SubTask>?> ChangeStatus(int requestId, int? subTaskId, string? status);
    }
}
