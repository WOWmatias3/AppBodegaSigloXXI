using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using MahApps.Metro.Controls;
using Tulpep.NotificationWindow;

namespace AppBodega
{
    /// <summary>
    /// Lógica de interacción para ListadodeInsumos.xaml
    /// </summary>
    public partial class ListadodeInsumos : MetroWindow
    {
        public string username = null;
        DataTable dtlist = new DataTable();
        DataTable dtlb = new DataTable();
        public ListadodeInsumos()
        {

            InitializeComponent();


        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnBuscar.IsEnabled = false;
            btnListar_critico.IsEnabled = false;
            txt_bebestible.IsEnabled = false;
            txt_Ingredientes.IsEnabled = false;
            lblUser.Content = username;
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Bodega bod = new Bodega();
            Close();
            bod.ShowDialog();
        }

        private void rbtIngredientes_Checked(object sender, RoutedEventArgs e)
        {

            txt_bebestible.Visibility = Visibility.Hidden;
            txt_Ingredientes.Visibility = Visibility.Visible;
            txt_bebestible.Text = "";
            txt_Ingredientes.Text = "";
            lblInsumo.Content = "Ingrese el nombre del Ingrediente: ";
            btnBuscar.IsEnabled = true;
            btnListar_critico.IsEnabled = true;
            txt_bebestible.IsEnabled = false;
            txt_Ingredientes.IsEnabled = true;

        }

        private void rbtBebestibles_Checked(object sender, RoutedEventArgs e)
        {
            txt_Ingredientes.Visibility = Visibility.Hidden;
            txt_bebestible.Visibility = Visibility.Visible;
            txt_bebestible.Text = "";
            txt_Ingredientes.Text = "";
            lblInsumo.Content = "Ingrese el nombre del Bebestible: ";
            btnBuscar.IsEnabled = true;
            btnListar_critico.IsEnabled = true;
            txt_bebestible.IsEnabled = true;
            txt_Ingredientes.IsEnabled = false;
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            usuarioBLL ub = new usuarioBLL();

            if (rbtIngredientes.IsChecked == true)
            {
                ingredienteBLL ib = new ingredienteBLL();
                System.Data.DataTable dt = ib.GetAllIngredientes();
                dgListado.ItemsSource = dt.DefaultView;
            }
            else if (rbtBebestibles.IsChecked == true)
            {
                bebestibleBLL ib = new bebestibleBLL();
                System.Data.DataTable dt = ib.Get_allbebest();
                dgListado.ItemsSource = dt.DefaultView;
            }
        }



        private void lblCompletar4_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar4.Visibility = Visibility.Hidden;
        }



        private void Txt_Ingredientes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txt_Ingredientes.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 65 && ascii <= 90 || ascii >= 97 && ascii <= 122)
            {
                string texto = txt_Ingredientes.Text;
                if (texto == "")
                {
                    ingredienteBLL ib = new ingredienteBLL();
                    System.Data.DataTable dt = ib.GetAllIngredientes();
                    dgListado.ItemsSource = dt.DefaultView;
                }
                else
                {
                    if (tamanio < 30)
                    {
                        e.Handled = false;
                        ingredienteBLL ib = new ingredienteBLL();
                        string nombre_ins = txt_Ingredientes.Text;
                        System.Data.DataTable dt = ib.getIngrediente(nombre_ins);
                        dgListado.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Txt_bebestible_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txt_Ingredientes.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 65 && ascii <= 90 || ascii >= 97 && ascii <= 122)
            {
                if (tamanio < 30)
                {
                    e.Handled = false;
                    bebestibleBLL ib = new bebestibleBLL();
                    string nombre_ins = txt_bebestible.Text;
                    System.Data.DataTable dt = ib.Get_BebestibleByName(nombre_ins);
                    dgListado.ItemsSource = dt.DefaultView;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void DgListado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                string uno = dgListado.SelectedItems[0].ToString();

                int index = dgListado.SelectedIndex;
                object item = dgListado.SelectedItem;
                if (rbtBebestibles.IsChecked == true)
                {
                    try
                    {
                        if (item != null)
                        {

                            int ID = Int32.Parse((dgListado.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                            string Nombre = (dgListado.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                            string Proveedor = (dgListado.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock = (dgListado.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock_bar = (dgListado.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock_critico = (dgListado.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;

                            if (dtlb.Columns.Count == 0)
                            {
                                dtlb.Clear();
                                dtlb.Columns.Add("id");
                                dtlb.Columns.Add("nombre");
                                dtlb.Columns.Add("proveedor");
                                dtlb.Columns.Add("stock");
                                dtlb.Columns.Add("stock bar");
                                dtlb.Columns.Add("stock critico");


                            }
                            else
                            {

                            }

                            dtg_2.ItemsSource = Listados(ID.ToString(), Nombre, Proveedor, Stock, Stock_bar, index, Stock_critico).DefaultView;

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex);
                    }
                }
                else if (rbtIngredientes.IsChecked == true)
                {


                    try
                    {
                        if (item != null)
                        {

                            int ID = Int32.Parse((dgListado.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                            string Nombre = (dgListado.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                            string Proveedor = (dgListado.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock = (dgListado.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock_Cocina = (dgListado.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                            string Stock_critico = (dgListado.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                            if (dtlist.Columns.Count == 0)
                            {

                                dtlist.Clear();
                                dtlist.Columns.Add("id");
                                dtlist.Columns.Add("nombre");
                                dtlist.Columns.Add("proveedor");
                                dtlist.Columns.Add("stock");
                                dtlist.Columns.Add("stock cocina");
                                dtlist.Columns.Add("stock critico");


                            }
                            else
                            {

                            }
                            dtg_2.ItemsSource = Listado(ID.ToString(), Nombre, Proveedor, Stock, Stock_Cocina, index, Stock_critico).DefaultView;

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex);
                    }
                }
                else
                {

                }
            }
            catch
            {

            }


        }


        private DataTable Listado(string id, string nombre, string proveedor, string stock, string stock_cocina, int index, string stockcritico)
        {

            bool coincide = false;
            foreach (DataRow line in dtlist.Rows)
            {

                int idd = Int32.Parse(line["id"].ToString());
                string nom = line["nombre"].ToString();
                string proc = line["proveedor"].ToString();
                string stk = line["stock"].ToString();
                string stkcocina = line["stock cocina"].ToString();
                string stkcritico = line["stock critico"].ToString();

                if (Int32.Parse(id) == Int32.Parse(line["id"].ToString()))
                {
                    coincide = true;
                    break;
                }
                else { }
            }
            if (coincide == false)
            {
                DataRow row = dtlist.NewRow();
                row["id"] = id;
                row["nombre"] = nombre;
                row["proveedor"] = proveedor;
                row["stock"] = stock;
                row["stock cocina"] = stock_cocina;
                row["stock critico"] = stockcritico;
                dtlist.Rows.Add(row);

            }
            dtlist.AcceptChanges();

            return dtlist;
        }



        private DataTable Listados(string id, string nombre, string proveedor, string stock, string stock_cocina, int index, string stockcritico)
        {

            bool coincide = false;
            foreach (DataRow line in dtlb.Rows)
            {

                int idd = Int32.Parse(line["id"].ToString());
                string nom = line["nombre"].ToString();
                string proc = line["proveedor"].ToString();
                string stk = line["stock"].ToString();
                string stkcocina = line["stock bar"].ToString();
                string stkcritico = line["stock critico"].ToString();

                if (Int32.Parse(id) == Int32.Parse(line["id"].ToString()))
                {
                    coincide = true;
                    break;
                }
                else { }
            }
            if (coincide == false)
            {
                DataRow row = dtlb.NewRow();
                row["id"] = id;
                row["nombre"] = nombre;
                row["proveedor"] = proveedor;
                row["stock"] = stock;
                row["stock bar"] = stock_cocina;
                row["stock critico"] = stockcritico;

                dtlb.Rows.Add(row);

            }
            dtlb.AcceptChanges();

            return dtlb;
        }

        private void Btn_add_row_Click(object sender, RoutedEventArgs e)
        {

            PedidoBLL pb = new PedidoBLL();
            ingredienteBLL ib = new ingredienteBLL();
            bebestibleBLL bb = new bebestibleBLL();

            DataTable dt = pb.Get_ultimopedido();
            DataRow row = dt.Rows[0];
            int id = Int32.Parse(row[0].ToString());
            int idactual = id + 1;


            if (rbtBebestibles.IsChecked == true)
            {
                foreach (DataRow line in dtlb.Rows)
                {
                    DataTable dtt = pb.Get_ultimopedido();
                    DataRow roww = dtt.Rows[0];
                    int idd = Int32.Parse(roww[0].ToString());
                    DateTime fecha = System.DateTime.Now;
                    int monto = 0;
                    int cant = 0;
                    string nombre = line["nombre"].ToString();
                    int id_insumo = bb.Get_bebyid(nombre);
                    string prov = line["proveedor"].ToString();
                    int id_prov = pb.get_idprovbyname(prov);
                    string stock = line["stock"].ToString();
                    string stock_cocina = line["stock bar"].ToString();
                    string stock_critico = line["stock critico"].ToString();
                    string estado = "EN ESPERA DE APROBACIÓN";
                    string tipo = "Pedido de bebestibles";


                    if (idd == idactual)
                    {
                        pb.InsertarDetallePedido(idactual, monto, estado, id_insumo, id_prov, tipo, cant);
                    }
                    else
                    {
                        pb.Insert_Pedido(fecha, monto);
                        pb.InsertarDetallePedido(idactual, monto, estado, id_insumo, id_prov, tipo, cant);
                    }
                }
                dt.Clear();
            }

            else if (rbtIngredientes.IsChecked == true)
            {
                foreach (DataRow line in dtlist.Rows)
                {

                    DataTable dtt = pb.Get_ultimopedido();
                    DataRow roww = dtt.Rows[0];
                    int idd = Int32.Parse(roww[0].ToString());

                    DateTime fecha = System.DateTime.Now;

                    int monto = 0;

                    int cant = 0;
                    string nombre = line["nombre"].ToString();
                    int id_insumo = ib.Get_idbynom(nombre);
                    string prov = line["proveedor"].ToString();
                    int id_prov = pb.get_idprovbyname(prov);
                    string stock = line["stock"].ToString();
                    string stock_cocina = line["stock cocina"].ToString();
                    string estado = "EN ESPERA DE APROBACIÓN";
                    string tipo = "Pedido de ingredientes";

                    if (idd == idactual)
                    {

                        pb.InsertarDetallePedido(idactual, monto, estado, id_insumo, id_prov, tipo, cant);
                    }
                    else
                    {
                        pb.Insert_Pedido(fecha, monto);
                        pb.InsertarDetallePedido(idactual, monto, estado, id_insumo, id_prov, tipo, cant);
                    }
                }
                dtg_2.ItemsSource = null;
                PopupNotifier popup = new PopupNotifier();
                popup.TitleText = "Aviso";
                popup.Image = Properties.Resources.add;
                popup.ContentText = "Se ha enviado la solicitud de insumos al administrador \n de forma exitosa";
                popup.AnimationDuration = 800;
                popup.Delay = 1000;
                popup.Popup();

            }


        }

        private void BtnListar_critico_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtcriticos = new DataTable();
            bebestibleBLL bb = new bebestibleBLL();
            ingredienteBLL ib = new ingredienteBLL();
            if (rbtBebestibles.IsChecked == true)
            {
                dtcriticos = bb.Get_bebCritico();
                dgListado.ItemsSource = dtcriticos.DefaultView;
            }
            else if (rbtIngredientes.IsChecked == true)
            {
                dtcriticos = ib.Get_ingCritico();
                dgListado.ItemsSource = dtcriticos.DefaultView;
            }


        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            dtg_2.ItemsSource = null;
        }
    }
}
