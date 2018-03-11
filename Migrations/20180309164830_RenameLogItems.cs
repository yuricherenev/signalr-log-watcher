using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LogWatcher.Migrations
{
    public partial class RenameLogItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogItem_LogFiles_LogFileId",
                table: "LogItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogItem",
                table: "LogItem");

            migrationBuilder.RenameTable(
                name: "LogItem",
                newName: "LogItems");

            migrationBuilder.RenameIndex(
                name: "IX_LogItem_LogFileId",
                table: "LogItems",
                newName: "IX_LogItems_LogFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogItems",
                table: "LogItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogItems_LogFiles_LogFileId",
                table: "LogItems",
                column: "LogFileId",
                principalTable: "LogFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogItems_LogFiles_LogFileId",
                table: "LogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogItems",
                table: "LogItems");

            migrationBuilder.RenameTable(
                name: "LogItems",
                newName: "LogItem");

            migrationBuilder.RenameIndex(
                name: "IX_LogItems_LogFileId",
                table: "LogItem",
                newName: "IX_LogItem_LogFileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogItem",
                table: "LogItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogItem_LogFiles_LogFileId",
                table: "LogItem",
                column: "LogFileId",
                principalTable: "LogFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
