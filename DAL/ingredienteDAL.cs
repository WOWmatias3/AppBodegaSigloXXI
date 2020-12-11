﻿using System;
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
    public class ingredienteDAL
    {
        public int id_ingrediente { get; set; }
        public string nombre_ins { get; set; }     
        public double stock { get; set; }




        public static ingredienteDAL instance = null;


        public ingredienteDAL()
        { 

        }

        public static ingredienteDAL Getinstancia() 
        {
            if (instance == null)
            {
                instance = new ingredienteDAL();
            }
            return instance;
        }
        public ingredienteDAL(int id_ingrediente,
       string nombre_ins,
       double stock
       )
     
        {
            this.id_ingrediente = id_ingrediente;
            this.nombre_ins = nombre_ins;
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
        public bool SolicitarInsumo(ingredienteDAL objingDAL)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SolicitarInsumoIngrediente", con);
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


        public DataTable getIngredientes()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetallIngredientes", con);
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

        public DataTable BuscaringNomb(string nombre_ins)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("getListarNombre", con);
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

        

        public bool UpdateIngredienteStock(int id_ing, double cantidad_nueva)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetStockIngrediente", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("iding", OracleDbType.Int32).Value = id_ing;
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

        //Update2

        public bool AgregaIngredienteStock(int id_ing, double cantidad_agregar)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetAddStockingrediente", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("iding", OracleDbType.Int32).Value = id_ing;
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

        public bool SetOnlyIngredienteStock(int id_ing, double cantidad_nueva)
        {
            try
            {

                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand objCmd = new OracleCommand("SetOnlyStockingrediente", con);
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.BindByName = true;
                    objCmd.Parameters.Add("iding", OracleDbType.Int32).Value = id_ing;
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



        public DataTable getIngredientesHab()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_allIngredientesHabilitados", con);
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

        public double GetStockIngrediente(int id)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("GetStockingrediente", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("id_ing", OracleDbType.Int32).Value = id;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.Double);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                cm.ExecuteNonQuery();

                double valor = double.Parse( output.Value.ToString());

                return valor;
            }
        }

        

        public DataTable GetAllIngredientes()
        {
            try
            {
                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleCommand cm = new OracleCommand("Get_alling_Pedido", con);
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
            catch(Exception ex)
            {
                Console.WriteLine("" + ex);
                return null;
            }
        }


        public DataTable Get_ing_critico()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_alling_Pedido_critico", con);
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




        public int Get_iding_nomb(string nombre_ins)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_idingbynombre", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.Parameters.Add("nomb", OracleDbType.Varchar2).Value = nombre_ins;
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

    }
}
