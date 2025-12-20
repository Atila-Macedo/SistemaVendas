using SistemaVendas.Configuracao;

namespace SistemaVendas.Menus;

public static class MenuPrincipal
{
    public static void Exibir(Container container)
    {
        string opcao;
        do {
            Console.Clear();
            Console.WriteLine("1. Produtos\n2. Pedidos\n0. Sair");
            opcao = Console.ReadLine() ?? "0";

            if (opcao == "1") MenuProduto.Exibir(container);
            if (opcao == "2") MenuPedido.Exibir(container);

        } while (opcao != "0");
    }
}