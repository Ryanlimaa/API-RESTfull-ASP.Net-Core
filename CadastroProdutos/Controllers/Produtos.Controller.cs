using CadastroProdutos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private ProdutosServices prodServ = new ProdutosServices(); 
        // Método para listar os produtos
        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(prodServ.ObterTodos());
        }

        // Método para buscar os produtos por id
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = prodServ.ObterPorId(id);

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
            prodServ.Adicionar(novoProduto);

            return Ok("Produto criado com sucesso!");
        }

        // Método para atualizar um produto
        [HttpPut("{id}")]
        public ActionResult<Produto> Put(int id, Produto prodAtualizado)
        {
            var produto = prodServ.Atualizar(id, prodAtualizado);

            if (produto is null)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }

            return Ok(produto);
        }
        
        // Método para excluir um produto
        [HttpDelete("/{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var deletou = prodServ.Remover(id);

            if (deletou == false)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }

            return Ok("Produto excluido com sucesso!");
        }
    }
}