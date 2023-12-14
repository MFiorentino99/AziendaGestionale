using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IDettagliRepository
    {
        public bool CreateDettaglio(DTODettaglio dettaglio);
        public bool UpdateDettaglioById(string fattura,string prodotto,DTODettaglio dettaglio);
        public bool DeleteDettaglioById(string fattura, string prodotto);   

    }
}
