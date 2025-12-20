using SistemaVendas.Configuracao;
using SistemaVendas.Entidades;

namespace SistemaVendas.Menus;

public static class MenuPedido
{
    public static void Exibir(Container container)
    {
        string opcao;
        do {
            Console.Clear();
            Console.WriteLine("=== Menu Pedidos ===");
            Console.WriteLine("1. Criar Pedido");
            Console.WriteLine("2. Listar Pedidos");
            Console.WriteLine("3. Marcar como Entregue");
            Console.WriteLine("4. Cancelar Pedido");
            Console.WriteLine("0. Voltar");
            opcao = Console.ReadLine() ?? "0";

            switch (opcao) {
                case "1": Criar(container); break;
                case "2": Listar(container); break;
                case "3": Entregar(container); break;
                case "4": Cancelar(container); break;
            }
        } while (opcao != "0");
    }

    private static void Criar(Container container)
{
    Console.Clear();
    Console.WriteLine("--- NOVO PEDIDO ---");
    
    Console.Write("Nome do Cliente: ");
    string cliente = Console.ReadLine() ?? "Consumidor";
    
    Console.Write("Endereço de Entrega: ");
    string endereco = Console.ReadLine() ?? "Balcão";

    // Lista de tuplas para o carrinho (ID e Quantidade)
    var itens = new List<(int produtoId, int quantidade)>();
    string continuar = "s";

    while (continuar.ToLower() == "s")
    {
        Console.Clear();
        Console.WriteLine("--- PRODUTOS DISPONÍVEIS ---");
        
        // PASSO 1: Listamos os produtos ANTES de pedir o ID
        var produtos = container.ProdutoServico.ListarProdutos();
        if (produtos.Count == 0)
        {
            Console.WriteLine("Aviso: Não há produtos em stock!");
            break;
        }

        foreach (var p in produtos) 
        {
            Console.WriteLine(p); // Imprime a string formatada pelo serviço
        }

        Console.WriteLine("\n-------------------------------------------");
        Console.Write("Digite o ID do Produto que deseja: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) 
        {
            Console.WriteLine("ID inválido!");
            Thread.Sleep(1000);
            continue;
        }

        Console.Write("Quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int qtd))
        {
            Console.WriteLine("Quantidade inválida!");
            Thread.Sleep(1000);
            continue;
        }

        // Adicionamos ao carrinho temporário
        itens.Add((id, qtd));

        Console.Write("\nDeseja adicionar mais um produto? (s/n): ");
        continuar = Console.ReadLine() ?? "n";
    }

    if (itens.Count > 0)
    {
        try
        {
            // O serviço valida o stock, abate a quantidade e calcula o total
            container.PedidoServico.CriarPedido(cliente, endereco, "Dinheiro", itens);
            Console.WriteLine("\n✅ Pedido realizado e stock atualizado!");
        }
        catch (Exception ex)
        {
            // Captura erros de stock insuficiente ou ID inexistente
            Console.WriteLine($"\n❌ Erro: {ex.Message}");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

    private static void Listar(Container container)
    {
        var pedidos = container.PedidoServico.ListarPedidos();
        foreach (var p in pedidos) Console.WriteLine(p);
        Console.ReadKey();
    }

    private static void Entregar(Container container)
    {
   Console.Clear();
    Console.WriteLine("--- MARCAR PEDIDO COMO ENTREGUE ---");

    // 2️⃣ Buscar todos os pedidos chamando o serviço
    var pedidos = container.PedidoServico.ListarPedidos();

    // 3️⃣ Verificar se existem pedidos e exibi-los para o usuário escolher
    if (pedidos.Count == 0)
    {
        Console.WriteLine("\nNão há pedidos registrados para entrega.");
    }
    else
    {
        Console.WriteLine("\nPedidos no Sistema:");
        Console.WriteLine("--------------------------------------------------");
        foreach (var p in pedidos)
        {
            // Exibe a string formatada vinda do serviço
            Console.WriteLine(p); 
        }
        Console.WriteLine("--------------------------------------------------");

        // 4️⃣ Solicitar o ID do pedido
        Console.Write("\nDigite o ID do pedido que deseja entregar: ");
        
        if (int.TryParse(Console.ReadLine(), out int pedidoId))
        {
            try
            {
                // 5️⃣ Enviar o ID para o serviço processar a entrega
                container.PedidoServico.MarcarComoEntregue(pedidoId);
                Console.WriteLine("\n✅ Sucesso: O pedido foi marcado como ENTREGUE.");
            }
            catch (Exception ex)
            {
                // 6️⃣ Exibir mensagem de erro (ex: pedido já entregue ou inexistente)
                Console.WriteLine($"\n❌ Erro: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nID inválido. Operação cancelada.");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
    }
    private static void Cancelar(Container container)
    {
        Console.Clear();
        Console.WriteLine("--- CANCELAR PEDIDO E DEVOLVER ESTOQUE ---");

        // 1️⃣ Lista os pedidos para o usuário ver o que pode cancelar
        var pedidos = container.PedidoServico.ListarPedidos();

        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido ativo encontrado.");
        }
        else
        {
            foreach (var p in pedidos) Console.WriteLine(p);

            Console.Write("\nDigite o ID do pedido que deseja CANCELAR: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    container.PedidoServico.CancelarPedido(id);
                    Console.WriteLine("\n✅ Pedido cancelado e produtos devolvidos ao estoque!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                }
            }
        }
        Console.WriteLine("\nPressione qualquer tecla...");
        Console.ReadKey();
    }
}