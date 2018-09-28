﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Medic
    {
        public int MedicID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastAccess { get; set; }

        public string Crm { get; set; }
        public new TypeSpeciality TypeSpeciality { get; set; }

        public ICollection<ScheduleTreatment> ScheduleTreatment { get; set; }

    }
}
