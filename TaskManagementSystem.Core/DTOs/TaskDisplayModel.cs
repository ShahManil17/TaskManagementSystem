using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.DTOs
{
    public class TaskDisplayModel
    {
        public int? Id { get; set; }
        [Required]
        public string? TaskName { get; set; }
        [Required]
        public int? HasSubTask { get; set; }
        public string? Status { get; set; }
        public int? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public List<SubTaskModel>? SubTasks { get; set; }
        [NotMapped]
        public List<UserViewModel>? Users { get; set; }
    }
}
