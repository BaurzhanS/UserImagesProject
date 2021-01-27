using Microsoft.EntityFrameworkCore.Migrations;

namespace UserImagesRepo.Migrations
{
    public partial class UpdatedModelNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ToModerator",
                table: "Notification",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToModerator",
                table: "Notification");
        }
    }
}
