using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class MedicineScheduleTreatmentResultViewModel
    {
        public MedicineViewModel Medicine { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineID { get; set; }

        public ScheduleTreatmentResultViewModel ScheduleTreatment { get; set; }
        [ForeignKey("ScheduleTreatment")]
        public int ScheduleTreatmentID { get; set; }

        public bool Disabled { get; set; } = false;
    }
}
