using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutos.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private List<Produto> produtos = new List<Produto>()
        {
            new Produto() {Id = 1, Nome = "Mouse sem Fio", Preco = 99.90, Estoque = 50},
            new Produto() {Id = 2, Nome = "Telcado", Preco = 249.90, Estoque = 30}
        };

        [HttpGet]
        public ActionResult<List<Produto>> Get()
        {
            return Ok(produtos);
        }
    }
}