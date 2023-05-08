using ApiExamenCubos.Models;
using ApiExamenCubos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExamenCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {

        private RepositoryCubos repo;

        public TiendaController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Marcas()
        {
            List<string> list = await this.repo.GetMarcasAsync();
            return list;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Cubo>>> Productos()
        {
            List<Cubo> list = await this.repo.GetProductosAsync();
            return list;
        }

        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> ProductosMarca(string marca)
        {
            List<Cubo> list = await this.repo.GetProductosMarcaAsync(marca);
            return list;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Cubo>> Producto(int id)
        {
            Cubo producto = await this.repo.FindProductoAsync(id);
            return producto;
        }
    }
}
