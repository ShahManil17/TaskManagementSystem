using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getPermissions]
            @id INT
            AS
            BEGIN
	            SELECT Permissions.Name FROM Permissions
	            INNER JOIN RoleHasPermission AS rhp ON Permissions.Id = rhp.PermissionId
	            WHERE rhp.RoleId = (SELECT RoleId FROM Users WHERE Id = @id)
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
