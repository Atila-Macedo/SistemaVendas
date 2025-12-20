using SistemaVendas.Entidades;
using SistemaVendas.Exceptions;
using SistemaVendas.Repositorios.Interfaces;
using SistemaVendas.Servicos.Interfaces;

namespace SistemaVendas.Servicos;

public class ProdutoServico : IProdutoServico
{
    private readonly IProdutoRepositorio _repositorio;
    public ProdutoServico(IProdutoRepositorio repositorio) => _repositorio = repositorio;

    public void CriarProduto(string nome, decimal preco, int estoque)
    {
        if (preco <= 0) throw new Exception("Preço inválido.");
        _repositorio.Criar(new Produto { Nome = nome, Preco = preco, Estoque = estoque });
    }

    public List<string> ListarProdutos() => _repositorio.Listar()
        .Select(p => $"ID: {p.Id} | {p.Nome} | Preço: {p.Preco:C2} | Estoque: {p.Estoque}").ToList();

    public Produto? BuscarPorId(int id) => _repositorio.BuscarPorId(id);

    public void AtualizarEstoque(int produtoId, int quantidade)
    {
        var produto = _repositorio.BuscarPorId(produtoId);
        if (produto == null || produto.Estoque < quantidade) throw new System.Exception("Estoque insuficiente.");
        produto.Estoque -= quantidade;
        _repositorio.Atualizar(produto);
    }
    public void DeletarProduto(int id)
{
    var produto = _repositorio.BuscarPorId(id);
    if (produto == null) 
        throw new SistemaVendas.Exceptions.NegocioException("Produto não encontrado.");

    _repositorio.Deletar(produto); // Chama o repositório para marcar como deletado
}
}