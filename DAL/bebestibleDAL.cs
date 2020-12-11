using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Security.Cryptography;
using System.Text;

namespace DAL
{

    public class bebestibleDAL
    {
        public int id_bebestible { get; set; }
        public string nombre_ins { get; set; }
        public Double stock { get; set; }





        public static bebestibleDAL instance = null;


        public bebestibleDAL()
        {

        }

        public static bebestibleDAL Getinstancia()
        {
            if (instance == null)
            {
                instance = new bebestibleDAL();
            }
            return instance;
        }
        public bebestibleDAL(int id_bebestible,
       string nombre_bebestible,
       Double stock)

        {
            this.id_bebestible = id_bebestible;
            this.nombre_ins = nombre_bebestible;
            this.stock = stock;

        }
        /*
        public DataTable GetAllIng()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_alling", con);
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
        */

        public bool SolicitarBebestible(bebestibleDAL objingDAL)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SolicitarInsumoBebestible", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("p_nomb", OracleDbType.Varchar2).Value = nombre_ins;
                    objCmd.Parameters.Add("p_stock", OracleDbType.Double).Value = stock;


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


        public DataTable getBebestible()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetListarNombreB", con);
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


     


        public DataTable BusarBebestiblenombre(string nombre_ins)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetListarNombreB", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;

                cm.Parameters.Add("p_nomb", OracleDbType.Varchar2).Value = nombre_ins;

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


        public DataTable Get_bebestibleByName(string nombre_ins)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_nomBebestible", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;

                cm.Parameters.Add("p_nom", OracleDbType.Varchar2).Value = nombre_ins;

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

        //ACTUALIZAR STOCK INSUMOS

        public DataTable ListAllBebestiblesHab()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_allBebestiblesHabilitados", con);
                cm.BindByName = true;
                cm.CommandType = CommandType.StoredProcedure;

                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = ParameterDirection.ReturnValue;
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

        public double GetStockBebestible(int id)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetStockBebestible", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("id_beb", OracleDbType.Int32).Value = id;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Double);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                double valor = double.Parse(output.Value.ToString());

                return valor;
            }
        }

        //update2

        public bool AgregaBebestibleStock(int id_beb, int cantidad_agregar)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetAddStockBebestible", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("idbeb", OracleDbType.Int32).Value = id_beb;
                    objCmd.Parameters.Add("cantidad", OracleDbType.Double).Value = cantidad_agregar;

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

        public bool SetOnlyBebestibleStock(int id_beb, int cantidad_nueva)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetOnlyStockBebestible", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("idbeb", OracleDbType.Int32).Value = id_beb;
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

        public DataTable GetallBebest()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_allbebest", con);
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

        public int Get_bebyid(string nom)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_bebyid", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("nomb", OracleDbType.Varchar2).Value = nom;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Int32);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                    return Int32.Parse(output.Value.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                    return 0;
                }


            }
        }




        public DataTable Get_beb_critico()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_allbebest_critico", con);
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
    }
}
