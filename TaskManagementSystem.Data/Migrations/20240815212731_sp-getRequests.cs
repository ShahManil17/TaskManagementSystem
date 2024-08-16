using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class spgetRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[getRequests]
            @testerId INT
            AS
            BEGIN
	            SELECT Requests.*, 
	            (
		            SELECT Name FROM SubTask WHERE Id = UserHasTask.TaskId
	            ) AS TaskName
	            FROM Requests
	            INNER JOIN UserHasTask ON UserHasTask.TaskId = Requests.SubTaskId
	            WHERE UserHasTask.UserId = @testerId AND IsOpen = 1
            END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
