using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class SystemUser
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Cpf { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastAccess { get; set; }

        public bool Disabled { get; set; } = false;

        public TypeUserEnum TypeUser { get; set; }
    }
}
