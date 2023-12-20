using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Models.FileHelpers;

namespace ApplicationLayer
{
    public class FileHelperConversion
    {
        private static MultiRecordEngine Engine => new MultiRecordEngine(
            typeof(FHCliente),typeof(FHFattura));

        public static string GetStringFromDTO(DTOCliente_Fatture dto)
        {
            var records = new List<object>();
            var mapperCliente = MapperInitializer.InitializeMapperCliente();
            var mapperFattura = MapperInitializer.InitializeMapperFatture();

            FHCliente fhCliente = mapperCliente.Map<FHCliente>(dto);
            records.Add(fhCliente);
            /*
            records.Add(new FHCliente(new DTOCliente()
            {
                Id_cliente = dto.Id_cliente,
                Citta = dto.Citta,
                Cognome = dto.Cognome,
                Nome = dto.Nome
            }));
            */
            foreach(var fattura in  dto.FatturaList)
            {
                FHFattura fHFattura = mapperFattura.Map<FHFattura>(fattura);
                records.Add(fHFattura);
               // records.Add(new FHFattura(fattura));
            }

            var s = Engine.WriteString(records) + "\n";
            return s;
        }

    }
}
