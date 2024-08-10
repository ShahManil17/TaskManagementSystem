using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.DTOs
{
    public class UserWithTaskModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        [NotMapped]
        public List<InTask>? InTask {  get; set; }
    }

}
