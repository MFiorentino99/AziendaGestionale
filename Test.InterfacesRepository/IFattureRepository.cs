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
        public Task<bool> CreateFattura(DTOFattura fattura);
        public Task<bool> UpdateFatturaByID(string id, string data, DTOFattura fattura);
        public Task<bool> DeleteFatturaByID(string id,string data);
        public Task<bool> UpdateTot();
    }
}
