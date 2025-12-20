using SistemaVendas.Configuracao;

namespace SistemaVendas.Menus;

public static class MenuProduto
{
    public static void Exibir(Container container)
    {
        string opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Menu Produtos ===");
            Console.WriteLine("1. Cadastrar Produto");
            Console.WriteLine("2. Listar Produtos");
            Console.WriteLine("3. Deletar Produto");
            Console.WriteLine("0. Voltar");
            Console.Write("Opção: ");
            opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    Cadastrar(container);
                    break;
                case "2":
                    Listar(container);
                    break;
                case "3": 
                    Deletar(container);
                    break;
            }
        } while (opcao != "0");
    }

    private static void Cadastrar(Container container)
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";
        Console.Write("Preço: ");
        decimal preco = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Estoque: ");
        int estoque = int.Parse(Console.ReadLine() ?? "0");

        try {
            container.ProdutoServico.CriarProduto(nome, preco, estoque);
            Console.WriteLine("Produto salvo!");
        } catch (Exception ex) {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.ReadKey();
    }

    private static void Listar(Container container)
    {
        Console.Clear();
        var produtos = container.ProdutoServico.ListarProdutos();
        // Correção do erro CS1061: Como 'produtos' agora é uma lista de strings, 
        // apenas imprimimos a string diretamente.
        foreach (var p in produtos) {
            Console.WriteLine(p); 
        }
        Console.ReadKey();
    }
    private static void Deletar(Container container)
    {
        Console.Clear();
        Console.WriteLine("--- DELETAR PRODUTO ---");

        // 1️⃣ LISTAR antes de pedir o ID
        var produtos = container.ProdutoServico.ListarProdutos();

        if (produtos.Count == 0)
        {
            Console.WriteLine("Não há produtos cadastrados.");
        }
        else
        {
            Console.WriteLine("\nProdutos no Sistema:");
            Console.WriteLine("-------------------------------------------");
            foreach (var p in produtos)
            {
                Console.WriteLine(p); // Exibe a string formatada
            }
            Console.WriteLine("-------------------------------------------");

            // 2️⃣ PEDIR O ID
            Console.Write("\nDigite o ID do produto que deseja DELETAR: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    // 3️⃣ EXECUTAR AÇÃO
                    container.ProdutoServico.DeletarProduto(id);
                    Console.WriteLine("\n✅ Produto removido com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erro: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido.");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}