using Microsoft.EntityFrameworkCore;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Server.Repositorio.Contrato;

namespace SistemaVentaBlazor.Server.Repositorio.Implementacion
{
    public class TipoSalidaRepositorio : ITipoSalidaRepoitorio
    {
        private readonly InventarioContext _context;
        public TipoSalidaRepositorio(InventarioContext context)
        {
            _context = context;
        }
        public async Task<List<TipoSalida>> Lista()
        {
            try
            {
                return await _context.TipoSalida.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
