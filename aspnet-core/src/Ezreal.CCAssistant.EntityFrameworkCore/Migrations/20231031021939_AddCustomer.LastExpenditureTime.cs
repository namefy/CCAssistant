using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ezreal.CCAssistant.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerLastExpenditureTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastExpenditureTime",
                table: "AppCustomers",
                type: "datetime2",
                nullable: true,
                comment: "最近消费时间");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastExpenditureTime",
                table: "AppCustomers");
        }
    }
}
