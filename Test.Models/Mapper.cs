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
    public class MapperInitializer : Profile
    {
        public MapperInitializer() {
            CreateMap<DTOCliente_Fatture, FHCliente>();
            CreateMap<DTOFattura, FHFattura>();
            CreateMap<FHFattura,DTOFattura>();
            CreateMap<FHCliente,DTOCliente>();
        }
        /*
        public static Mapper InitializeMapperCliente()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DTOCliente_Fatture, FHCliente>();
            }
            ) ;
            var mapper = new Mapper(config);
            return mapper;
        }

        public static Mapper InitializeMapperFatture()
        { 
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DTOFattura, FHFattura>();
            }
            );
            var mapper = new Mapper(config);
            return mapper;

        }
        */
    }
}
