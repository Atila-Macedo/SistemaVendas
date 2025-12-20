using Microsoft.EntityFrameworkCore;
using SistemaVendas.Entidades;

namespace SistemaVendas.BancoConfig;

public class SistemaVendasContext : DbContext
{
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=vendas.db");

    public override int SaveChanges()
    {
        var entidades = ChangeTracker.Entries()
            .Where(e => e.Entity is EntidadeBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entidades)
        {
            var entidade = (EntidadeBase)entry.Entity;
            entidade.AtualizadoEm = DateTime.Now;
            if (entry.State == EntityState.Added) entidade.CriadoEm = DateTime.Now;
        }
        return base.SaveChanges();
    }
}