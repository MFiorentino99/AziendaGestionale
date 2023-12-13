using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public  interface IGestioneQuery
    {
        public Task<IEnumerable<DTOGestione>> GetAll();
        public Task<DTOGestione> GetGestioneById(string id, string id2);
        public bool UpdateGestioneById(string id, string id2, DTOGestione element);
        public bool CreateGestione(DTOGestione element);
        public bool DeletegestoineById(string id, string id2);
    }
}
