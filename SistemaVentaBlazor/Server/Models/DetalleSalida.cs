using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class DetalleSalida
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? TipoSalida { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Salida? IdVentaNavigation { get; set; }

    public virtual TipoSalida? TipoSalidaNavigation { get; set; }
}
