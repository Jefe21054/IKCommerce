using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMISApp.Model
{
    class Usuario
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public string Username { get; set; }
    }
}
