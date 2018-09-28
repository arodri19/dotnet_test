﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class MedicViewModel
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Cpf { get; set; }
        [Required(ErrorMessage ="O Preenchimento é necessário")]
        public string Password { get; set; }
        public string Crm { get; set; }
        public TypeSpecialityEnum TypeSpeciality { get; set; }
    }
}
