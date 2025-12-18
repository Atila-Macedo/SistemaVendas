using SistemaVendas.Entidades;

namespace SistemaVendas.Servicos.Interfaces;

public interface IPedidoServico
{
    // O método principal que recebe os dados do balcão (Menu)
    void CriarPedido(string nomeCliente, string endereco, string pagamento, List<ItemPedido> itens);
    List<Pedido> ListarPedidos();
}   