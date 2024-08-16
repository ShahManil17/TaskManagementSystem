using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.DTOs
{
    public class RequestViewModel
    {
        public int? Id { get; set; }
        public int? SubTaskId { get; set; }
        public int? RequestedId { get; set; }
        public int? AcceptedId { get; set; }
        public string? RequestedStatus { get; set; }
        public int? IsOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? TaskName { get; set; }
    }
}
