using System;
using System.Collections.Generic;

namespace SistemaVentaBlazor.Server.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? IdCategoria { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<DetalleProducto> DetalleProducto { get; } = new List<DetalleProducto>();

    public virtual ICollection<DetalleSalida> DetalleSalida { get; } = new List<DetalleSalida>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
