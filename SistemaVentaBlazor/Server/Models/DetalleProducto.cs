using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class DetalleProducto
{
    public int Id { get; set; }

    public int? ProductoId { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public int? Stock { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public virtual Producto? Producto { get; set; }
}
