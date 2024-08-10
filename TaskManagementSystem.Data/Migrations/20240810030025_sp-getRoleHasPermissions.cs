using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetRoleHasPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getRoleHasPermissions] @id INT
            AS
            BEGIN
	            SELECT Permissions.Id FROM Permissions
	            INNER JOIN RoleHasPermission AS rhp ON Permissions.Id = rhp.PermissionId
	            WHERE RHP.RoleId = @id;
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
