namespace SistemaVentaBlazor.Client.Servicios.Contrato
{
    public interface ITipoSalidaService
    {
        Task<ResponseDTO<List<TipoSalidaDTO>>> Lista();
    }
}
