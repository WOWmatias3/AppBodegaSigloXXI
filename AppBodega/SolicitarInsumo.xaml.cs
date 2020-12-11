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
using BLL;
using MahApps.Metro.Controls;



namespace AppBodega
{
    /// <summary>
    /// Lógica de interacción para Ingreso_de_Insumos.xaml
    /// </summary>
    public partial class Solicitar_Insumo : MetroWindow
    {

        public string username = null;

        public Solicitar_Insumo()
        {
            InitializeComponent();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lblUser.Content = username;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Bodega bod = new Bodega();
            Close();
            bod.ShowDialog();
        }


        
        private void txtInsumos_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txtInsumos.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 65 && ascii <= 90 || ascii >= 97 && ascii <= 122)
            {
                if (tamanio < 100)
                {
                    e.Handled = false;
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
        private void txtBebestible_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txtBebestible.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 65 && ascii <= 90 || ascii >= 97 && ascii <= 122)
            {
                if (tamanio < 100)
                {
                    e.Handled = false;
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

        private void txtCantidad_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txtCantidad.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 48 && ascii <= 57)
            {
                if (tamanio < 9)
                {
                    e.Handled = false;
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
        private void txtcantBebestible_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int tamanio = txtcantBebestible.Text.Length;
            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascii >= 48 && ascii <= 57)
            {
                if (tamanio < 9)
                {
                    e.Handled = false;
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

        private void rbtIngredientes_Checked(object sender, RoutedEventArgs e)
        {
            txtInsumos.Text = "";
            txtCantidad.Text = "";
            txtInsumos.Visibility = Visibility.Visible;
            txtCantidad.Visibility = Visibility.Visible;
            lblNombre.Visibility = Visibility.Visible;
            lblCantidad.Visibility = Visibility.Visible;


            txtBebestible.Visibility = Visibility.Hidden;
            txtcantBebestible.Visibility = Visibility.Hidden;
            lblBebestible.Visibility = Visibility.Hidden;
            lblcantBebestible.Visibility = Visibility.Hidden;


            txtBebestible.Clear();
            txtcantBebestible.Clear();

        }

        private void rbtBebestibles_Checked(object sender, RoutedEventArgs e)
        {

            txtBebestible.Text = "";
            txtcantBebestible.Text = "";
            txtBebestible.Visibility = Visibility.Visible;
            txtcantBebestible.Visibility = Visibility.Visible;
            lblBebestible.Visibility = Visibility.Visible;
            lblcantBebestible.Visibility = Visibility.Visible;

            txtInsumos.Visibility = Visibility.Hidden;
            txtCantidad.Visibility = Visibility.Hidden;
            lblNombre.Visibility = Visibility.Hidden;
            lblCantidad.Visibility = Visibility.Hidden;

            txtInsumos.Clear();
            txtCantidad.Clear();

        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            bool txtingrediente1 = true;
            bool txtbebestible1 = true;
            bool txtingrediente2 = true;
            bool txtbebestible2 = true;

            if (txtInsumos.Text == "" && txtBebestible.Text == "")
            {
                txtingrediente1 = false;
                lblCompletar.Content = "Debe completar el campo";
                lblCompletar.Visibility = Visibility.Visible;
            }
            if (txtBebestible.Text == "" && txtInsumos.Text == "")
            {
                txtbebestible2 = false;
                lblCompletar.Content = "Debe completar el campo";
                lblCompletar.Visibility = Visibility.Visible;
            }
            if (txtCantidad.Text == "" && txtcantBebestible.Text == "")
            {
                txtingrediente2 = false;
                lblCompletar2.Content = "Debe completar el campo";
                lblCompletar2.Visibility = Visibility.Visible;
            }
            if (txtcantBebestible.Text == "" && txtCantidad.Text == "")
            {
                txtbebestible2 = false;
                lblCompletar2.Content = "Debe completar el campo";
                lblCompletar2.Visibility = Visibility.Visible;
            }
            if (rbtIngredientes.IsChecked == false && rbtBebestibles.IsChecked == false)
            {
                lblCompletar3.Content = "Debe seleccionar un insumo";
                lblCompletar3.Visibility = Visibility.Visible;
            }


            if (txtingrediente1 && txtingrediente2 && txtbebestible1 && txtbebestible2)

                if (rbtIngredientes.IsChecked == true)
                {
                    ingredienteBLL ib = new ingredienteBLL();
                    ib.nombre_ins = txtInsumos.Text;
                    ib.stock = Double.Parse(txtCantidad.Text);
                    bool estado = ib.btnAceptar(ib);
                    if (estado)
                    {
                        MessageBox.Show("Ingrediente solicitado correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Ingrediente no ingresado para la solicitud");
                    }
                    txtBebestible.Clear();
                    txtcantBebestible.Clear();
                }

                else if (rbtBebestibles.IsChecked == true)
                {
                    bebestibleBLL ib = new bebestibleBLL();
                    ib.nombre_ins = txtBebestible.Text;
                    ib.stock = Double.Parse(txtcantBebestible.Text);
                    bool estado = ib.btnAceptar(ib);
                    if (estado)
                    {
                        MessageBox.Show("Bebestible solicitado correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Bebestible no ingresado para la solicitud");
                    }
                }
        }

        private void txtInsumos_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar.Visibility = Visibility.Hidden;
           
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar2.Visibility = Visibility.Hidden;
        }

        private void txtBebestible_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar.Visibility = Visibility.Hidden;
        }

        private void txtcantBebestible_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar2.Visibility = Visibility.Hidden;
        }

        private void lblCompletar3_GotFocus(object sender, RoutedEventArgs e)
        {
            lblCompletar3.Visibility = Visibility.Hidden;
        }
    }
}

