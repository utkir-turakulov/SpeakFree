using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeakFree.DAL.Migrations
{
    public partial class Message_Title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Messages");
        }
    }
}
