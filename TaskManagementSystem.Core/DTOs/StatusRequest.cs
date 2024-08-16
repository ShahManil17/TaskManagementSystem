using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.DTOs
{
    public class StatusRequest
    {
        public int? UserId {  get; set; }
        public int? SubTaskId { get; set; }
        public string? RequestStatus { get; set; }
    }
}
