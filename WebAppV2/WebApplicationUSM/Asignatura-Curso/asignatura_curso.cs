using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class asignatura_curso
    {
        
        public string Nom_asig { get; set; }
        public string Cod_asig { get; set; }
    public asignatura_curso()
    {

     this.Nom_asig = "";
     this.Cod_asig = "";

    }
            public asignatura_curso (string nom_asig, string cod_asig){
            this.Nom_asig = nom_asig; 
            this.Cod_asig = cod_asig;
        
        
        }

        }
    }
