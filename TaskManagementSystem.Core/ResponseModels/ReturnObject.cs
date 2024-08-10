using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs;

namespace TaskManagementSystem.Core.ResponseModels
{
    public class ReturnObject<T>
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
    }
}
