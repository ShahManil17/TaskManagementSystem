using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Data.DataModels
{
    public class Requests
    {
        [Key]
        public int? Id { get; set; }
        public int? SubTaskId { get; set; }
        public SubTask? SubTask { get; set; }
        public int? RequestedId { get; set; }
        public Users? Requested { get; set; }
        public int? AcceptedId { get; set; }
        public Users? Accepted { get; set; }
        public string? RequestedStatus { get; set; }
        public int? IsOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
