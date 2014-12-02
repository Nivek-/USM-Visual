using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class profesor_asignatura
    {
        public string Cod_asig { get; set; }
        public string Cod_curso { get; set; }
        public string Cod_profesor { get; set; }
         
        public profesor_asignatura()
        {
            this.Cod_asig = "";
            this.Cod_curso = "";
            this.Cod_profesor = "";
            
        }
    public profesor_asignatura(string cod_asig, string cod_curso,string cod_profesor)
        {
            this.Cod_asig = cod_asig;
            this.Cod_curso = cod_curso;
            this.Cod_profesor = cod_profesor;
         
            
        }         
    }
}