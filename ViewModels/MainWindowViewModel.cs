using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using umfg.venda.app.Abstracts;
using umfg.venda.app.Commands;
using umfg.venda.app.Interfaces;

namespace umfg.venda.app.ViewModels
{
    internal sealed class MainWindowViewModel : AbstractViewModel, IObserver
    {
        private UserControl userControl = new UserControl();

        public UserControl UserControl
        {
            get => userControl;
            set => SetField(ref userControl, value);
        }

        public ListarProdutosCommand ListarProdutos { get; private set; } = new();

        public MainWindowViewModel() : base("UMFG - Tela Principal")
        {
        }

        public void Update(ISubject subject)
        {
            if (subject is ListarProdutosViewModel)
                UserControl = (subject as ListarProdutosViewModel).UserControl;

            if (subject is ReceberPedidoViewModel)
                UserControl = (subject as ReceberPedidoViewModel).UserControl;
        }
    }
}
