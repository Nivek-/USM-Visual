using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using ServiceWebAplicacion;

namespace WebApplicationUSM
{
    /// <summary>
    /// Summary description for ServicioClientes
    /// </summary>
    [WebService(Namespace = "http://usm.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebServiceUSM : System.Web.Services.WebService
    {
        //hace referencia a la clase conexion, ahi esta la cadena de conexion y nuestros metodos
        Conexion con = new Conexion();

        //Métodos del servicio web
        //...
        [WebMethod]
        public int InsertarNoticia(string Cod_profesor, string Cod_asig, string Cod_curso, string descripcion)
        {
            SqlConnection con =
                new SqlConnection(
                   @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "INSERT INTO Noticia (Cod_asig,Cod_curso,Cod_fecha,Cod_profesor,noticia) VALUES (@cod_asig,@cod_curso,@cod_fecha, @cod_profesor, @descripcion)";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@cod_asig", System.Data.SqlDbType.NVarChar).Value = Cod_asig;
            DateTime Cod_fecha = System.DateTime.Now;
            cmd.Parameters.Add("@cod_fecha", System.Data.SqlDbType.DateTime).Value = Cod_fecha;
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = Cod_profesor;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = Cod_curso;
            cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.NVarChar).Value = descripcion;
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public noticia[] ObtenerNoticia(string cod_asig, string cod_profesor, string cod_curso)
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");

            con.Open();

            string sql = "SELECT cod_profesor,cod_asig,cod_fecha,cod_curso,noticia FROM Noticia WHERE cod_asig = @cod_asig  and cod_profesor = @cod_profesor  and cod_curso = @cod_curso";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_asig", System.Data.SqlDbType.NVarChar).Value = cod_asig;

            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = cod_profesor;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = cod_curso;

            SqlDataReader reader = cmd.ExecuteReader();

            List<noticia> lista = new List<noticia>();

            while (reader.Read())
            {
                lista.Add(
                    new noticia(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(3),
                                reader.GetString(4)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public curso[] ObtenerCurso_Profesor(string cod_profesor)
        {
            SqlConnection con =
                new SqlConnection(
                           @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "SELECT cod_curso FROM profesor_asignatura WHERE  cod_profesor = @cod_profesor group by cod_curso";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = cod_profesor;
            
            
            SqlDataReader reader = cmd.ExecuteReader();

            List<curso> lista = new List<curso>();

            while (reader.Read())
            {
                lista.Add(
                    new curso(reader.GetString(0)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public asignatura_curso[] ObtenerAsignatura(string cod_curso,string cod_profesor)
        {
            SqlConnection con =
                new SqlConnection(
                            @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "SELECT nom_asig, a.cod_asig FROM asignatura a, profesor_asignatura pa WHERE a.cod_asig = pa.cod_asig and a.cod_curso = pa.cod_curso and a.cod_curso = @cod_curso and pa.cod_profesor = @cod_profesor";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = cod_profesor;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = cod_curso;
            SqlDataReader reader = cmd.ExecuteReader();

            List<asignatura_curso> lista = new List<asignatura_curso>();

            while (reader.Read())
            {
                lista.Add(
                    new asignatura_curso(reader.GetString(0),
                                         reader.GetString(1)));
            }

            con.Close();

            return lista.ToArray();
        }

        [WebMethod]
        public int EliminarNoticia(string Cod_profesor, string Cod_asig, string Cod_curso, string Cod_fecha)
        {
            SqlConnection con =
                new SqlConnection(
                        @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "DELETE FROM Noticia where cod_asig = @cod_asig and cod_profesor = @cod_profesor and cod_fecha = @cod_fecha and cod_curso = @cod_curso";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@cod_asig", System.Data.SqlDbType.NVarChar).Value = Cod_asig;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = Cod_curso;
            cmd.Parameters.Add("@cod_fecha", System.Data.SqlDbType.DateTime).Value = Cod_fecha;
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = Cod_profesor;
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public int ModificarNoticia(string Cod_profesor, string Cod_asig, string Cod_curso, string Cod_fecha,string descripcion)
        {
            SqlConnection con =
                new SqlConnection(
                      @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "UPDATE Noticia SET noticia = @descripcion where cod_asig = @cod_asig AND cod_profesor = @cod_profesor AND cod_fecha = @cod_fecha AND cod_curso = @cod_curso";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.NVarChar).Value = descripcion;
            cmd.Parameters.Add("@cod_asig", System.Data.SqlDbType.NVarChar).Value = Cod_asig;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = Cod_curso;
            cmd.Parameters.Add("@cod_fecha", System.Data.SqlDbType.DateTime).Value = Cod_fecha;
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = Cod_profesor;
            
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }
        [WebMethod]
        public curso[] ObtenerCurso_ProfesorTodo()
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");

            con.Open();

            string sql = "SELECT cod_curso FROM profesor_asignatura";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();

            List<curso> lista = new List<curso>();

            while (reader.Read())
            {
                lista.Add(
                    new curso(reader.GetString(0)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public String LoginUsuario(string user, String password)
        {
            string msje = "";
            msje = con.InicioSesion(user, password);

            return msje;
        }

    }
}