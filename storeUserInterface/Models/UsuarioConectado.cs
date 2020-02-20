using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace storeUserInterface.Models
{
    public class UsuarioConectado
    {
        public bool IsLogged { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}