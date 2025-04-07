using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Salida
{
    public int IdSalida { get; set; }

    public string? NumeroDocumento { get; set; }

    public int? TipoSalida { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleSalida> DetalleSalida { get; } = new List<DetalleSalida>();

    public virtual TipoSalida? TipoSalidaNavigation { get; set; }
}
