using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezreal.CCAssistant.Migrations
{
    /// <inheritdoc />
    public partial class AddHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "AppCustomers",
                type: "decimal(18,2)",
                nullable: false,
                comment: "账户余额",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "AppHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id"),
                    HistoryRecords = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "历史记录")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHistories", x => x.Id);
                },
                comment: "历史记录");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppHistories");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "AppCustomers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "账户余额");
        }
    }
}
