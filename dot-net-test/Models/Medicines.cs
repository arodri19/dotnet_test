using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Medicines
    {
        public int MedicinesID { get; set; }
        public Medicine Medicine { get; set; }
        public ScheduleTreatment ScheduleTreatment { get; set; }
    }
}
