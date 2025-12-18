using SistemaVendas.Menus;

namespace SistemaVendas.Menus;
public static class MenuPedido
{
    public static void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Pedidos ===");
            Console.WriteLine("1. Criar pedido");
            Console.WriteLine("2. Listar Pedidos");
            Console.WriteLine("3. Remover Pedido");
            Console.WriteLine("0. Voltar ao Menu Principal");
            Console.Write("Selecione uma opção: ");
            opcao = int.Parse(Console.ReadLine() ?? "0");
           
            switch (opcao)
            {
                case 1:
                    Exibir();
                    break;
                case 2:
                    Exibir();
                    break;
                case 3:
                    Exibir();
                    break;
                case 0:
                    MenuPrincipal.Exibir();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (opcao != 0);
    }
}