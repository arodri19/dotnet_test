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

        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
