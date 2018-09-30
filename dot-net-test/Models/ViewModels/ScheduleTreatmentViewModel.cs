using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class ScheduleTreatmentViewModel
    {

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Medic")]
        public int MedicID { get; set; }

        public TreatmentViewModel Treatment { get; set; }
        [ForeignKey("Treatment")]
        public int TreatmentID { get; set; }

        public string Obs { get; set; }
        public DateTime Schedule { get; set; }

    }
}
