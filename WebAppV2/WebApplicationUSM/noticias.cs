using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationUSM
{
    public class noticia
    {
        public string Cod_profesor { get; set; }
        public string Cod_asig { get; set; }
        public string Cod_curso { get; set; }
        public DateTime Cod_fecha { get; set; }
        public string descripcion {get; set;}
     
        
    
        public noticia()
        {
            this.Cod_profesor = "";
            this.Cod_asig = "";
            this.Cod_curso = "";
            DateTime date =  DateTime.Now;
            this.Cod_fecha = date;
            this.descripcion="";
           
        }



        public noticia(string cod_profesor, string cod_asig, string cod_curso, DateTime cod_fecha, string mensaje)
        {
            this.Cod_profesor = cod_profesor;
            this.Cod_asig = cod_asig;
            this.Cod_curso = cod_curso;
            this.Cod_fecha = cod_fecha;
            this.descripcion = mensaje;
           
        }
    }
}