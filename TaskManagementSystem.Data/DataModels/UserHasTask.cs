using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class UserHasTask
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual Users? User { get; set; }
        public int? TaskId { get; set; }
        public virtual SubTask? Task { get; set; }
    }
}
