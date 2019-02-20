using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dto.Usuario
{
    public class UsuarioAuthenticationDto
    {
        public bool authenticated { get; set; }
        public string message { get; set; }
        public string created { get; set; }
        public string expiration { get; set; }
        public string accessToken { get; set; }
        public string usuario { get; set; }
    }
}
