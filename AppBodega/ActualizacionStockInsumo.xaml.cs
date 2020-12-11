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
using Tulpep.NotificationWindow;

namespace AppBodega
{
    /// <summary>
    /// Lógica de interacción para ActualizacionStockInsumo.xaml
    /// </summary>
    public partial class ActualizacionStockInsumo : MetroWindow
    {
        public ActualizacionStockInsumo()
        {
            InitializeComponent();
        }

        public void CargaDtgInsumos() {
            if (rbIngrediente.IsChecked == true)
            {
                ingredienteBLL ingBLL = new ingredienteBLL();
                DataTable ingredientesDT = ingBLL.GetIngHab();
                dtgInsumos.ItemsSource = ingredientesDT.DefaultView;
            }
            else if  (rbbebestible.IsChecked == true)
            {
                bebestibleBLL bebBLL = new bebestibleBLL();
                DataTable bebestiblesDT = bebBLL.GetBebHabilitado();
                dtgInsumos.ItemsSource = bebestiblesDT.DefaultView;
            }
        }

        private void RbIngrediente_Checked(object sender, RoutedEventArgs e)
        {
            ingredienteBLL ingBLL = new ingredienteBLL();
            DataTable ingredientesDT = ingBLL.GetIngHab();
            if (ingredientesDT.Rows.Count > 0)
            {
                dtgInsumos.ItemsSource = ingredientesDT.DefaultView;
                
            }
            txtID.Text = "";
            txtStockActual.Text = "";
            txtStockAgregar.Text = "";

        }

        private void Rbbebestible_Checked(object sender, RoutedEventArgs e)
        {
            bebestibleBLL bebBLL = new bebestibleBLL();
            DataTable bebestiblesDT = bebBLL.GetBebHabilitado();
            dtgInsumos.ItemsSource = bebestiblesDT.DefaultView;

            txtID.Text = "";
            txtStockActual.Text = "";
            txtStockAgregar.Text = "";
        }

        private void TxtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtID.Text.Length > 0)
            {
                if (rbIngrediente.IsChecked == true)
                {
                    ingredienteBLL ingBLL = new ingredienteBLL();
                    double stock = ingBLL.GetStock(int.Parse(txtID.Text));
                    txtStockActual.Text = "" + stock;
                }
                else if (rbbebestible.IsChecked == true)
                {
                    bebestibleBLL bebBLL = new bebestibleBLL();
                    double stock = bebBLL.GetStock(int.Parse(txtID.Text));
                    txtStockActual.Text = "" + stock;

                }
            }
        }

        private void DtgInsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (dtgInsumos.SelectedCells.Count > 0)
            {
                DataRowView row = dtgInsumos.SelectedItem as DataRowView;
                if (row != null)
                {
                    int id = int.Parse(row.Row.ItemArray[0].ToString());
                    txtID.Text = "" + id;
                }
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(rbIngrediente.IsChecked == true && txtID.Text.Length>0)
            {
                if (rbCambiaStock.IsChecked ==true)
                {
                    if (txtStockAgregar.Text.Length > 0)
                    {
                        ingredienteBLL ingBLL = new ingredienteBLL();
                        int id = int.Parse(txtID.Text);
                        double cantidad = double.Parse(txtStockAgregar.Text);
                        if (ingBLL.UpdateOnlyStockingrediente(id, cantidad))
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.add;
                            popup.ContentText = "Stock Actualizado Correctamente";
                            popup.AnimationDuration = 500;
                            popup.Delay = 1500;
                            popup.Popup();
                            txtStockAgregar.Text = "";
                            CargaDtgInsumos();
                            txtStockAgregar.Text = "";
                            CargaDtgInsumos();
                        }
                        else
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.delete;
                            popup.ContentText = "No se pudo Actualizar correctamente el Stock";
                            popup.AnimationDuration = 500;
                            popup.Delay = 2500;
                            popup.Popup();
                        }
                    }
                    else
                    {
                        
                        lbstock.Content = ("Ingrese un Valor de Stock");
                    }
                }
                else if (rbAgregaStock.IsChecked == true)
                {
                    if (txtStockAgregar.Text.Length > 0)
                    {

                        ingredienteBLL ingBLL = new ingredienteBLL();
                        int id = int.Parse(txtID.Text);
                        double cantidad = double.Parse(txtStockAgregar.Text);
                        if (ingBLL.UpdateStockAddIngrediente(id, cantidad))
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.add;
                            popup.ContentText = "Stock Actualizado Correctamente";
                            popup.AnimationDuration = 500;
                            popup.Delay = 1500;
                            popup.Popup();
                            txtStockAgregar.Text = "";
                            CargaDtgInsumos();
                        }
                        else
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.delete;
                            popup.ContentText = "No se pudo Actualizar correctamente el Stock";
                            popup.AnimationDuration = 500;
                            popup.Delay = 2500;
                            popup.Popup();
                        }
                    }
                    else
                    {
                        lbstock.Content = ("Ingrese un Valor de Stock");
                    }

                }
                else
                {
                    lbaccion.Content="Seleccione una de las opciones de cambio de stock";
                }
            }
            else if (rbbebestible.IsChecked == true && txtID.Text.Length > 0) 
            {
                if (rbCambiaStock.IsChecked == true)
                {
                    if (txtStockAgregar.Text.Length > 0)
                    {
                        bebestibleBLL bebBLL = new bebestibleBLL();
                        int id = int.Parse(txtID.Text);
                        int cantidad = int.Parse(txtStockAgregar.Text);
                        if (bebBLL.UpdateOnlyStockBeb(id, cantidad))
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.add;
                            popup.ContentText = "Stock Actualizado Correctamente";
                            popup.AnimationDuration = 500;
                            popup.Delay = 1500;
                            popup.Popup();
                            txtStockAgregar.Text = "";
                            CargaDtgInsumos();
                        }
                        else
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.delete;
                            popup.ContentText = "No se pudo Actualizar correctamente el Stock";
                            popup.AnimationDuration = 500;
                            popup.Delay = 2500;
                            popup.Popup();
                        }
                    }
                    else
                    {
                        lbstock.Content=("Ingrese un Valor de Stock");
                    }

                }
                else if (rbAgregaStock.IsChecked == true)
                {
                    if (txtStockAgregar.Text.Length > 0)
                    {
                        bebestibleBLL bebBLL = new bebestibleBLL();
                        int id = int.Parse(txtID.Text);
                        int cantidad = int.Parse(txtStockAgregar.Text);
                        if (bebBLL.UpdateStockAddBeb(id, cantidad))
                        {
                            MessageBox.Show("Stock Actualizado correctamente");
                            txtStockAgregar.Text = "";
                            CargaDtgInsumos();
                        }
                        else
                        {

                            PopupNotifier popup = new PopupNotifier();
                            popup.TitleText = "Aviso";
                            popup.Image = Properties.Resources.delete;
                            popup.ContentText = "No se pudo Actualizar correctamente el Stock";
                            popup.AnimationDuration = 500;
                            popup.Delay = 2500;
                            popup.Popup();
                        }
                    }
                    else
                    {
                        lbstock.Content = ("Ingrese un Valor de Stock");
                    }
                }
                else
                {
                   lbaccion.Content="Seleccione una de las opciones de cambio de stock";
                }
            }
            else
            {
                lbtipo.Content="Seleccione una opcion de ingrediente o bebestible y seleccione un item de la lista";
            }
        }

        private void RbCambiaStock_Checked(object sender, RoutedEventArgs e)
        {
            lbIngresoStock.Content = "Stock Nuevo";
        }

        private void RbAgregaStock_Checked(object sender, RoutedEventArgs e)
        {
            lbIngresoStock.Content = "Stock a Agregar";
        }

        private void TxtStockAgregar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {


            int ascii = Convert.ToInt32(Convert.ToChar(e.Text));
            if (rbbebestible.IsChecked == true)
            {
                if (ascii >= 48 && ascii <= 57)
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
                if ((ascii >= 48 && ascii <= 57) || ascii == 46)
                {

                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
                if (txtStockAgregar.Text.Contains(".") && ascii == 46)
                { e.Handled = true; }
            }

        }
    }
}
