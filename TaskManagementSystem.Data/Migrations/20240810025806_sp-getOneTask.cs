using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetOneTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getOneTask] @id INT
            AS
            BEGIN
	            SELECT (
		            SELECT *, (
			            SELECT * FROM SubTask WHERE SubTask.TaskId = Tasks.Id
			            FOR JSON PATH
		            ) AS SubTasks
	            FROM Tasks
	            WHERE Id = @id
	            FOR JSON PATH)
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
