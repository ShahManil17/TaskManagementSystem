using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.DataModels;

namespace TaskManagementSystem.Core.DTOs
{
    public class SubTaskModel
    {
        public int? Id { get; set; }
        public int? TaskId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public List<Users>? Users { get; set; }
    }
}
