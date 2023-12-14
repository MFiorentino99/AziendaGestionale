using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IFattureRepository
    {
        public bool CreateFattura(DTOFattura fattura);
        public bool UpdateFatturaByID(string id, string data, DTOFattura fattura);
        public bool DeleteFatturaByID(string id,string data);
        public bool UpdateTot();
        public DateTime DateConverter(string data);
    }
}
