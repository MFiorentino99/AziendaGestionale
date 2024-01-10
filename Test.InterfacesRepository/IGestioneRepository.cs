using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IGestioneRepository
    {
        public Task<bool> UpdateGestioneById(string id, string date, DTOGestione gestione);
        public Task<bool> CreateGestione(DTOGestione gestione);
        public Task<bool> DeleteGestioneById(string id,string date);
        public Task<bool> CategoriaToLowerCase();
    }
}
