using SistemaVendas.Entidades;

namespace SistemaVendas.Repositorios.Interfaces;

public interface IPedidoRepositorio
{
    // Salva o pedido e os itens vinculados a ele
    void Criar(Pedido pedido);
    
    // Lista todos os pedidos (geralmente para o histórico)
    List<Pedido> Listar();
    
    // Busca um pedido específico com seus itens incluídos
    Pedido? BuscarPorId(int id);
}