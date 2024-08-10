using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.DataModels
{
    public class NotificationStatus
    {
        [Key]
        public int Id { get; set; }
        public int? NotificationId { get; set; }
        public virtual Notifications? Notification { get; set; }
        public int IsRead { get; set; } = 0;
        public int IsDelivered { get; set; } = 0;
        public int? ReceiverId { get; set; }
        public virtual Users? Receiver { get; set; }
    }
}
