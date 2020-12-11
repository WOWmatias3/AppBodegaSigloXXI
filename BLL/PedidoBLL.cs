using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PedidoBLL
    {
        public int id_pedido { get; set; }
        public int cantidad { get; set; }
        public int monto_total { get; set; }
        public DateTime fecha { get; set; }
        public int id_insumo { get; set; }
        public int id_proveedor { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }

        public PedidoBLL()
        {

        }

        public PedidoBLL(int id_pedido, int cantidad, int monto_total, DateTime fecha, int id_insumo, int id_proveedor, string estado, string tipo)
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



        /*public void Insert_Detalle_pedido(PedidoBLL objpedido)
        {
            PedidoDAL pd = new PedidoDAL();
            pd.id_pedido = this.id_pedido;
            pd.cantidad = this.cantidad;
            pd.monto_total = this.monto_total;
            pd.fecha = this.fecha;
            pd.id_insumo = this.id_insumo;
            pd.id_proveedor = this.id_proveedor;
            pd.estado = this.estado;
            pd.tipo = this.tipo;
            pd.Insert_DetallePedido(pd);

        }*/
        public void InsertarDetallePedido(int id, int monto, string estado_, int id_insumo_, int p_id_prov, string tipo_, int cant)
        {
            PedidoDAL pd = new PedidoDAL();
            pd.Insert_DetallePedido(id, monto, estado_, id_insumo_, p_id_prov, tipo_, cant);
        }

        public void Insert_Pedido(DateTime fecha, int monto)
        {
            PedidoDAL pd = new PedidoDAL();
            pd.Insert_Pedido(fecha, monto);
        }


        public DataTable Get_iding_nomb(string nomb)
        {
            PedidoDAL pd = new PedidoDAL();
            DataTable dt = Get_iding_nomb(nomb);
            return dt;
        }

        public DataTable Get_ultimopedido()
        {
            PedidoDAL pd = new PedidoDAL();
            DataTable dt = pd.Get_ultimopedido();
            return dt;
        }


        public DataTable Get_all_pedidos()
        {
            PedidoDAL pd = new PedidoDAL();
            return pd.Get_allpedidos();
        }

        public DataTable Get_all_pedidos_hoy()
        {
            PedidoDAL pd = new PedidoDAL();
            return pd.Get_allpedidos_hoy();
        }

        public DataTable Get_all_pedidos_semana()
        {
            PedidoDAL pd = new PedidoDAL();
            return pd.Get_allpedidos_semana();
        }

        public DataTable Get_all_pedidos_mes()
        {
            PedidoDAL pd = new PedidoDAL();
            return pd.Get_allpedidos_mes();
        }

        /*-------------------------------------------------------------------------------------------------------------

                                                                               | | | | | 
                                                INSERTAR EN  CLASE PROVEEDOR   | | | | | 
                                                                               V V V V V     

        --------------------------------------------------------------------------------------------------------------*/


        public int get_idprovbyname(string nom)
        {
            PedidoDAL pd = new PedidoDAL();
            return pd.Get_idprovbynom(nom);
        }
    }
}
