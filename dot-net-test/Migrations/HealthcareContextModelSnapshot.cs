﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet_test;

namespace dotnet_test.Migrations
{
    [DbContext(typeof(HealthcareContext))]
    partial class HealthcareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("dotnet_test.Models.Medic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf");

                    b.Property<string>("Crm");

                    b.Property<DateTime>("LastAccess");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("TypeSpeciality");

                    b.HasKey("ID");

                    b.ToTable("Medic");
                });

            modelBuilder.Entity("dotnet_test.Models.Medicine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Obs");

                    b.HasKey("ID");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("dotnet_test.Models.MedicineScheduleTreatment", b =>
                {
                    b.Property<int>("MedicineID");

                    b.Property<int>("ScheduleTreatmentID");

                    b.Property<int>("ID");

                    b.HasKey("MedicineID", "ScheduleTreatmentID");

                    b.HasAlternateKey("ID");

                    b.HasIndex("ScheduleTreatmentID");

                    b.ToTable("MedicineScheduleTreatment");
                });

            modelBuilder.Entity("dotnet_test.Models.Patient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf");

                    b.Property<DateTime>("LastAccess");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("ID");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("dotnet_test.Models.ScheduleTreatment", b =>
                {
                    b.Property<int>("MedicID");

                    b.Property<int>("PatientID");

                    b.Property<int>("TreatmentID");

                    b.Property<DateTime>("Schedule");

                    b.HasKey("MedicID", "PatientID", "TreatmentID");

                    b.HasIndex("PatientID");

                    b.ToTable("ScheduleTreatment");
                });

            modelBuilder.Entity("dotnet_test.Models.SystemUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf");

                    b.Property<DateTime>("LastAccess");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("TypeUser");

                    b.HasKey("ID");

                    b.ToTable("SystemUser");
                });

            modelBuilder.Entity("dotnet_test.Models.Treatment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Obs");

                    b.Property<int>("TypeTreatment");

                    b.HasKey("ID");

                    b.ToTable("Treatment");
                });

            modelBuilder.Entity("dotnet_test.Models.MedicineScheduleTreatment", b =>
                {
                    b.HasOne("dotnet_test.Models.Medicine", "Medicine")
                        .WithMany("MedicineScheduleTreatment")
                        .HasForeignKey("MedicineID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet_test.Models.ScheduleTreatment", "ScheduleTreatment")
                        .WithMany("MedicineScheduleTreatment")
                        .HasForeignKey("ScheduleTreatmentID")
                        .HasPrincipalKey("TreatmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dotnet_test.Models.ScheduleTreatment", b =>
                {
                    b.HasOne("dotnet_test.Models.Medic", "Medic")
                        .WithMany("ScheduleTreatment")
                        .HasForeignKey("MedicID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet_test.Models.Patient", "Patient")
                        .WithMany("ScheduleTreatment")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dotnet_test.Models.Treatment", "Treatment")
                        .WithMany("ScheduleTreatment")
                        .HasForeignKey("TreatmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
