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
            records.Add(new FHCliente(new DTOCliente()
            {
                Id_cliente = dto.Id_cliente,
                Citta = dto.Citta,
                Cognome = dto.Cognome,
                Nome = dto.Nome
            }));

            foreach(var fattura in  dto.FatturaList)
            {
                records.Add(new FHFattura(fattura));
            }

            var s = Engine.WriteString(records) + "\n";
            return s;
        }

    }
}
