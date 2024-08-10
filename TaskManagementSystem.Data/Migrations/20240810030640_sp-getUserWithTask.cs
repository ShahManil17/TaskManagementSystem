using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetUserWithTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getUserWithTask] @id INT
            AS
            BEGIN
	            SELECT (
		            SELECT Id, UserName, 
		            (
			            SELECT Id FROM UserHasTask
			            WHERE UserHasTask.UserId = Users.Id AND UserHasTask.TaskId = (SELECT Id FROM SubTask WHERE TaskId = @id)
			            FOR JSON PATH
		            )AS InTask
		            FROM Users
		            FOR JSON PATH
	            )
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
