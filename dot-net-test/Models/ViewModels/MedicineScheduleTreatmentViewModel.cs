using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class MedicineScheduleTreatmentViewModel
    {
        [Key]
        public int ID { get; set; }

        public MedicineViewModel Medicine { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineID { get; set; }

        [ForeignKey("ScheduleTreatment")]
        public int ScheduleTreatmentID { get; set; }
    }
}
