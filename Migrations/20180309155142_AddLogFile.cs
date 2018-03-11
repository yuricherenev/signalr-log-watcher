using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LogWatcher.Migrations
{
    public partial class AddLogFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogItems",
                table: "LogItems");

            migrationBuilder.RenameTable(
                name: "LogItems",
                newName: "LogItem");

            migrationBuilder.AddColumn<Guid>(
                name: "LogFileId",
                table: "LogItem",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogItem",
                table: "LogItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LogFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogFiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogItem_LogFileId",
                table: "LogItem",
                column: "LogFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogItem_LogFiles_LogFileId",
                table: "LogItem",
                column: "LogFileId",
                principalTable: "LogFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogItem_LogFiles_LogFileId",
                table: "LogItem");

            migrationBuilder.DropTable(
                name: "LogFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogItem",
                table: "LogItem");

            migrationBuilder.DropIndex(
                name: "IX_LogItem_LogFileId",
                table: "LogItem");

            migrationBuilder.DropColumn(
                name: "LogFileId",
                table: "LogItem");

            migrationBuilder.RenameTable(
                name: "LogItem",
                newName: "LogItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogItems",
                table: "LogItems",
                column: "Id");
        }
    }
}
