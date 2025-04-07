using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServicioProducto
    {
        private readonly IRepositorioProducto _repository;

        public ServicioProducto(IRepositorioProducto repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Producto>> ObtenerTodos() => _repository.ObtenerTodos();
        public Task<Producto?> ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public Task Agregar(Producto producto) => _repository.Agregar(producto);
        public Task Actualizar(Producto producto) => _repository.Actualizar(producto);
        public Task Eliminar(int id) => _repository.Eliminar(id);
    }
}
