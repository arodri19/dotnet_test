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
        [Key]
        public int ID { get; set; }

        public Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public Medic Medic { get; set; }
        [ForeignKey("Medic")]
        public int MedicID { get; set; }

        public TypeTreatmentEnum TypeTreatment { get; set; }

        public string Obs { get; set; }
        public string Schedule { get; set; }

        public ICollection<Medicines> Medicines { get; set; }
    }
}
