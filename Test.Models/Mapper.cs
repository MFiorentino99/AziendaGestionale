using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Test.Models.FileHelpers;

namespace Test.Models
{
    public class MapperInitializer
    {
        public static Mapper InitializeMapperCliente()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DTOCliente_Fatture,FHCliente>()
                .ForMember(dest => dest.Id_cliente, act => act.MapFrom(src=>src.Id_cliente))
                .ForMember(dest => dest.Nome, act => act.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Cognome, act => act.MapFrom(src => src.Cognome))
                .ForMember(dest => dest.Citta, act => act.MapFrom(src => src.Citta));
            }
            ) ;
            var mapper = new Mapper(config);
            return mapper;
        }

        public static Mapper InitializeMapperFatture()
        { 
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTOFattura, FHFattura>()
                .ForMember(dest => dest.Id_cliente, act => act.MapFrom(src => src.Id_cliente))
                .ForMember(dest => dest.Id_venditore, act => act.MapFrom(src => src.Id_venditore))
                .ForMember(dest => dest.Id_fattura, act => act.MapFrom(src => src.Id_fattura))
                .ForMember(dest => dest.Data_vendita, act => act.MapFrom(src => src.Data_vendita))
                .ForMember(dest => dest.Totale, act => act.MapFrom(src => src.Totale));
            }
            );
            
            var mapper = new Mapper(config);
            return mapper;

        }
    }
}
