namespace SistemaVentaBlazor.Shared
{
    public class TipoSalidaDTO
    {
        public int IdTipo { get; set; } 
        public string Descripcion { get; set; } = string.Empty;

        public override bool Equals(object o)
        {
            var other = o as TipoSalidaDTO;
            return other?.IdTipo == IdTipo;
        }
        public override int GetHashCode() => IdTipo.GetHashCode();
        public override string ToString()
        {
            return Descripcion;
        }


    }
}
