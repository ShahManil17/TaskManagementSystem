using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetUserHasSubTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getUserHasSubTasks] @id INT
            AS
            BEGIN
	            SELECT SubTask.* FROM SubTask
	            INNER JOIN UserHasTask AS userTask ON SubTask.Id = userTask.TaskId
	            WHERE userTask.UserId = @id
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
