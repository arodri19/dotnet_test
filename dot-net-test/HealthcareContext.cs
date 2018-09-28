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

        public DbSet<Medicines> Medicines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleTreatment>()
        .HasKey(bc => new { bc.MedicID, bc.PatientID });

            modelBuilder.Entity<ScheduleTreatment>()
                .HasOne(bc => bc.Medic)
                .WithMany(b => b.ScheduleTreatment)
                .HasPrincipalKey(c => c.ID)
                .HasForeignKey(bc => bc.MedicID);

            modelBuilder.Entity<ScheduleTreatment>()
                .HasOne(bc => bc.Patient)
                .WithMany(c => c.ScheduleTreatment)
                .HasPrincipalKey(c => c.ID)
                .HasForeignKey(bc => bc.PatientID);


            modelBuilder.Entity<Medicines>()
        .HasKey(bc => new { bc.MedicineID, bc.ScheduleTreatmentID });

            modelBuilder.Entity<Medicines>()
                .HasOne(bc => bc.Medicine)
                .WithMany(b => b.Medicines)
                .HasPrincipalKey(c => c.ID)
                .HasForeignKey(bc => bc.MedicineID);

            modelBuilder.Entity<Medicines>()
                .HasOne(bc => bc.ScheduleTreatment)
                .WithMany(c => c.Medicines)
                .HasPrincipalKey(c => c.ID)
                .HasForeignKey(bc => bc.ScheduleTreatmentID);            
        }

    }
}
