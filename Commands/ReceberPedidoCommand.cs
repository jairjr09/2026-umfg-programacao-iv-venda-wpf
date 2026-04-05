using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using umfg.venda.app.Abstracts;
using umfg.venda.app.UserControls;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.Commands
{
    internal sealed class ReceberPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        private bool ValidarCartao(string numero)
        {
            int soma = 0;
            bool alternar = false;

            for (int i = numero.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(numero[i].ToString());

                if (alternar)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }

                soma += n;
                alternar = !alternar;
            }

            return soma % 10 == 0;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is ListarProdutosViewModel vmProdutos)
            {
                if (vmProdutos.Pedido == null || !vmProdutos.Pedido.Produtos.Any())
                {
                    MessageBox.Show("Adicione itens ao pedido antes de continuar.");
                    return;
                }

                ucReceberPedido.Exibir(vmProdutos.MainWindow, vmProdutos.Pedido);
                return;
            }

            if (parameter is ReceberPedidoViewModel vm)
            {
                var erros = new List<string>();
                var hoje = DateTime.Now;

                if (string.IsNullOrWhiteSpace(vm.NomeCartao))
                    erros.Add("Nome é obrigatório.");

                if ((vm.NumeroCartao <= 0 || vm.NumeroCartao.ToString().Length < 13) || (!ValidarCartao(vm.NumeroCartao.ToString())))
                    erros.Add("Número do cartão inválido.");

                if (vm.CVV <= 0 || vm.CVV.ToString().Length != 3)
                    erros.Add("CVV deve ter 3 dígitos.");

                if (vm.DataValidade.Year < hoje.Year ||
                   (vm.DataValidade.Year == hoje.Year && vm.DataValidade.Month < hoje.Month))
                {
                    erros.Add("Data de validade inválida.");
                }

                if (erros.Any())
                {
                    MessageBox.Show(string.Join("\n", erros));
                    return;
                }

                MessageBox.Show("Pagamento realizado com sucesso!");

                vm.Pedido.Produtos.Clear();
                vm.Pedido.Total = 0;

                var telaInicial = new ListarProdutosViewModel(vm.MainWindow, new UserControl());
                vm.MainWindow.Update(telaInicial);
            }
        }
    }
}
