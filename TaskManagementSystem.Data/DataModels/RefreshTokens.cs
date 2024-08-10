using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Data.DataModels
{
    public class RefreshTokens
    {
        [Key]
        public int? Id { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public int? UserId { get; set; }
        public Users? User { get; set; }
    }
}
