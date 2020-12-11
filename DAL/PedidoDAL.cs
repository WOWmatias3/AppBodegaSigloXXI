using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class PedidoDAL
    {
        public int id_pedido { get; set; }
        public int cantidad { get; set; }
        public int monto_total { get; set; }
        public DateTime fecha { get; set; }
        public int id_insumo { get; set; }
        public int id_proveedor { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }

        public PedidoDAL()
        {

        }

        public PedidoDAL(int id_pedido, int cantidad, int monto_total, DateTime fecha, int id_insumo, int id_proveedor, string estado, string tipo)
        {
            this.id_pedido = id_pedido;
            this.cantidad = cantidad;
            this.monto_total = monto_total;
            this.fecha = fecha;
            this.id_insumo = id_insumo;
            this.id_proveedor = id_proveedor;
            this.estado = estado;
            this.tipo = tipo;
        }

        /* public void Insert_DetallePedido(PedidoDAL objpedido)
         {
             try
             {
                 using (OracleConnection con = new conexion().Conexion())
                 {
                     OracleDataAdapter da = new OracleDataAdapter();
                     OracleCommand cm = new OracleCommand("Insert_detalle_pedido", con);
                     cm.BindByName = true;
                     cm.CommandType = System.Data.CommandType.StoredProcedure;
                     cm.Parameters.Add("pd_id", OracleDbType.Int32).Value = id_pedido;
                     cm.Parameters.Add("monto", OracleDbType.Int32).Value = monto_total;
                     cm.Parameters.Add("estado", OracleDbType.Varchar2).Value = estado;
                     cm.Parameters.Add("id_insumo", OracleDbType.Int32).Value = id_insumo;
                     cm.Parameters.Add("p_id_prov", OracleDbType.Int32).Value = id_proveedor;
                     cm.Parameters.Add("tipo", OracleDbType.Varchar2).Value = tipo;
                     cm.Parameters.Add("cant", OracleDbType.Int32).Value = cantidad;
                     con.Open();
                     cm.ExecuteNonQuery();
                     con.Close();
                 }

             }
             catch (Exception ex)
             {
                 Console.WriteLine("" + ex);
             }
         }*/


        public void Insert_DetallePedido(int id, int monto, string estado_, int id_insumo_, int p_id_prov, string tipo_, int cant)
        {
            try
            {
                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleCommand cm = new OracleCommand("restaurant.Insert_detalle_pedido", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;
                    cm.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cm.Parameters.Add("monto", OracleDbType.Int32).Value = monto;
                    cm.Parameters.Add("estado", OracleDbType.Varchar2).Value = estado_;
                    cm.Parameters.Add("id_insumo", OracleDbType.Int32).Value = id_insumo_;
                    cm.Parameters.Add("p_id_prov", OracleDbType.Int32).Value = p_id_prov;
                    cm.Parameters.Add("tipo", OracleDbType.Varchar2).Value = tipo_;
                    cm.Parameters.Add("cant", OracleDbType.Int32).Value = cant;
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
            }
        }

        public void Insert_Pedido(DateTime fecha, int monto)
        {
            try
            {
                using (OracleConnection con = new conexion().Conexion())
                {
                    OracleDataAdapter da = new OracleDataAdapter();
                    OracleCommand cm = new OracleCommand("InsertPedido", con);
                    cm.BindByName = true;
                    cm.CommandType = System.Data.CommandType.StoredProcedure;
                    cm.Parameters.Add("fecha", OracleDbType.Date).Value = fecha;
                    cm.Parameters.Add("monto", OracleDbType.Int32).Value = monto;

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
            }
        }


        public DataTable Get_ultimopedido()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_ultimopedido", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                }
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




        public DataTable Get_allpedidos()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_all_pedidos", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                }
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


        public DataTable Get_allpedidos_hoy()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_all_pedidos_hoy", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                }
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

        public DataTable Get_allpedidos_semana()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_all_pedidos_semana", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                }
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
        public DataTable Get_allpedidos_mes()
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_all_pedidos_mes", con);
                cm.BindByName = true;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                output.Direction = System.Data.ParameterDirection.ReturnValue;
                con.Open();
                try
                {
                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex);
                }
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
        /*-------------------------------------------------------------------------------------------------------------

                                                                             | | | | | 
                                              INSERTAR EN  CLASE PROVEEDOR   | | | | | 
                                                                             V V V V V     

           --------------------------------------------------------------------------------------------------------------*/

        public int Get_idprovbynom(string nombre_ins)
        {
            using (OracleConnection con = new conexion().Conexion())
            {
                OracleCommand cm = new OracleCommand("Get_idprovbyName", con);
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



        /* public int Get_ultimopedido(string nombre_ins)
         {
             using (OracleConnection con = new conexion().Conexion())
             {
                 OracleCommand cm = new OracleCommand("Get_ultimopedido", con);
                 cm.BindByName = true;
                 cm.CommandType = System.Data.CommandType.StoredProcedure;

                 OracleParameter output = cm.Parameters.Add("my_cursor", OracleDbType.RefCursor);
                 output.Direction = System.Data.ParameterDirection.ReturnValue;
                 con.Open();
                 try
                 {
                     OracleDataReader reader = cm.ExecuteReader();
                     int id = Int32.Parse(reader.GetValue(0).ToString());
                     return id;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine("" + ex);
                     return 0;
                 }


             }
         }*/




    }
}
