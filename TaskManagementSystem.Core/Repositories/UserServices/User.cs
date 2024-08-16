using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.ResponseModels;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.Repositories.UserServices
{
    public class User : IUser
    {
        private readonly ApplicationDbContext _context;
        public User(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }
        public async Task<ReturnObject<Requests>> PostRequest(StatusRequest model)
        {
            try
            {
                Requests insertdData = new Requests()
                {
                    SubTaskId = model.SubTaskId,
                    RequestedId = model.UserId,
                    RequestedStatus = model.RequestStatus,
                    IsOpen = 1,
                    CreatedAt = DateTime.Now
                };
                await _context.Requests.AddAsync(insertdData);
                await _context.SaveChangesAsync();
                return new ReturnObject<Requests>()
                {
                    IsSuccess = true,
                    Result = insertdData
                };
            }
            catch(Exception ex)
            {
                return new ReturnObject<Requests>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
