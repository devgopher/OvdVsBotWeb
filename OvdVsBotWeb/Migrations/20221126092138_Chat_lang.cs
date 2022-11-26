using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OvdVsBotWeb.Migrations
{
    /// <inheritdoc />
    public partial class Chatlang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lang",
                table: "Chats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lang",
                table: "Chats");
        }
    }
}
