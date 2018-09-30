using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class PatientScheduleViewModel
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public bool Disabled { get; set; } = false;
    }
}
