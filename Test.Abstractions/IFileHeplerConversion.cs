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
        public string GetStringFromDTO(DTOCliente_Fatture dto);
        public void SaveRecordText();
    }
}
