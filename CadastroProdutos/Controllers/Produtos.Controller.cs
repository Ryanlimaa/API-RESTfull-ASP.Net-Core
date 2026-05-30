using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private static List<Produto> produtos = new List<Produto>()
        {
            new Produto() {Id = 1, Nome = "Mouse sem Fio", Preco = 99.90, Estoque = 50},
            new Produto() {Id = 2, Nome = "Telcado", Preco = 249.90, Estoque = 30}
        };

        // Método para listar os produtos
        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(produtos);
        }

        // Método para buscar os produtos por id
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = produtos.FirstOrDefault(x => x.Id == id);

            if (produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }

            return Ok(produto);
        }

        // Método para inserir um novo produto
        [HttpPost]
        public ActionResult Post(Produto novoProduto)
        {
            produtos.Add(novoProduto);

            return Ok("Produto criado com sucesso!");
        }

        // Método para atualizar um produto
        [HttpPut("{id}")]
        public ActionResult<Produto> Put(int id, Produto prodAtualizado)
        {
            var produto = produtos.FirstOrDefault(x => x.Id == id);

            if (produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }

            produto.Nome = prodAtualizado.Nome;
            produto.Preco = prodAtualizado.Preco;
            produto.Estoque = prodAtualizado.Estoque;

            return Ok(produto);
        }
        
        // Método para excluir um produto
        [HttpDelete("/{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = produtos.FirstOrDefault(x => x.Id == id);

            if (produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }
            
            produtos.Remove(produto);

            return Ok("Produto excluido com sucesso!");
        }
    }
}