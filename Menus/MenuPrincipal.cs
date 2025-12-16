using SistemaVendas.Menus;

namespace SistemaVendas.Menus;
public static class MenuPrincipal
{
    public static void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Principal ===");
            Console.WriteLine("1. Gerenciar Produtos");
            Console.WriteLine("2. Gerenciar Pedidos");
            Console.WriteLine("0. Sair");
            Console.Write("Selecione uma opção: ");
            opcao = int.Parse(Console.ReadLine() ?? "0");

            switch (opcao)
            {
                case 1:
                    MenuProduto.Exibir();
                    break;
                case 2:
                    MenuPedido.Exibir();
                    break;
                case 0:
                    Console.WriteLine("Saindo do sistema...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (opcao != 0);
    }
}
