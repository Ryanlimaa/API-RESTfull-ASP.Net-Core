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
        throw new NotImplementedException();
    }

    public Produto ObterPorId(int id)
    {
        throw new NotImplementedException();
    }

    public List<Produto> ObterTodos()
    {
        return context.Produtos.ToList();
    }

    public bool Remover(int id)
    {
        throw new NotImplementedException();
    }
}