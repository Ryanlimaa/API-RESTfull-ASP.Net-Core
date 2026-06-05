using System;
using System.ComponentModel;
using CadastroProdutos.Database;

namespace CadastroProdutos.Services;

public class ProdutosDatabaseService : IProdutosService
{

    private ApplicationDbContext context;

    public ProdutosDatabaseService(ApplicationDbContext banco)
    {
        context = banco;
    }
    public void Adicionar(Produto novoProduto)
    {
        context.Produtos.Add(novoProduto);
        context.SaveChanges();
    }

    public Produto Atualizar(int id, Produto prodAtualizado)
    {
        var produto = context.Produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return null;
        }

        produto.Nome = prodAtualizado.Nome;
        produto.Preco = prodAtualizado.Preco;
        produto.Estoque = prodAtualizado.Estoque;

        context.SaveChanges();

        return produto;
    }

    public Produto ObterPorId(int id)
    {
        return context.Produtos.FirstOrDefault(x => x.Id == id);
    }

    public List<Produto> ObterTodos()
    {
        return context.Produtos.ToList();
    }

    public bool Remover(int id)
    {
        var produto = context.Produtos.FirstOrDefault(x => x.Id == id);

        if (produto is null)
        {
            return false;
        }

        context.Produtos.Remove(produto);

        context.SaveChanges();

        return true;
    }
}