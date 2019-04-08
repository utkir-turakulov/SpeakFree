using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeakFree.DAL.Migrations
{
    public partial class MessageModelChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId1",
                table: "Messages",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId1",
                table: "Messages",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
