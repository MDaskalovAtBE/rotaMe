using Microsoft.EntityFrameworkCore.Migrations;

namespace RotaMe.Data.Migrations
{
    public partial class ChangeProjectModelBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId",
                table: "Projects",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId1",
                table: "Projects",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId1",
                table: "Projects",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
