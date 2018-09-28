using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class TypeSpeciality
    {
        [Key]
        public TypeSpecialityEnum ETypeUser { get; set; }
        public ICollection<Medic> Medic { get; set; }

        public enum TypeSpecialityEnum
        {
            Geral = 0,
            Pediatria = 1
        }
    }
}
