using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{

    public class TypeUsers
    {
        [Key]
        public TypeUsersEnum ETypeUsers { get; set; }
        public ICollection<Users> Users { get; set; }

        public enum TypeUsersEnum
        {
            Administrador = 0,
            Medico = 1,
            Paciente = 2
        }
    }
}
