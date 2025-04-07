using System.Net.Http.Json;

namespace SistemaVentaBlazor.Client.Servicios.Implementacion
{
    public class TipoSalidaService : ITipoSalidaService
    {
        private readonly HttpClient _httpClient;
        public TipoSalidaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDTO<List<TipoSalidaDTO>>> Lista()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseDTO<List<TipoSalidaDTO>>>("api/tiposalida/Lista");
            return result;
        }
    }
}
