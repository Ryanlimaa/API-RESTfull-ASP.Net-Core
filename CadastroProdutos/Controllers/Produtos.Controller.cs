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

        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(produtos);
        }

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

        [HttpPost]
        public ActionResult Post(Produto novoProduto)
        {
            produtos.Add(novoProduto);

            return Ok("Produto criado com sucesso!");
        }
    }
}