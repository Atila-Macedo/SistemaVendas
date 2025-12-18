using SistemaVendas.Entidades;
using SistemaVendas.Repositorios.Interfaces;
using SistemaVendas.Servicos.Interfaces;

namespace SistemaVendas.Servicos;

public class PedidoServico : IPedidoServico
{
    private readonly IPedidoRepositorio _pedidoRepositorio;
    private readonly IProdutoRepositorio _produtoRepositorio;

    // O "Gerente de Pedidos" contrata o "Almoxarife de Pedidos" E o de "Produtos"
    public PedidoServico(IPedidoRepositorio pedidoRepo, IProdutoRepositorio produtoRepo)
    {
        _pedidoRepositorio = pedidoRepo;
        _produtoRepositorio = produtoRepo;
    }

    public void CriarPedido(string nomeCliente, string endereco, string pagamento, List<ItemPedido> itens)
    {
        // REGRA 1: Validações básicas
        if (string.IsNullOrWhiteSpace(nomeCliente)) throw new Exception("Cliente é obrigatório.");
        if (itens == null || !itens.Any()) throw new Exception("O pedido deve ter pelo menos um item.");

        decimal totalPedido = 0;

        // REGRA 2: Validar estoque e calcular preços para cada item
        foreach (var item in itens)
        {
            var produto = _produtoRepositorio.ObterPorId(item.ProdutoId);
            
            if (produto == null) throw new Exception($"Produto ID {item.ProdutoId} não encontrado.");

            // Verificação de estoque (Regra essencial de negócio)
            if (produto.Estoque < item.Quantidade)
                throw new Exception($"Estoque insuficiente para {produto.Nome}. Disponível: {produto.Estoque}");

            // Atualiza o preço do item com o preço atual do produto no banco
            item.PrecoUnitario = produto.Preco;
            totalPedido += item.PrecoUnitario * item.Quantidade;

            // REGRA 3: Baixa no estoque (Abatemos o que foi vendido)
            produto.Estoque -= item.Quantidade;
            _produtoRepositorio.Atualizar(produto); 
        }

        // Criamos o objeto Pedido final
        var novoPedido = new Pedido
        {
            NomeCliente = nomeCliente,
            EnderecoEntrega = endereco,
            TipoPagamento = pagamento,
            SubTotal = totalPedido,
            Entregue = false,
            Itens = itens
        };

        // PASSO FINAL: Mandamos o repositório salvar no banco
        _pedidoRepositorio.Criar(novoPedido);
    }

    public List<Pedido> ListarPedidos()
    {
        return _pedidoRepositorio.Listar();
    }
}