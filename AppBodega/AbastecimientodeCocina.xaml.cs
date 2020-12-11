using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using BLL;
using System.Data;
using System.Media;

namespace AppBodega
{

    /// <summary>
    /// Lógica de interacción para AbastecimientodeCocina.xaml
    /// </summary>
    public partial class AbastecimientodeCocina : MetroWindow
    {

        public DataTable ingredientesDT = new DataTable();

        public DataTable PlatosACocina = new DataTable();
        public DataTable IngredientesAEnviar = new DataTable();

        public DataTable Ing1Plato = new DataTable();

        public string username = null;
        public AbastecimientodeCocina()
        {
            InitializeComponent();
            PlatosACocina.Clear();
            IngredientesAEnviar.Clear();

            PlatosACocina.Columns.Add("id");
            PlatosACocina.Columns.Add("nombre");
            PlatosACocina.Columns.Add("cantidad");

            IngredientesAEnviar.Columns.Add("id");
            IngredientesAEnviar.Columns.Add("nombre");
            IngredientesAEnviar.Columns.Add("cantidad");


            Ing1Plato.Columns.Add("Nombre");
            Ing1Plato.Columns.Add("Cantidad");

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lblUser.Content = username;
            fillCbPlatos();
            fillDgIngredientes();


        }

        public void fillCbPlatos()
        {
            PlatoBLL plbll = new PlatoBLL();
            DataTable dt = new DataTable();
            dt = plbll.GetPlatosHabilitados();
            
            cbPlatos.ItemsSource = dt.DefaultView;
            cbPlatos.DisplayMemberPath = "NOMBRE_PLATO";
            cbPlatos.SelectedValuePath = "ID_PLATO";
            cbPlatos.SelectedIndex = 0;
        }

        public void fillDgIngredientes()
        {
            ingredienteBLL inBLL = new ingredienteBLL();
            DataTable dt = new DataTable();
            dt = inBLL.AllingredientesList();
            ingredientesDT = dt;
            dtgIngredientesDisp.ItemsSource = dt.DefaultView;
        }




        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Bodega bod = new Bodega();
            Close();
            bod.ShowDialog();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int idplato = Int32.Parse(cbPlatos.SelectedValue.ToString());
                DataTable DtIngredientesNecesarios = new DataTable();

                DataTable IngredientesTemporal = ingredientesDT;


                PlatoBLL plBLL = new PlatoBLL();
                DtIngredientesNecesarios = plBLL.GetIngredientesPlato(idplato);
                bool estadoComprovacion = true;

                if (Int32.Parse(txtCantidad.Text) > 0)
                {
                    foreach (DataRow ingNec in DtIngredientesNecesarios.Rows)
                    {


                        double cantidadNecesaria = double.Parse(ingNec[2].ToString()) * Int32.Parse(txtCantidad.Text);
                        int id_ingredienteNec = Int32.Parse(ingNec[1].ToString());

                        foreach (DataRow row in IngredientesTemporal.Rows)
                        {
                            string prueba1 = row[0].ToString();
                            string prueba2 = row[1].ToString();
                            string prueba3 = row[2].ToString();

                            int id_ingrediente = Int32.Parse(row[0].ToString());

                            if (id_ingrediente == id_ingredienteNec)
                            {
                                double stockActual = double.Parse(row[2].ToString());

                                if (cantidadNecesaria > stockActual)
                                {
                                    MessageBox.Show("Insuficiente Stock de: " + prueba2);
                                    estadoComprovacion = false;
                                    break;

                                }
                                row[2] = stockActual - cantidadNecesaria;

                            }

                        }
                        if (estadoComprovacion == false)
                        {
                            break;
                        }
                    }
                    if (estadoComprovacion)
                    {
                        MessageBox.Show("Se ha agregado el plato");
                        ingredientesDT = IngredientesTemporal;
                        dtgIngredientesDisp.ItemsSource = IngredientesTemporal.DefaultView;
                        actualizadtgIngredientes(idplato);


                        actualizardtgplatos(idplato, int.Parse(txtCantidad.Text));


                    }
                    else
                    {
                        dtgIngredientesDisp.ItemsSource = ingredientesDT.DefaultView;

                    }
                }
                else
                {
                    lb1.Content = "Ingrese una cantidad mayor a 0";
                }
            }
            catch
            {

            }
        }

        private void actualizadtgIngredientes(int id_plato)
        {
            PlatoBLL plBLL = new PlatoBLL();
            DataTable DtIngredientesNecesarios = new DataTable();
            DtIngredientesNecesarios = plBLL.GetIngredientesPlato(id_plato);
            bool coincide ;

            foreach (DataRow ingNec in DtIngredientesNecesarios.Rows)
            {
                double cantidadNecesaria = double.Parse(ingNec[2].ToString()) * Int32.Parse(txtCantidad.Text);
                int id_ingredienteNec = Int32.Parse(ingNec[1].ToString());
                string nombre = ingNec[3].ToString();

                coincide = false;

                foreach (DataRow row in IngredientesAEnviar.Rows)
                {

                    int id_ingrediente = Int32.Parse(row["id"].ToString());

                    if (id_ingrediente == id_ingredienteNec)
                    {
                        row["cantidad"] = double.Parse(row["cantidad"].ToString()) + cantidadNecesaria;
                        dtgIngEnviar.ItemsSource = IngredientesAEnviar.DefaultView;
                        coincide = true;
                        break;
                    } 
                }
                if (!coincide)
                {
                    DataRow row = IngredientesAEnviar.NewRow();
                    row["id"] = id_ingredienteNec;
                    row["nombre"] = nombre;
                    row["cantidad"] = cantidadNecesaria;

                    IngredientesAEnviar.Rows.Add(row);

                    dtgIngEnviar.ItemsSource = IngredientesAEnviar.DefaultView;
                }


            }
        }

        public void actualizardtgplatos(int id,int cantidad)
        {
            if (cantidad > 0)
            {

                bool coincide = false;
                foreach (DataRow line in PlatosACocina.Rows)
                {
                    int id_plato = int.Parse(line["id"].ToString());
                    int cant = int.Parse(line["cantidad"].ToString());


                    //PlatoBLL ptbll = new PlatoBLL();
                    if (id_plato == id)
                    {
                        line["cantidad"] = int.Parse(line["cantidad"].ToString()) + cantidad;
                        dtgPlatosEnviar.ItemsSource = PlatosACocina.DefaultView;
                        coincide = true;
                        break;
                    }
                    else {
                    }
                }
                if (coincide == false)
                {
                    DataRow row = PlatosACocina.NewRow();
                    row["id"] = id;
                    row["nombre"] = cbPlatos.Text;
                    row["cantidad"] = txtCantidad.Text;

                    PlatosACocina.Rows.Add(row);

                    dtgPlatosEnviar.ItemsSource = PlatosACocina.DefaultView;
                }
                PlatosACocina.AcceptChanges();
                

            }
            else
            {
                MessageBox.Show("Por Favor Ingrese todos los Campos al Agregar un ingrediente.");

            }
        }





        private void CbPlatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id_plato =Int32.Parse( cbPlatos.SelectedValue.ToString());
            Ing1Plato.Clear();
            PlatoBLL plBLL = new PlatoBLL();
            DataTable DtIngredientesNecesarios = new DataTable();
            DtIngredientesNecesarios = plBLL.GetIngredientesPlato(id_plato);
            foreach(DataRow row in DtIngredientesNecesarios.Rows)
            {
                DataRow line = Ing1Plato.NewRow();
                line["Nombre"] = row[3];
                line["Cantidad"] = row[2];

                Ing1Plato.Rows.Add(line);
            }


            dtgingredientes1plato.ItemsSource = Ing1Plato.DefaultView;
        }

        private void Btnchanges_Click(object sender, RoutedEventArgs e)
        {

        }



        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable UpdateIngredientes = ingredientesDT.GetChanges();
                foreach (DataRow row in UpdateIngredientes.Rows)
                {
                    ingredienteBLL ingBLL = new ingredienteBLL();
                    int id = int.Parse(row[0].ToString());
                    double cantidad = double.Parse(row[2].ToString());
                    ingBLL.UpdateStockIngrediente(id, cantidad);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
            }

            try
            {
                DataTable UpdatePlato = PlatosACocina;
                foreach (DataRow row in UpdatePlato.Rows)
                {
                    PlatoBLL plaBLL = new PlatoBLL();
                    int id = int.Parse(row[0].ToString());
                    int cantidad = int.Parse(row[2].ToString());
                    plaBLL.SetStockPlato(id, cantidad);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());

            }

            MessageBox.Show("Se han enviado correctamente el registro a cocina");
            ReloadPage();
        }

        public void ReloadPage()
        {
            InitializeComponent();
            PlatosACocina.Clear();
            IngredientesAEnviar.Clear();

            dtgIngEnviar.ItemsSource = IngredientesAEnviar.DefaultView;
            dtgPlatosEnviar.ItemsSource = PlatosACocina.DefaultView;
            ingredientesDT.AcceptChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ingredientesDT.RejectChanges();
            dtgIngredientesDisp.ItemsSource = ingredientesDT.DefaultView;
            PlatosACocina.Clear();
            IngredientesAEnviar.Clear();

            dtgIngEnviar.ItemsSource = IngredientesAEnviar.DefaultView;
            dtgPlatosEnviar.ItemsSource = PlatosACocina.DefaultView;

        }

        private void TxtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txtCantidad.Text.Length;

            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 48 && ascii <= 57)
            {
                if (tamanio < 5)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;

                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }

        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            lb1.Content = "";
        }
    }
}
