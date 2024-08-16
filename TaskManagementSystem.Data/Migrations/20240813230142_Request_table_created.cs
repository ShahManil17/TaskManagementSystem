using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Request_table_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubTaskId = table.Column<int>(type: "int", nullable: true),
                    RequestedId = table.Column<int>(type: "int", nullable: true),
                    AcceptedId = table.Column<int>(type: "int", nullable: true),
                    RequestedStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpen = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_SubTask_SubTaskId",
                        column: x => x.SubTaskId,
                        principalTable: "SubTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Users_AcceptedId",
                        column: x => x.AcceptedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Users_RequestedId",
                        column: x => x.RequestedId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AcceptedId",
                table: "Requests",
                column: "AcceptedId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestedId",
                table: "Requests",
                column: "RequestedId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SubTaskId",
                table: "Requests",
                column: "SubTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
