using FileHelpers;
using Test.Models;
using Test.Models.FileHelpers;
using Test.Abstractions;
using AutoMapper.Internal;

namespace ApplicationLayer
{
    public class FileHelperConversion : IFileHeplerConversion
    {
        
        private List<object> _files;
        private static MultiRecordEngine Engine => new MultiRecordEngine(
            typeof(FHCliente),typeof(FHFattura));

        public FileHelperConversion()
        {
            _files = new List<object>();
        }
        public string GetStringFromDTO(DTOCliente_Fatture dto)
        {
            var records = new List<object>();
            var mapperCliente = MapperInitializer.InitializeMapperCliente();
            var mapperFattura = MapperInitializer.InitializeMapperFatture();

            FHCliente fhCliente = mapperCliente.Map<FHCliente>(dto);
            records.Add(fhCliente);

            foreach(var fattura in  dto.FatturaList)
            {
                FHFattura fHFattura = mapperFattura.Map<FHFattura>(fattura);
                records.Add(fHFattura);
            }
            
            _files.AddRange(records);
            
            var s = Engine.WriteString(records) + "\n";
            return s;
        }

        public void SaveRecordText()
        {
            Engine.WriteFile("file.txt",_files.ToArray());
        }
    }
}
