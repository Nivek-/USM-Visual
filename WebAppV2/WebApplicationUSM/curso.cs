using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class curso
    {
       
        public string Cod_curso { get; set; }
      

        public curso()
        {
            
            this.Cod_curso = "";
           

        }
        public curso( string cod_curso)
        {
          
            this.Cod_curso = cod_curso;
            


        }
    }
}