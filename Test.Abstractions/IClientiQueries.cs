using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface IClientiQueries
    {
        public Task<IEnumerable<DTOCliente>> GetAll();
        public Task<DTOCliente> GetById(string id);
    }
}
