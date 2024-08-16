using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetDesplayDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getDesplayDetails]
			@taskId INT
			AS
			BEGIN
				SELECT (
					SELECT *,
					(
						SELECT UserName FROM Users WHERE Id = (SELECT CreatedById FROM Tasks WHERE Id = @taskId)
					) AS CreatedByName,
					(
						SELECT *,
						(
							SELECT Users.* FROM Users
							INNER JOIN UserHasTask ON UserHasTask.UserId = Users.Id
							WHERE UserHasTask.TaskId = SubTask.Id
							FOR JSON PATH
						) AS Users
						FROM SubTask WHERE SubTask.TaskId = Tasks.Id
						FOR JSON PATH
					) AS SubTasks
					FROM Tasks
					WHERE Tasks.Id = @taskId
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
