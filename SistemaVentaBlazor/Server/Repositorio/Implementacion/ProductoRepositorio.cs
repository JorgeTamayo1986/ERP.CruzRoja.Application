using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly InventarioContext _dbContext;

        public ProductoRepositorio(InventarioContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Producto>> Consultar(Expression<Func<Producto, bool>> filtro = null)
        {
            IQueryable<Producto> queryEntidad = filtro == null ? _dbContext.Producto.Include(f => f.DetalleProducto): _dbContext.Producto.Include(f => f.DetalleProducto).Where(filtro);


            return queryEntidad;
        }

        public async Task<Producto> Crear(Producto entidad)
        {
            try
            {
                _dbContext.Set<Producto>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Producto entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Producto entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Obtener(Expression<Func<Producto, bool>> filtro = null)
        {
            try
            {
                var producto = await _dbContext.Producto.Include(f => f.DetalleProducto).Where(filtro).FirstOrDefaultAsync();
                return producto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleProducto> ObtenerDetalle(Expression<Func<DetalleProducto, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.DetalleProducto.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
