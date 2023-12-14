using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IDettagliQueries
    {
        public Task<IEnumerable<DTODettaglio>> GetAll();
        public Task<DTODettaglio> GetDettaglioById(string fattura, string prodotto);
    }
}
