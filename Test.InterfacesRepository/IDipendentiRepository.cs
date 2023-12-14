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
        public bool CreateDipendente(DTODipendente dipendente);
        public bool UpdateDipendenteById(string id,DTODipendente dipendente);
        public bool DeleteDipendenteById(string id);
    }
}
