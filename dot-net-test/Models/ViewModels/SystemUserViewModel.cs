using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Models.ViewModels
{
    public class SystemUserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
