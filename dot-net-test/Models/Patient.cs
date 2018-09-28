using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastAccess { get; set; }

        public ICollection<ScheduleTreatment> ScheduleTreatment { get; set; }
    }
}
