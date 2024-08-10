using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? Password { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? RoleId { get; set; }
        public virtual Roles? Role { get; set; }
    }
}
