using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentaBlazor.Shared
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public string? NumeroDocumento { get; set; }
        public int TipoSalida { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public virtual List<DetalleVentaDTO>? DetalleVenta { get; set; }
    }
}
