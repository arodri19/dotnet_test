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
                name: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    LastAccess = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "TypeSpeciality",
                columns: table => new
                {
                    ETypeUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSpeciality", x => x.ETypeUser);
                });

            migrationBuilder.CreateTable(
                name: "TypeTreatment",
                columns: table => new
                {
                    ETypeTreatment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTreatment", x => x.ETypeTreatment);
                });

            migrationBuilder.CreateTable(
                name: "TypeUser",
                columns: table => new
                {
                    ETypeUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeUser", x => x.ETypeUser);
                });

            migrationBuilder.CreateTable(
                name: "Medic",
                columns: table => new
                {
                    MedicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    LastAccess = table.Column<DateTime>(nullable: false),
                    Crm = table.Column<string>(nullable: true),
                    TypeSpecialityETypeUser = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medic", x => x.MedicID);
                    table.ForeignKey(
                        name: "FK_Medic_TypeSpeciality_TypeSpecialityETypeUser",
                        column: x => x.TypeSpecialityETypeUser,
                        principalTable: "TypeSpeciality",
                        principalColumn: "ETypeUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    TreatmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TypeTreatmentETypeTreatment = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.TreatmentID);
                    table.ForeignKey(
                        name: "FK_Treatment_TypeTreatment_TypeTreatmentETypeTreatment",
                        column: x => x.TypeTreatmentETypeTreatment,
                        principalTable: "TypeTreatment",
                        principalColumn: "ETypeTreatment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                columns: table => new
                {
                    SystemUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    LastAccess = table.Column<DateTime>(nullable: false),
                    TypeUserETypeUser = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUser", x => x.SystemUserID);
                    table.ForeignKey(
                        name: "FK_SystemUser_TypeUser_TypeUserETypeUser",
                        column: x => x.TypeUserETypeUser,
                        principalTable: "TypeUser",
                        principalColumn: "ETypeUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTreatment",
                columns: table => new
                {
                    ScheduleTreatmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientID = table.Column<int>(nullable: true),
                    MedicID = table.Column<int>(nullable: true),
                    TypeTreatmentETypeTreatment = table.Column<int>(nullable: true),
                    Obs = table.Column<string>(nullable: true),
                    Schedule = table.Column<string>(nullable: true),
                    TreatmentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTreatment", x => x.ScheduleTreatmentID);
                    table.ForeignKey(
                        name: "FK_ScheduleTreatment_Medic_MedicID",
                        column: x => x.MedicID,
                        principalTable: "Medic",
                        principalColumn: "MedicID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleTreatment_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleTreatment_Treatment_TreatmentID",
                        column: x => x.TreatmentID,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleTreatment_TypeTreatment_TypeTreatmentETypeTreatment",
                        column: x => x.TypeTreatmentETypeTreatment,
                        principalTable: "TypeTreatment",
                        principalColumn: "ETypeTreatment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicinesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicineID = table.Column<int>(nullable: true),
                    ScheduleTreatmentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicinesID);
                    table.ForeignKey(
                        name: "FK_Medicines_Medicine_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicine",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicines_ScheduleTreatment_ScheduleTreatmentID",
                        column: x => x.ScheduleTreatmentID,
                        principalTable: "ScheduleTreatment",
                        principalColumn: "ScheduleTreatmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medic_TypeSpecialityETypeUser",
                table: "Medic",
                column: "TypeSpecialityETypeUser");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineID",
                table: "Medicines",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_ScheduleTreatmentID",
                table: "Medicines",
                column: "ScheduleTreatmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreatment_MedicID",
                table: "ScheduleTreatment",
                column: "MedicID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreatment_PatientID",
                table: "ScheduleTreatment",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreatment_TreatmentID",
                table: "ScheduleTreatment",
                column: "TreatmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreatment_TypeTreatmentETypeTreatment",
                table: "ScheduleTreatment",
                column: "TypeTreatmentETypeTreatment");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUser_TypeUserETypeUser",
                table: "SystemUser",
                column: "TypeUserETypeUser");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_TypeTreatmentETypeTreatment",
                table: "Treatment",
                column: "TypeTreatmentETypeTreatment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "SystemUser");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "ScheduleTreatment");

            migrationBuilder.DropTable(
                name: "TypeUser");

            migrationBuilder.DropTable(
                name: "Medic");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Treatment");

            migrationBuilder.DropTable(
                name: "TypeSpeciality");

            migrationBuilder.DropTable(
                name: "TypeTreatment");
        }
    }
}
