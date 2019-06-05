using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeakFree.DAL.Migrations
{
    public partial class Message_Priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Messages");
        }
    }
}
