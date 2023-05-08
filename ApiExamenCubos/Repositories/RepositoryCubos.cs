using ApiExamenCubos.Data;
using ApiExamenCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamenCubos.Repositories
{
    public class RepositoryCubos
    {

        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> ExisteUsuario(string email, string password)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }


        private async Task<int> GetMaxPedidoAsync()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1;
            }
        }

        public async Task<List<Cubo>> GetProductosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<Cubo> FindProductoAsync(int id)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(x => x.IdCubo == id);
        }

        public async Task<List<Pedido>> GetPedidos(int idUser)
        {
            return await this.context.Pedidos.Where(x => x.IdUsuario == idUser).ToListAsync();
        }

        public async Task RegistrarPedidoAsync(Pedido pedido)
        {
            Pedido newPedido = new Pedido()
            {
                IdPedido = await this.GetMaxPedidoAsync(),
                Fecha = DateTime.Now,
                IdCubo = pedido.IdCubo,
                IdUsuario = pedido.IdUsuario
            };

            this.context.Add(newPedido);
            await this.context.SaveChangesAsync();
        }
    }
}
