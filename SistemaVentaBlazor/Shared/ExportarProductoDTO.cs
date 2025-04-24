namespace SistemaVentaBlazor.Shared
{
    public class ExportarProductoDTO
    {
        public string? Nombre { get; set; }
        public string? DescripcionCategoria { get; set; }
        public int? Stock { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
