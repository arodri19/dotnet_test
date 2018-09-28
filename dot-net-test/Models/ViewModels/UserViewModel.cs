using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
