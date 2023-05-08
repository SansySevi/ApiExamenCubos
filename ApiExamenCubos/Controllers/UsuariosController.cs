using ApiExamenCubos.Models;
using ApiExamenCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiExamenCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryCubos repo;

        public UsuariosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> PerfilUsuario()
        {
            //DEBEMOS BUSCAR EL CLAIM DEL EMPLEADO
            Claim claim = HttpContext.User.Claims
                .SingleOrDefault(x => x.Type == "USERDATA");
            string jsonUsuario =
                claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>
                (jsonUsuario);
            return usuario;
        }


        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Pedido>>> Pedidos()
        {
            string jsonUser =
                HttpContext.User.Claims.SingleOrDefault(z => z.Type == "USERDATA").Value;
            Usuario user = JsonConvert.DeserializeObject<Usuario>(jsonUser);

            List<Pedido> pedidos = await this.repo.GetPedidos(user.IdUsuario);
            return pedidos;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RealizarPedido(Pedido pedido)
        {
            await this.repo.RegistrarPedidoAsync(pedido);
            return Ok();
        }
    }
}
