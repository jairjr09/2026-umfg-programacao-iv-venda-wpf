using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.UserControls
{
    /// <summary>
    /// Interação lógica para ucReceberPedido.xam
    /// </summary>
    public partial class ucReceberPedido : UserControl
    {

        private ucReceberPedido(IObserver observer, PedidoModel pedido)
        {
            InitializeComponent();

            var vm = new ReceberPedidoViewModel(this, observer, pedido);

            vm.DataValidade = DateTime.Now;

            DataContext = vm;
        }

        internal static void Exibir(IObserver observer, PedidoModel pedido)
        {
            (new ucReceberPedido(observer, pedido).DataContext as ReceberPedidoViewModel).Notify();   
        }

        private void SomenteNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void SomenteLetras(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[\p{L} ]+$");
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpHidden.SelectedDate.HasValue)
            {
                var data = dpHidden.SelectedDate.Value;

                dpHidden.SelectedDate = new DateTime(data.Year, data.Month, 1);
            }

            dpHidden.Visibility = Visibility.Collapsed;
        }

        private void BloquearDigitacao(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void AbrirCalendario(object sender, MouseButtonEventArgs e)
        {
            dpHidden.Visibility = Visibility.Visible;
            dpHidden.IsDropDownOpen = true;
        }
    }
}
