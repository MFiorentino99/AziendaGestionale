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
        public bool UpdateGestioneById(string id, string date, DTOGestione gestione);
        public bool CreateGestione(DTOGestione gestione);
        public bool DeleteGestioneById(string id,string date);
        public bool CategoriaToLowerCase();
        public DateTime DateConverter(string date);
    }
}
