using SistemaVendas.Entidades;

namespace SistemaVendas.Repositorios.Interfaces;

public interface IProdutoRepositorio
{
    void Criar(Produto produto);
    List<Produto> Listar();
    Produto? BuscarPorId(int id);
    void Deletar(Produto produto);
    void Atualizar(Produto produto);
}