using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_test.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    MedicineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Obs = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UsersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LastAccess = table.Column<DateTime>(nullable: false),
                    crm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UsersID);
                });

            migrationBuilder.CreateTable(
                name: "TypeUsers",
                columns: table => new
                {
                    ETypeUsers = table.Column<int>(nullable: false),
                    UsersID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeUsers", x => x.ETypeUsers);
                    table.ForeignKey(
                        name: "FK_TypeUsers_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeUsers_UsersID",
                table: "TypeUsers",
                column: "UsersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "TypeUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
