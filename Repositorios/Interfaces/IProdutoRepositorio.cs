using SistemaVendas.Entidades;

namespace SistemaVendas.Repositorios.Interfaces;

public interface IProdutoRepositorio
{
    void Adicionar(Produto produto);
    List<Produto> ListarTodos();
    Produto? ObterPorId(int id);
    void Atualizar(Produto produto);
    void Remover(int id);
    List<Produto> Listar();
}