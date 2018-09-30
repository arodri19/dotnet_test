using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class TreatmentViewModel
    {
        [Key]
        public int ID { get; set; }
        public TypeTreatmentEnum TypeTreatment { get; set; }
        public string Obs { get; set; }
    }
}
