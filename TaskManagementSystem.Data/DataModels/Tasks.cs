using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public int? HasSubTask { get; set; }
        public string? Status { get; set; }
        public int? CreatedById { get; set; }
        public virtual Users? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
