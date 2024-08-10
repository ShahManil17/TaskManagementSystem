using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class RoleHasPermissions
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public virtual Roles? Role { get; set; }
        public int? PermissionId { get; set; }
        public virtual Permissions? Permission { get; set; }
    }
}
