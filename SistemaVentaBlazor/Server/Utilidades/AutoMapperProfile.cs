using AutoMapper;
using SistemaVentaBlazor.Server.Models;
using SistemaVentaBlazor.Shared;
using System.Globalization;

namespace SistemaVentaBlazor.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.rolDescripcion,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );

            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
            );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region TipoSalida
            CreateMap<TipoSalida, TipoSalidaDTO>().ReverseMap();
            #endregion TipoSalida

            #region Producto
            CreateMap<Producto, ProductoDTO>()
             .ForMember(destino =>
                destino.IdProducto,
                opt => opt.MapFrom(origen => origen.Id)
            )
            .ForMember(destino =>
                destino.IdCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoria)
            )
            .ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion)
            )
             .ForMember(destino =>
                destino.Stock,
                opt => opt.MapFrom(origen => origen.DetalleProducto.FirstOrDefault().Stock)
            )
             .ForMember(destino =>
                destino.FechaVencimiento,
                opt => opt.MapFrom(origen => origen.DetalleProducto.FirstOrDefault().FechaVencimiento)
            );
 

            CreateMap<ProductoDTO, Producto>()
            .ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
            );

            CreateMap<DetalleProducto, ProductoDTO>()
           .ForMember(destino =>
               destino.FechaVencimiento,
               opt => opt.MapFrom(origen => origen.FechaVencimiento)
           )
           .ForMember(destino =>
               destino.Stock,
               opt => opt.MapFrom(origen => origen.Stock)
           ); 

            #endregion Producto

            #region Salida
            CreateMap<Salida, VentaDTO>();

            CreateMap<VentaDTO, Salida>();


            #endregion Salida

            #region DetalleSalida

            CreateMap<DetalleSalida, DetalleVentaDTO>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.DescripcionSalida,
                    opt => opt.MapFrom(origen => origen.TipoSalidaNavigation.Descripcion)
                );

            CreateMap<DetalleVentaDTO, DetalleSalida>();

            #endregion

            #region Reporte
            CreateMap<DetalleSalida, ReporteDTO>()
                .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.NumeroDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destino =>
                    destino.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.TipoSalida,
                    opt => opt.MapFrom(origen => origen.TipoSalidaNavigation.Descripcion)
                );
            #endregion Reporte
        }
    }
}
