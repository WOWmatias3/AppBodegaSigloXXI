using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;

namespace DAL
{
    public class PlatoDAL
    {
        public int id_plato { get; set; }
        public string nombre_plato { get; set; }
        public int precio { get; set; }
        public int stock_preparado { get; set; }
        public int stock_a_preparar { get; set; }
        public string habilitado { get; set; }

        public PlatoDAL()
        {
        }

        public PlatoDAL(int id_plato, string nombre_plato, int precio, int stock_preparado, int stock_a_preparar, string habilitado)
        {
            this.id_plato = id_plato;
            this.nombre_plato = nombre_plato;
            this.precio = precio;
            this.stock_preparado = stock_preparado;
            this.stock_a_preparar = stock_a_preparar;
            this.habilitado = habilitado;
        }

        public DataTable getPlatosHabilitados()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_allplatosHabilitados", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                con.Close();
                using (DataTable dt = new DataTable())
                {
                    OracleDataAdapter adapter = new OracleDataAdapter(cm);
                    adapter.Fill(dt);
                    return dt;

                }
            }
        }


        public DataTable getIngredientesFromPlato(int id_plat)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetIngredientesFromPlato", con);
                cm.BindByName = true;
                cm.CommandType = CommandType.StoredProcedure;

                cm.Parameters.Add("id_plat", OracleDbType.Varchar2).Value = id_plat;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                OracleDataReader reader = ((OracleRefCursor)output.Value).GetDataReader();
                con.Close();
                using (DataTable dt = new DataTable())
                {
                    OracleDataAdapter adapter = new OracleDataAdapter(cm);
                    adapter.Fill(dt);
                    return dt;

                }
            }
        }

        public bool UpdatePlatoStock(int id_pla, int cantidad_nueva)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetStockPlato", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("idplato", OracleDbType.Int32).Value = id_pla;
                    objCmd.Parameters.Add("cantidad", OracleDbType.Double).Value = cantidad_nueva;

                    con.Open();
                    objCmd.ExecuteNonQuery();
                    con.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
                return false;
            }

        }


    }
}
