using SistemaVendas.BancoConfig;
using SistemaVendas.Repositorios;
using SistemaVendas.Servicos;
using SistemaVendas.Servicos.Interfaces;

namespace SistemaVendas.Configuracao;

public class Container
{
    // As propriedades DEVEM ser públicas para os menus as enxergarem
    public IProdutoServico ProdutoServico { get; }
    public IPedidoServico PedidoServico { get; }

    public Container()
    {
        var context = new SistemaVendasContext();
        
        // Criamos os repositórios passando o contexto
        var pRepo = new ProdutoRepositorio(context);
        var pedRepo = new PedidoRepositorio(context);

        // Criamos os serviços injetando os repositórios
        ProdutoServico = new ProdutoServico(pRepo);
        PedidoServico = new PedidoServico(pedRepo, ProdutoServico);
    }
}