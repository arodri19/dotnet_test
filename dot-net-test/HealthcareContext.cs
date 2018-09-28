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

        public DbSet<TypeUser> TypeUser { get; set; }
        public DbSet<TypeTreatment> TypeTreatment { get; set; }
        public DbSet<TypeSpeciality> TypeSpeciality { get; set; }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<Medic> Medic { get; set; }

        public DbSet<Medicine> Medicine { get; set; }

        public DbSet<ScheduleTreatment> ScheduleTreatment { get; set; }

        public DbSet<Medicines> Medicines { get; set; }

    }
}
