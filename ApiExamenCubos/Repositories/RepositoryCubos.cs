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

        private async Task<int> GetMaxUserAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1;
            }
        }

        public async Task RegisterAsync(Usuario user)
        {
            Usuario newUser = new Usuario()
            {
                IdUsuario = await this.GetMaxUserAsync(),
                Password = user.Password,
                Email = user.Email,
                Nombre = user.Nombre,
                ImagenPerfil = user.ImagenPerfil
            };

            this.context.Add(newUser);
            await this.context.SaveChangesAsync();
        }

        private async Task<int> GetMaxProductoAsync()
        {
            if (this.context.Cubos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Cubos.MaxAsync(x => x.IdCubo) + 1;
            }
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

        public async Task<List<string>> GetMarcasAsync()
        {
            var consulta = (from datos in this.context.Cubos
                            select datos.Marca).Distinct();
            return await consulta.ToListAsync();
            
        }

        public async Task<List<Cubo>> GetProductosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> GetProductosMarcaAsync(string marca)
        {
            return await this.context.Cubos.Where(x=> x.Marca == marca).ToListAsync();
        }

        public async Task<Cubo> FindProductoAsync(int id)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(x => x.IdCubo == id);
        }

        public async Task RegistrarProductoAsync(Cubo cubo)
        {
            Cubo newCubo = new Cubo()
            {
                IdCubo = await this.GetMaxProductoAsync(),
                Nombre = cubo.Nombre,
                Marca = cubo.Marca,
                Imagen = cubo.Imagen,
                Precio = cubo.Precio
            };

            this.context.Add(newCubo);
            await this.context.SaveChangesAsync();
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
