using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace DAL
{
    public class usuarioDAL
    {
        int id_usuario { get; set; }
        string nom_usuario { get; set; }
        string clave { get; set; }
        string rol { get; set; }


        public usuarioDAL()
        {
        }

        public usuarioDAL(int id_usuario, string nom_usuario, string clave, string rol)
        {
            this.id_usuario = id_usuario;
            this.nom_usuario = nom_usuario;
            this.clave = clave;
            this.rol = rol;
        }
        private string encriptador(string palabra)
        {
            SHA256 sha = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha.ComputeHash(encoding.GetBytes(palabra));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }

        public int Getrut(string nomuser, string pass)
        {
            try
            {

                OracleConnection con = new conexion().Conexion();
                con.Open();

                OracleCommand com = new OracleCommand("get_userpass", con);
                com.BindByName = true;
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = nomuser;
                OracleParameter output = com.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                com.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                string shaword = encriptador(pass);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int rut = Int32.Parse(reader[0].ToString());
                        nom_usuario = reader[1].ToString();
                        clave = reader[2].ToString();
                        rol = reader[3].ToString();
                        if (nom_usuario == nomuser && clave == shaword && rol == "Bodeguero")
                        {
                            con.Close();
                            return rut;
                        }

                    }
                }
                con.Close();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
                return 0;
            }
        }



        public string Get_nombrecompleto(int rut)
        {
            try
            {
                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleCommand cm = new OracleCommand("Get_nombre_usuarioby_Rut", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;
                    cm.Parameters.Add("p_rut", OracleDbType.Int32).Value = rut;
                    OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Varchar2, 40);
                    output.Direction = System.Data.ParameterDirection.ReturnValue;
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    return output.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
                return "";
            }
        }

        public bool getLogin(string nomuser, string pass)
        {
            try
            {

                OracleConnection con = new conexion().Conexion();
                con.Open();

                OracleCommand com = new OracleCommand("get_userpass", con);
                com.BindByName = true;
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = nomuser;
                OracleParameter output = com.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                com.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                string shaword = encriptador(pass);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nom_usuario = reader[1].ToString();
                        clave = reader[2].ToString();
                        rol = reader[3].ToString();
                        if (nom_usuario == nomuser && clave == shaword && rol == "Bodeguero")
                        {
                            con.Close();
                            return true;
                        }

                    }
                }
                con.Close();

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
