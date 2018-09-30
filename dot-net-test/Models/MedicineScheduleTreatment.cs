﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class MedicineScheduleTreatment
    {
        public Medicine Medicine { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineID { get; set; }

        public ScheduleTreatment ScheduleTreatment { get; set; }
        [ForeignKey("ScheduleTreatment")]
        public int ScheduleTreatmentID { get; set; }

        public bool Disabled { get; set; } = false;
    }
}
