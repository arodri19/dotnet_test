using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_test.Migrations
{
    public partial class InitialCreate_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeUsers_Users_UsersID",
                table: "TypeUsers");

            migrationBuilder.DropIndex(
                name: "IX_TypeUsers_UsersID",
                table: "TypeUsers");

            migrationBuilder.DropColumn(
                name: "UsersID",
                table: "TypeUsers");

            migrationBuilder.AddColumn<int>(
                name: "TypeUsersETypeUsers",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeUsersETypeUsers",
                table: "Users",
                column: "TypeUsersETypeUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TypeUsers_TypeUsersETypeUsers",
                table: "Users",
                column: "TypeUsersETypeUsers",
                principalTable: "TypeUsers",
                principalColumn: "ETypeUsers",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TypeUsers_TypeUsersETypeUsers",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TypeUsersETypeUsers",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TypeUsersETypeUsers",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UsersID",
                table: "TypeUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeUsers_UsersID",
                table: "TypeUsers",
                column: "UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeUsers_Users_UsersID",
                table: "TypeUsers",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "UsersID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
