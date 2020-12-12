using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserImagesRepo.Migrations
{
    public partial class UpdateImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Image",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Image",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Image",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Image");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Name",
                table: "Image",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
