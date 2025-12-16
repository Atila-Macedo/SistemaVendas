using SistemaVendas.BancoConfig;
using SistemaVendas.Entidades;
using SistemaVendas.Repositorios.Interfaces;

namespace SistemaVendas.Repositorios;

public class ProdutoRepositorio : IProdutoRepositorio
{
    private readonly SistemaVendasContext _context;

    public ProdutoRepositorio()
    {
        _context = new SistemaVendasContext();
    }

    public void Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
    }

    public List<Produto> ListarTodos()
    {
        // Retornamos apenas os que não foram marcados como "Deletado" (Soft Delete)
        return _context.Produtos.Where(p => !p.Deletado).ToList();
    }

    public Produto? ObterPorId(int id)
    {
        return _context.Produtos.FirstOrDefault(p => p.Id == id && !p.Deletado);
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);
        _context.SaveChanges();
    }

    public void Remover(int id)
    {
        var produto = ObterPorId(id);
        if (produto != null)
        {
            // Em vez de apagar do banco, marcamos como deletado
            produto.Deletado = true;
            _context.SaveChanges();
        }
    }
}