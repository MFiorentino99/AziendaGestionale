using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IClientiRepository
    {
        public Task<bool> CreateCliente(DTOCliente cliente);
        public Task<bool> DeleteClienteById(string id);
        public Task<bool> UpdateClienteById(string id, DTOCliente cliente);
    }
}
