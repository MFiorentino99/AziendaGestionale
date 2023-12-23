using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IFileHeplerConversion
    {
        List<DTOFattura> GetDTOFromString(string recordsText, out List<DTOCliente> cliente);
        public string GetStringFromDTO(IEnumerable<DTOCliente_Fatture> dto);
        public void SaveRecordText(); 
    }
}
