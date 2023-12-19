using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Abstractions
{
    public interface ICliente_FattureQueries
    {
       public Task<IEnumerable<DTOCliente_Fatture>> PrintListInvoicesPerClient();
    }
}
