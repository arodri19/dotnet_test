using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{

    public class TypeUser
    {
        [Key]
        public TypeUserEnum ETypeUser { get; set; }
        public ICollection<SystemUser> User { get; set; }

        public enum TypeUserEnum
        {
            Administrador = 0,
            Outros = 1
        }
    }
}
