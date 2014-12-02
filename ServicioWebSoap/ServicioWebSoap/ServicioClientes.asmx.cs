using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
namespace ServicioWebSoap
{
    /// <summary>
    /// Summary description for ServicioClientes
    /// </summary>
    [WebService(Namespace = "http://sgoliver.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ServicioClientes : System.Web.Services.WebService
    {
        //Métodos del servicio web
        //...
        [WebMethod]
        public int NuevoClienteSimple(string nombre, int telefono)
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=DBUSM;Integrated Security=True");

            con.Open();

            string sql = "INSERT INTO Clientes (Nombre, Telefono) VALUES (@nombre, @telefono)";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = nombre;
            cmd.Parameters.Add("@telefono", System.Data.SqlDbType.Int).Value = telefono;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public horario_profesor[] ListadoClientes()
        {
            SqlConnection con =
                new SqlConnection(
                    @"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=DBCLIENTES;Integrated Security=True");

            con.Open();

            string sql = "SELECT IdCliente, Nombre, Telefono FROM Clientes";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader = cmd.ExecuteReader();

            List<horario_profesor> lista = new List<horario_profesor>();

            while (reader.Read())
            {
                lista.Add(
                    new horario_profesor(reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetInt32(2)));
            }

            con.Close();

            return lista.ToArray();
        }
        [WebMethod]
        public int NuevoClienteObjeto(horario_profesor cliente)
        {
            SqlConnection con = new SqlConnection(@"Data Source=EQUIPO-KEVIN\SQLSERVER;Initial Catalog=DBCLIENTES;Integrated Security=True");

            con.Open();

            string sql = "INSERT INTO Clientes (Nombre, Telefono) VALUES (@nombre, @telefono)";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cliente.dia;
            cmd.Parameters.Add("@telefono", System.Data.SqlDbType.Int).Value = cliente.bloque;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }




    }
}