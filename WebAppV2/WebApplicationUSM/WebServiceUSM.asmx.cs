using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using ServiceWebAplicacion;
using WebApplicationUSM;

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
        public String LoginUsuario(string user, String password)
        {
            string msje = "";
            msje = con.InicioSesion(user, password);

            return msje;
        }
        //Profesor-------------------------------------------------------------------------------------------------------------------------

        [WebMethod]
        public horProf[] ObtenerHorarioProf(string cod_profesor)
        {
            SqlConnection con =
                new SqlConnection(
                           @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "SELECT cod_dia,cod_bloque,estado FROM horario_profesor WHERE  cod_profesor = @cod_profesor order by cod_bloque,cod_dia";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_profesor", System.Data.SqlDbType.NVarChar).Value = cod_profesor;


            SqlDataReader reader = cmd.ExecuteReader();

            List<horProf> lista = new List<horProf>();

            while (reader.Read())
            {
                lista.Add(
                    new horProf(reader.GetInt16(0),
                                reader.GetInt16(1),
                                reader.GetChar(2)));
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
        public asignatura_curso[] ObtenerAsignatura(string cod_curso, string cod_profesor)
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

        //Noticia--------------------------------------------------------------------------------------------------------
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
                                reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetString(4)));
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
        public int ModificarNoticia(string Cod_profesor, string Cod_asig, string Cod_curso, string Cod_fecha, string descripcion)
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


        //ALUMNOS---------------------------------------------------------------------------------------------------------------------------------------------------

        [WebMethod]
        public curso[] ObtenerCursoAlumno(string cod_alumno)
        {
            SqlConnection con =
                new SqlConnection(
                           @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "SELECT cod_curso FROM alumno_asignatura WHERE  cod_alumno = @cod_alumno group by cod_curso";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_alumno", System.Data.SqlDbType.NVarChar).Value = cod_alumno;


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
        public asignatura_curso[] ObtenerAsignaturaAlumno(string cod_curso, string cod_alumno)
        {
            SqlConnection con =
                new SqlConnection(
                            @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "SELECT nom_asig, a.cod_asig FROM asignatura a, alumno_asignatura aa WHERE a.cod_asig = aa.cod_asig and a.cod_curso = aa.cod_curso and a.cod_curso = @cod_curso and aa.cod_alumno = @cod_alumno";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_alumno", System.Data.SqlDbType.NVarChar).Value = cod_alumno;
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
        public noticia[] ObtenerNoticiaAlumno(string cod_asig, string cod_curso)
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");

            con.Open();

            string sql = "select cod_profesor,cod_asig,cod_curso,cod_fecha,noticia from Noticia where cod_asig=@cod_asig and cod_curso=@cod_curso order by cod_fecha DESC";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_asig", System.Data.SqlDbType.NVarChar).Value = cod_asig;
            cmd.Parameters.Add("@cod_curso", System.Data.SqlDbType.NVarChar).Value = cod_curso;

            SqlDataReader reader = cmd.ExecuteReader();

            List<noticia> lista = new List<noticia>();

            while (reader.Read())
            {
                lista.Add(
                    new noticia(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetString(4)));
            }

            con.Close();

            return lista.ToArray();
        }

        //Casino--------------------------------------------------------------------------------------------------
        [WebMethod]
        public int InsertarMenu(string Cod_tipo, string Cod_dia, int dia, string fondo, string entrada, string postre)
        {
            SqlConnection con =
                new SqlConnection(
                   @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "INSERT INTO MenuCasino (Cod_tipo,Cod_dia,dia,fondo,entrada,postre) VALUES (@cod_tipo,@cod_dia,@dia,@fondo, @entrada, @postre)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_tipo", System.Data.SqlDbType.NVarChar).Value = Cod_tipo;
            cmd.Parameters.Add("@cod_dia", System.Data.SqlDbType.Date).Value = Cod_dia;
            cmd.Parameters.Add("@dia", System.Data.SqlDbType.Int).Value = dia;
            cmd.Parameters.Add("@fondo", System.Data.SqlDbType.NVarChar).Value = fondo;
            cmd.Parameters.Add("@entrada", System.Data.SqlDbType.NVarChar).Value = entrada;
            cmd.Parameters.Add("@postre", System.Data.SqlDbType.NVarChar).Value = postre;
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public Menu[] ObtenerMenu(char cod_tipo, string cod_dia)
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");

            con.Open();

            string sql = "SELECT entrada,fondo,postre FROM MenuCasino WHERE cod_tipo = @cod_tipo  and cod_dia = @cod_dia";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_tipo", System.Data.SqlDbType.Char).Value = cod_tipo;
            cmd.Parameters.Add("@cod_dia", System.Data.SqlDbType.Date).Value = cod_dia;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Menu> lista = new List<Menu>();

            while (reader.Read())
            {
                lista.Add(
                    new Menu(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public Menu[] ObtenerMenuDiario(string cod_dia)
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");

            con.Open();

            string sql = "SELECT entrada,fondo,postre FROM MenuCasino where cod_dia = @cod_dia order by cod_tipo";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cod_dia", System.Data.SqlDbType.Date).Value = cod_dia;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Menu> lista = new List<Menu>();

            while (reader.Read())
            {
                lista.Add(
                    new Menu(reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public int EliminarMenu(string Cod_tipo, DateTime Cod_dia)
        {
            SqlConnection con =
                new SqlConnection(
                        @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "DELETE FROM MenuCasino where cod_tipo = @cod_tipo and cod_dia = @cod_dia";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@cod_tipo", System.Data.SqlDbType.NVarChar).Value = Cod_tipo;
            cmd.Parameters.Add("@cod_dia", System.Data.SqlDbType.DateTime).Value = Cod_dia;
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public int ModificarMenu(string cod_tipo, string cod_dia, string entrada, string fondo, string postre)
        {
            SqlConnection con =
                new SqlConnection(
                      @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=BDUSM;User Id=adm;password=usm");
            con.Open();

            string sql = "UPDATE MenuCasino SET  entrada = @entrada, fondo = @fondo, postre = @postre where cod_tipo = @cod_tipo and cod_dia = @cod_dia";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@cod_tipo", System.Data.SqlDbType.Char).Value = cod_tipo;
            cmd.Parameters.Add("@cod_dia", System.Data.SqlDbType.Date).Value = cod_dia;
            cmd.Parameters.Add("@fondo", System.Data.SqlDbType.NVarChar).Value = fondo;
            cmd.Parameters.Add("@entrada", System.Data.SqlDbType.NVarChar).Value = entrada;
            cmd.Parameters.Add("@postre", System.Data.SqlDbType.NVarChar).Value = postre;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }
    }
}