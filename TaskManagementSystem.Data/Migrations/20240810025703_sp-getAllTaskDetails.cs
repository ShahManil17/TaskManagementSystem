using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetAllTaskDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getAllTaskDetails]
            AS
            BEGIN
	            SELECT (
	            SELECT *, 
		            (
			            SELECT *, (
				            SELECT * FROM Users WHERE Users.Id IN (SELECT UserId FROM UserHasTask WHERE TaskId = SubTask.Id)
				            FOR JSON PATH
			            )AS Users
			            FROM SubTask WHERE SubTask.TaskId = Tasks.Id FOR JSON PATH
		            ) AS SubTasks
	            FROM Tasks
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
