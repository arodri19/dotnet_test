using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class ScheduleTreatmentResultViewModel
    {
        [Key]
        public int ID { get; set; }

        public PatientScheduleViewModel Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public MedicScheduleViewModel Medic { get; set; }
        [ForeignKey("Medic")]
        public int MedicID { get; set; }

        public TreatmentViewModel Treatment { get; set; }
        [ForeignKey("Treatment")]
        public int TreatmentID { get; set; }

        public DateTime Schedule { get; set; }

        public ICollection<MedicineScheduleTreatmentViewModel> Medicines { get; set; }

    }
}
