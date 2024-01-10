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
        public Task<DTOGestione> GetGestioneById(string id, string date);
        public Task<IEnumerable<dynamic>> RoleInInvoiceDate();
    }
}
