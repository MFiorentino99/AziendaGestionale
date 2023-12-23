using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IDipendentiRepository
    {
        public Task<bool> CreateDipendente(DTODipendente dipendente);
        public Task<bool> UpdateDipendenteById(string id,DTODipendente dipendente);
        public Task<bool> DeleteDipendenteById(string id);
    }
}
