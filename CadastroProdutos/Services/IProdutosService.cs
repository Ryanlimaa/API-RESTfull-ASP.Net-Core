using System;

namespace CadastroProdutos.Services;

public interface IProdutosService
{
    public List<Produto> ObterTodos();

    public Produto ObterPorId(int id);

    public void Adicionar(Produto novoProduto);

    public Produto Atualizar(int id, Produto prodAtualizado);

    public bool Remover(int id);
}