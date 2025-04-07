using Dapper;
using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioProducto : IRepositorioProducto
    {
        private readonly IDbConnection _conexion;

        public RepositorioProducto(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodos()
        {
            return await _conexion.QueryAsync<Producto>(
                "sp_GetProductos",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Producto?> ObtenerPorId(int id)
        {
            return await _conexion.QueryFirstOrDefaultAsync<Producto>(
                "sp_GetProductoById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task Agregar(Producto producto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Nombre", producto.Nombre);
            parametros.Add("@Precio", producto.Precio);

            await _conexion.ExecuteAsync(
                "sp_InsertProducto",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task Actualizar(Producto producto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", producto.Id);
            parametros.Add("@Nombre", producto.Nombre);
            parametros.Add("@Precio", producto.Precio);

            await _conexion.ExecuteAsync(
                "sp_UpdateProducto",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task Eliminar(int id)
        {
            await _conexion.ExecuteAsync(
                "sp_DeleteProducto",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
