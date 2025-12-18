using SistemaVendas.Servicos;
using SistemaVendas.Servicos.Interfaces;
using SistemaVendas.Entidades;

namespace SistemaVendas.Menus;
public static class MenuProduto
{
 // Criamos uma instância do serviço para gerenciar os produtos
    private static readonly IProdutoServico _produtoServico = new ProdutoServico();
    public static void Exibir()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Produtos ===");
            Console.WriteLine("1. Cadastrar Novo Produto");
            Console.WriteLine("2. Listar Todos os Produtos");
            Console.WriteLine("3. Remover Produto (Soft Delete)");
            Console.WriteLine("0. Voltar ao Menu Principal");
            Console.Write("\nSelecione uma opção: ");
            
            if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;
                case 2:
                    Listar();
                    break;
                case 3:
                    Remover();
                    break;
                case 0:
                    // Apenas sai do loop e volta para o MenuPrincipal
                    break;
                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                    break;
            }
        } while (opcao != 0);
    }

    private static void Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("--- Cadastrar Produto ---");
        
        Console.Write("Nome do Produto: ");
        string nome = Console.ReadLine() ?? "";

        Console.Write("Preço: ");
        decimal preco = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Quantidade em Estoque: ");
        int estoque = int.Parse(Console.ReadLine() ?? "0");

        try
        {
            _produtoServico.CriarProduto(nome, preco, estoque);
            Console.WriteLine("\nProduto cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro ao cadastrar: {ex.Message}");
        }
        
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void Listar()
    {
        Console.Clear();
        Console.WriteLine("--- Lista de Produtos ---");
        
        var produtos = _produtoServico.ListarProdutos();

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
        }
        else
        {
            foreach (var p in produtos)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco:C2} | Estoque: {p.Estoque}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void Remover()
    {
        Console.Clear();
        Console.WriteLine("--- Remover Produto ---");
        Console.Write("Digite o ID do produto que deseja remover: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        _produtoServico.DeletarProduto(id);
        
        Console.WriteLine("\nOperação realizada! (Se o ID existia, o produto foi removido).");
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}