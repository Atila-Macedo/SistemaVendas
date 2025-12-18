using SistemaVendas.Entidades;
using SistemaVendas.Repositorios;
using SistemaVendas.Repositorios.Interfaces;
using SistemaVendas.Servicos.Interfaces;

namespace SistemaVendas.Servicos;

public class ProdutoServico : IProdutoServico
{
    private readonly IProdutoRepositorio _produtoRepositorio;

    public ProdutoServico()
    {
        // O serviço usa o repositório para salvar os dados
        _produtoRepositorio = new ProdutoRepositorio();
    }

    public void CriarProduto(string nome, decimal preco, int estoque)
    {
        // Regra de negócio simples: não aceitar valores negativos
        if (preco <= 0) throw new Exception("O preço deve ser maior que zero.");
        if (estoque < 0) throw new Exception("O estoque não pode ser negativo.");

        var novoProduto = new Produto
        {
            Nome = nome,
            Preco = preco,
            Estoque = estoque
        };

        _produtoRepositorio.Adicionar(novoProduto);
    }

    public List<Produto> ListarProdutos()
    {
        return _produtoRepositorio.ListarTodos();
    }

    public void DeletarProduto(int id)
    {
        _produtoRepositorio.Remover(id);
    }
}