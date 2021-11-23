using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class AddAdoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Adoptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adoptions_Users_UserId",
                table: "Adoptions");

            migrationBuilder.DropIndex(
                name: "IX_Adoptions_UserId",
                table: "Adoptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Adoptions");
        }
    }
}
