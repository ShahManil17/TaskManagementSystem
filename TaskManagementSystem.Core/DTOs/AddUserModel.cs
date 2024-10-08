﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.DTOs
{
    public class AddUserModel
    {
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string? Email {  get; set; }

        [Required]
        public string? Password { get; set; }
        
        [Required]
        public int? RoleId { get; set; }
    }
}
