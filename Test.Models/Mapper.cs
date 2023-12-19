using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Test.Models
{
    public class MapperInitializer
    {
        public static Mapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<DTOCliente, DTOCliente_Fatture>();
                cfg.CreateMap<DTOFattura,DTOCliente_Fatture>();
            }
            ) ;
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
