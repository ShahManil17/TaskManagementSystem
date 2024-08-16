using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetStatusCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getStatusCount]
            @id INT, @status VARCHAR(MAX)
            AS
            BEGIN
	            IF (@id = 1)
	            BEGIN
		            SELECT Count(*) FROM SubTask WHERE Status = @status
	            END
	            ELSE 
	            BEGIN
		            SELECT Count(*) FROM SubTask
		            INNER JOIN UserHasTask AS userTask ON SubTask.Id = userTask.TaskId
		            WHERE userTask.UserId = @id AND SubTask.Status = @status
	            END
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
