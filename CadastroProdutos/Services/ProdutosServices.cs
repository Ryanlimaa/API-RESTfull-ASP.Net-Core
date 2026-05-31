using System;

namespace CadastroProdutos.Services;

public class ProdutosServices
{
    private static List<Produto> produtos = new List<Produto>()
    {
        new Produto() {Id = 1, Nome = "Mouse sem Fio", Preco = 99.90, Estoque = 50},
        new Produto() {Id = 2, Nome = "Telcado", Preco = 249.90, Estoque = 30}
    };

    public List<Produto> ObterTodos()
    {
        return produtos;
    }

    public Produto ObterPorId(int id)
    {
        return produtos.FirstOrDefault(x => x.Id == id);
    }

    public void Adicionar(Produto novoProduto)
    {
        produtos.Add(novoProduto);
    }

    public Produto Atualizar(int id, Produto prodAtualizado)
    {
        var produto = produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return null;
        }

        produto.Nome = prodAtualizado.Nome;
        produto.Preco = prodAtualizado.Preco;
        produto.Estoque = prodAtualizado.Estoque;

        return produto;
    }

    public bool Remover(int id)
    {
        var produto = produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return false;
        }

        produtos.Remove(produto);

        return true;
    }
}