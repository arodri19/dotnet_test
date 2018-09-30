using dotnet_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test
{
    public class HealthcareContext : DbContext
    {
        public HealthcareContext(DbContextOptions<HealthcareContext> options)
            : base(options)
        { }

        public HealthcareContext() { }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<Medic> Medic { get; set; }

        public DbSet<Medicine> Medicine { get; set; }

        public DbSet<ScheduleTreatment> ScheduleTreatment { get; set; }

        public DbSet<MedicineScheduleTreatment> MedicineScheduleTreatment { get; set; }

        public DbSet<Treatment> Treatment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleTreatment>()
        .HasKey(bc => new { bc.MedicID, bc.PatientID, bc.TreatmentID });

            modelBuilder.Entity<ScheduleTreatment>()
                .HasOne(bc => bc.Medic)
                .WithMany(b => b.ScheduleTreatment)
                .HasForeignKey(bc => bc.MedicID);

            modelBuilder.Entity<ScheduleTreatment>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.ScheduleTreatment)
                .HasForeignKey(bc => bc.PatientID)
                .IsRequired(true);

            modelBuilder.Entity<ScheduleTreatment>()
                .HasOne(bc => bc.Treatment)
                .WithMany(c => c.ScheduleTreatment)
                .HasForeignKey(bc => bc.TreatmentID);


            modelBuilder.Entity<MedicineScheduleTreatment>()
        .HasKey(bc => new { bc.MedicineID, bc.ScheduleTreatmentID });

            modelBuilder.Entity<MedicineScheduleTreatment>()
                .HasOne(bc => bc.Medicine)
                .WithMany(b => b.MedicineScheduleTreatment)
                .HasPrincipalKey(b=>b.ID)
                .HasForeignKey(bc => bc.MedicineID);

            modelBuilder.Entity<MedicineScheduleTreatment>()
                .HasOne(bc => bc.ScheduleTreatment)
                .WithMany(c => c.MedicineScheduleTreatment)
                .HasPrincipalKey(c => c.TreatmentID)
                .HasForeignKey(bc => bc.ScheduleTreatmentID);            
        }

    }
}
