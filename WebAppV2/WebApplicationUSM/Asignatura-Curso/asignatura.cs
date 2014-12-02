using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class asignatura
    {
        public string Cod_asig { get; set; }
        public string Cod_curso { get; set; }
        public string Nom_asig { get; set; }        
     
        public asignatura()
        {
            this.Cod_asig = "";
            this.Cod_curso = "";
            this.Nom_asig = "";    
            
        }
        public asignatura(string cod_asig, string cod_curso, string nom_asig)
        {
            this.Cod_asig = cod_asig;
            this.Cod_curso = cod_curso;
            this.Nom_asig = nom_asig;
         
            
        }         
    }
}