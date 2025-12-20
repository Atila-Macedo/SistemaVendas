using SistemaVendas.Entidades;

namespace SistemaVendas.Servicos.Interfaces;

public interface IPedidoServico
{
    // Note que o tipo da lista deve bater com o que seu professor pediu
    void CriarPedido(string nomeCliente, string enderecoEntrega, string tipoPagamento, List<(int produtoId, int quantidade)> itens);
    List<string> ListarPedidos();
    void MarcarComoEntregue(int pedidoId);
    // ADICIONE ESTA LINHA:
    void CancelarPedido(int id);
}