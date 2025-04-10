using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using System.Globalization;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class VentaRepositorio : IVentaRepositorio
    {

        private readonly InventarioContext _dbcontext;
        public VentaRepositorio(InventarioContext context)
        {
            _dbcontext = context;
        }

        public async Task<Salida> Registrar(Salida entidad)
        {
            Salida VentaGenerada = new Salida();

            //usaremos transacion, ya que si ocurre un error en algun insert a una tabla, debe reestablecer todo a cero, como si no hubo o no existió ningun insert
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                int CantidadDigitos = 4;
                try
                {
                    foreach (DetalleSalida dv in entidad.DetalleSalida)
                    {
                        DetalleProducto producto_encontrado = _dbcontext.DetalleProducto.Where(p => p.Id == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.DetalleProducto.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();


                    NumeroDocumento correlativo = _dbcontext.NumeroDocumento.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumento.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    entidad.NumeroDocumento = numeroVenta;

                    await _dbcontext.Salida.AddAsync(entidad);
                    await _dbcontext.SaveChangesAsync();

                    VentaGenerada = entidad;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return VentaGenerada;
        }

        public async Task<List<Salida>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Salida> query = _dbcontext.Salida;

            if (buscarPor == "fecha")
            {

                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                )
                .Include(dv => dv.DetalleSalida).ThenInclude(p => p.TipoSalidaNavigation)
                .Include(dv => dv.DetalleSalida).ThenInclude(p => p.IdProductoNavigation)
                .ToList();

            }
            else
            {
                return query.Where(v => v.NumeroDocumento == numeroVenta)
                  .Include(dv => dv.DetalleSalida).ThenInclude(p => p.TipoSalidaNavigation)
                  .Include(dv => dv.DetalleSalida).ThenInclude(p => p.IdProductoNavigation)
                  .ToList();
            }


        }

        public async Task<List<DetalleSalida>> Reporte(string FechaInicio, string FechaFin)
        {

            DateTime fech_Inicio = DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
            DateTime fech_Fin = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

            List<DetalleSalida> listaResumen = await _dbcontext.DetalleSalida
                .Include(p => p.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .Include(f => f.TipoSalidaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
                .ToListAsync();

            return listaResumen;
        }
    }
}
