using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IDettagliRepository
    {
        public Task<bool> CreateDettaglio(DTODettaglio dettaglio);
        public Task<bool> UpdateDettaglioById(string fattura,string prodotto,DTODettaglio dettaglio);
        public Task<bool> DeleteDettaglioById(string fattura, string prodotto);   

    }
}
