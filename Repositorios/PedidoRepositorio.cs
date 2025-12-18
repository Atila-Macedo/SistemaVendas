using Microsoft.EntityFrameworkCore;
using SistemaVendas.BancoConfig;
using SistemaVendas.Entidades;
using SistemaVendas.Repositorios.Interfaces;

namespace SistemaVendas.Repositorios;

public class PedidoRepositorio : IPedidoRepositorio
{
    private readonly SistemaVendasContext _context;

    // Seguindo o padrão do professor: Injeção do Contexto via Construtor
    public PedidoRepositorio(SistemaVendasContext context)
    {
        _context = context;
    }

    public void Criar(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges(); // Salva o Pedido e automaticamente os ItensPedido vinculados
    }

    public List<Pedido> Listar()
    {
        return _context.Pedidos
            .Include(p => p.Itens) // "Puxe também os itens deste pedido"
            .ThenInclude(i => i.Produto) // "E dentro dos itens, puxe os nomes dos produtos"
            .Where(p => !p.Deletado)
            .ToList();
    }

    public Pedido? BuscarPorId(int id)
    {
        return _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefault(p => p.Id == id && !p.Deletado);
    }
}