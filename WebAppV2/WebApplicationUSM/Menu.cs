using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class Menu
    {
        private string entrada{ get; set; }
        private string fondo{ get; set; }
        private string postre{ get; set; }


        public Menu()
        {
            this.entrada = "";
            this.fondo = "";
            this.postre = "";
                    
        }
        public Menu(string Entrada, string Fondo, string Postre)
        {
            this.entrada = Entrada;
            this.fondo = Fondo;
            this.postre = Postre;
            
           
        }
    }


}