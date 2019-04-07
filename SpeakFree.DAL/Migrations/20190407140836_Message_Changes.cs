using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeakFree.DAL.Migrations
{
    public partial class Message_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Messages",
                newName: "DeletedAt");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Messages",
                newName: "DateTime");

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
    }
}
