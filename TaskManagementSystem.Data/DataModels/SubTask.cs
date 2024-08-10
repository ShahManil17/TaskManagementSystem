using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class SubTask
    {
        [Key]
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public virtual Tasks? Task { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
