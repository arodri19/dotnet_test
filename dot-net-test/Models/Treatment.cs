using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Treatment
    {
        public int TreatmentID { get; set; }
        public string Name { get; set; }

        public ICollection<ScheduleTreatment> TreatmentMedicine { get; set; }

        public TypeTreatment TypeTreatment { get; set; }
    }
}
