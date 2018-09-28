using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class ScheduleTreatment
    {
        public int ScheduleTreatmentID { get; set; }
        public Patient Patient { get; set; }
        public Medic Medic { get; set; }
        public TypeTreatment TypeTreatment { get; set; }
        public string Obs { get; set; }
        public string Schedule { get; set; }
        public ICollection<Medicines> Medicines { get; set; }
    }
}
