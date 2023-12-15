using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Models
{
    public class DTOCliente_Fatture:DTOCliente
    {
        public List<DTOFattura> FatturaList { get; set; } 
    }
}
