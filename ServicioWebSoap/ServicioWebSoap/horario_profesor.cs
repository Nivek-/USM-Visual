using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebSoap
{
    public class horario_profesor
    {
        public int codigo { get; set; }
        public int dia { get; set; }
        public int bloque { get; set; }

        public horario_profesor()
        {
            this.codigo = 0;
            this.dia = 0;
            this.bloque = 0;
        }

        public horario_profesor(int cod, int dia, int bloque)
        {
            this.codigo = cod;
            this.dia =   dia;
            this.bloque = bloque;
        }
    }
}