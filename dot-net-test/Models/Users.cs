using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models
{
    public class Users
    {
        public int UsersID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public DateTime LastAccess { get; set; }
        public string crm { get; set; }

        public TypeUsers TypeUsers { get; set; }
    }
}
