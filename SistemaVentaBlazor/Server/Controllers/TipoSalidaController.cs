using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentaBlazor.Server.Repositorio.Contrato;
using SistemaVentaBlazor.Server.Repositorio.Implementacion;
using SistemaVentaBlazor.Shared;

namespace SistemaVentaBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoSalidaController : ControllerBase
    {
        private readonly IMapper _mapper;
        readonly ITipoSalidaRepoitorio _tipoSalidaRepositorio;
        public TipoSalidaController(ITipoSalidaRepoitorio tipoSalidaRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _tipoSalidaRepositorio = tipoSalidaRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<TipoSalidaDTO>> _response = new ResponseDTO<List<TipoSalidaDTO>>();

            try
            {
                List<TipoSalidaDTO> listaDTO = new List<TipoSalidaDTO>();
                listaDTO = _mapper.Map<List<TipoSalidaDTO>>(await _tipoSalidaRepositorio.Lista());

                if (listaDTO.Count > 0)
                    _response = new ResponseDTO<List<TipoSalidaDTO>>() { status = true, msg = "ok", value = listaDTO };
                else
                    _response = new ResponseDTO<List<TipoSalidaDTO>>() { status = false, msg = "sin resultados", value = null };


                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new ResponseDTO<List<TipoSalidaDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
