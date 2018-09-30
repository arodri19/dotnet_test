using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Medicine
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Obs { get; set; }

        public bool Disabled { get; set; } = false;

        public ICollection<MedicineScheduleTreatment> MedicineScheduleTreatment { get; set; }
    }
}
