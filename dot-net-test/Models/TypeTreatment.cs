using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class TypeTreatment
    {
        [Key]
        public TypeUserEnum ETypeTreatment { get; set; }
        public ICollection<Treatment> Treatment { get; set; }

        public enum TypeUserEnum
        {
            Consulta = 0,
            Exame = 1
        }
    }
}
