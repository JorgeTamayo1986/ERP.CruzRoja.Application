using SistemaVentaBlazor.Server.Models;

namespace SistemaVentaBlazor.Server.Repositorio.Contrato
{
    public interface ITipoSalidaRepoitorio
    {
        Task<List<TipoSalida>> Lista();
    }
}
