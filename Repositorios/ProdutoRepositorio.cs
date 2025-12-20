using SistemaVendas.BancoConfig;
using SistemaVendas.Entidades;
using SistemaVendas.Repositorios.Interfaces;

namespace SistemaVendas.Repositorios;

public class ProdutoRepositorio : IProdutoRepositorio
{
    private readonly SistemaVendasContext _context;
    public ProdutoRepositorio(SistemaVendasContext context) => _context = context;

    public void Criar(Produto produto) { _context.Produtos.Add(produto); _context.SaveChanges(); }
    public List<Produto> Listar() => _context.Produtos.Where(p => !p.Deletado).ToList();
    public Produto? BuscarPorId(int id) => _context.Produtos.FirstOrDefault(p => p.Id == id && !p.Deletado);
    public void Atualizar(Produto produto) { _context.Produtos.Update(produto); _context.SaveChanges(); }
    public void Deletar(Produto produto)
    {
        produto.Deletado = true; // Mantemos o rastro no banco, mas escondemos do sistema
        _context.Produtos.Update(produto);
        _context.SaveChanges();
    }
}