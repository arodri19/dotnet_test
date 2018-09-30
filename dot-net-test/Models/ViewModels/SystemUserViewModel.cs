using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class SystemUserViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }

        public bool Disabled { get; set; } = false;

        public TypeUserEnum TypeUserEnum { get; set; }
    }
}
