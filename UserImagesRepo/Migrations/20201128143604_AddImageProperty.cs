using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserImagesRepo.Migrations
{
    public partial class AddImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");
        }
    }
}
