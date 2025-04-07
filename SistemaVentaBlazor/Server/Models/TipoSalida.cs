using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class TipoSalida
{
    public int IdTipo { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<Salida> Salida { get; } = new List<Salida>();
}
