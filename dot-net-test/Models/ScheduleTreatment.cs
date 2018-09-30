using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class ScheduleTreatment
    {
        public Patient Patient { get; set; }
        public int PatientID { get; set; }

        public Medic Medic { get; set; }
        public int MedicID { get; set; }

        public Treatment Treatment { get; set; }
        public int TreatmentID { get; set; }
       
        public DateTime Schedule { get; set; }

        public bool Cancel { get; set; } = false;

        public ICollection<MedicineScheduleTreatment> MedicineScheduleTreatment { get; set; }
    }
}
