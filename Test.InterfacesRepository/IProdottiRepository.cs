using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.InterfacesRepository
{
    public interface IProdottiRepository
    {
        public bool UpdateProdottoById(string id, DTOProdotto prodotto);
        public bool CreateProdotto(DTOProdotto element);
        public bool DeleteProdototById(string id);
    }
}
