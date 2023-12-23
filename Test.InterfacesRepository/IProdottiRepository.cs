using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IProdottiRepository
    {
        public Task<bool> UpdateProdottoById(string id, DTOProdotto prodotto);
        public Task<bool> CreateProdotto(DTOProdotto element);
        public Task<bool> DeleteProdototById(string id);
    }
}
