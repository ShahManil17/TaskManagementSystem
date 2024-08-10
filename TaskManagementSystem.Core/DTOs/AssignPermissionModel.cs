namespace TaskManagementSystem.Core.DTOs
{
    public class AssignPermissionModel
    {
        public int? RoleId { get; set; }
        public List<int>? Permissions { get; set; }
    }
}
