using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{
    public class PatientService
    {
        private HealthcareContext _context;

        public PatientService(HealthcareContext context)
        {
            _context = context;
        }
    }
}
