using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class profesor
    {
         public string Cod { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Pass { get; set; }

        public profesor()
        {
            this.Cod = "";
            this.Nombre = "";
            this.Pass = "";
        }



        public profesor(string cod, string nombre, string apellido, string pass)
        {
            this.Cod = cod;
            this.Nombre = nombre;
            this.Apellido = Apellido;
            this.Pass = pass;
        }
    }
}