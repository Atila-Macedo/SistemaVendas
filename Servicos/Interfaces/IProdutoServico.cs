using SistemaVendas.Entidades;

namespace SistemaVendas.Servicos.Interfaces;

public interface IProdutoServico
{
    void CriarProduto(string nome, decimal preco, int estoque);
    List<string> ListarProdutos();
    Produto? BuscarPorId(int id);
    void AtualizarEstoque(int produtoId, int quantidade);
    // ADICIONE ESTA LINHA:
    void DeletarProduto(int id); 
}