
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class horProf
    {

        public int Dia { get; set; }
        public int Bloque { get; set; }
        public char Estado { get; set; }
      

        public horProf()
        {
            this.Dia = 0;
            this.Bloque = 0;
            this.Estado = ' ';
           

        }
        public horProf( int dia,int bloque,char estado)
        {
            this.Dia = dia;
            this.Bloque = bloque;
            this.Estado = estado;
            


        }
    }
}