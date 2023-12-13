using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IProdottiQueries
    {
        public Task<IEnumerable<DTOProdotto>> GetAll();
        public Task<DTOProdotto> GetProdottoByID(string id);
      
    }
}
