using SistemaVendas.BancoConfig;
using SistemaVendas.Repositorios;
using SistemaVendas.Servicos;
using SistemaVendas.Entidades;

namespace SistemaVendas.Menus;

public static class MenuPedido
{
    public static void Exibir()
    {
        Console.Clear();
        Console.WriteLine("=== MENU PEDIDO ===");
        Console.WriteLine("1 - Criar Pedido");
        Console.WriteLine("2 - Listar Pedidos");
        Console.WriteLine("0 - Voltar");

        var opcao = Console.ReadLine();

        var context = new SistemaVendasContext();
        var produtoRepo = new ProdutoRepositorio(context);
        var pedidoRepo = new PedidoRepositorio(context);
        var servico = new PedidoServico(pedidoRepo, produtoRepo);

        switch (opcao)
        {
            case "1":
                CriarPedido(servico, context); 
                break;
            case "2":
                ListarPedidos(servico);
                break;
            case "0":
                MenuPrincipal.Exibir();
                return;
        }
        Exibir();
    }

    private static void CriarPedido(PedidoServico servico, SistemaVendasContext context)
    {
        Console.Write("Nome do Cliente: ");
        string nome = Console.ReadLine() ?? "";
        
        Console.WriteLine("Funcionalidade de adicionar itens será detalhada no Dia 3!");
        Console.ReadKey();
    }

    private static void ListarPedidos(PedidoServico servico)
    {
        Console.Clear();
        Console.WriteLine("=== LISTAR PEDIDOS ===");
        Console.ReadKey();
    }
}