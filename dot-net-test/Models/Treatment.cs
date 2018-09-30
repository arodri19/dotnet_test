using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Treatment
    {
        [Key]
        public int ID { get; set; }
        public TypeTreatmentEnum TypeTreatment { get; set; }
        public string Obs { get; set; }

        public ICollection<ScheduleTreatment> ScheduleTreatment { get; set; }

    }
}
