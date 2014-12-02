using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class Curso_asig_alumno
    {
        public string Cod_asig { get; set; }
        public string Cod_curso { get; set; }

        public Curso_asig_alumno()
        {
            
            this.Cod_curso = "";

        }
        public Curso_asig_alumno(string cod_asig,string cod_curso)
        {
            this.Cod_asig = cod_asig;
            this.Cod_curso = cod_curso;
        }

    }
}