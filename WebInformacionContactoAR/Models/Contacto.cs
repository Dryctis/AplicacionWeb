using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInformacionContactoAR.Models
{
    public class Contacto
    {
        public int Idcontacto { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public int? Salario { get; set; }

        public string FechaNacimiento { get; set; }



    }
}