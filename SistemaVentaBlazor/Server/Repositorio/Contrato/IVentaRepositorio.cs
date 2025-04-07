using SistemaVentaBlazor.Server.Models;

namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface IVentaRepositorio
    {
        Task<Salida> Registrar(Salida entidad);
        Task<List<Salida>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<DetalleSalida>> Reporte(string FechaInicio, string FechaFin);
    }
}
