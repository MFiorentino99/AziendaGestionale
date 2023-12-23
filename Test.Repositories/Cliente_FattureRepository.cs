using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InterfacesRepository;

namespace Test.Repositories
{
    public class Cliente_FattureRepository : ICliente_FattureRepository
    {
        private readonly string _connectionString;
        public Cliente_FattureRepository(IConfiguration congif)
        {
            _connectionString = congif.GetConnectionString("oracleDB");
        }
       /* 
        * public async Task<bool> InsertStringIntoClient_Invoices(string insertText)
        {
            return await true;


        }
       */
    }
}
