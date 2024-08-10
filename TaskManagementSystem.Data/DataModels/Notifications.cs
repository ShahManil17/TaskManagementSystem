using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public int? TaskId { get; set; }
        public virtual SubTask? Task { get; set; }
        public int? SenderId { get; set; }
        public virtual Users? Sender { get; set; }
    }
}
