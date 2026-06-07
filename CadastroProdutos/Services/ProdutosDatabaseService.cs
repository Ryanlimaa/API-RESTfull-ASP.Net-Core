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
        ValidarProdutos(novoProduto);
        context.Produtos.Add(novoProduto);
        context.SaveChanges();
    }

    public Produto Atualizar(int id, Produto prodAtualizado)
    {
        ValidarProdutos(prodAtualizado);
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

    // Regras de negócio
    private void ValidarProdutos(Produto prod)
    {
        if (prod.Nome == "Produto Padrão")
        {
            throw new Exception("Não é permitido cadastrar um produto com o nome: Produto Padrão!");
        }

        if (prod.Estoque > 1000)
        {
            throw new Exception("O estoque não pode ser maior que 1000 unidades!");
        }
    }
}