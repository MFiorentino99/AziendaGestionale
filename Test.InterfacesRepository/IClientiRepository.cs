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
        public bool CreateCliente(DTOCliente cliente);
        public bool DeleteClienteById(string id);
        public bool UpdateClienteById(string id, DTOCliente cliente);
    }
}
