using Microsoft.EntityFrameworkCore.Migrations;

namespace Webservice.Migrations
{
    public partial class LogType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InnerException",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LogTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SUCCESS" },
                    { 2, "ERROR" },
                    { 3, "INSERT" },
                    { 4, "UPDATE" },
                    { 5, "DELETE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LogTypeId",
                table: "Logs",
                column: "LogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_LogTypes_LogTypeId",
                table: "Logs",
                column: "LogTypeId",
                principalTable: "LogTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_LogTypes_LogTypeId",
                table: "Logs");

            migrationBuilder.DropTable(
                name: "LogTypes");

            migrationBuilder.DropIndex(
                name: "IX_Logs_LogTypeId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "InnerException",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                table: "Logs");
        }
    }
}
