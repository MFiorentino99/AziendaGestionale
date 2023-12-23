using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Test.Models;
using Test.Abstractions;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;


namespace Test.Queries
{
    public class Cliente_FattureQueries : ICliente_FattureQueries
    {
        private readonly string _connectionString;

        public Cliente_FattureQueries(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }


        public async Task<IEnumerable<DTOCliente_Fatture>> PrintListInvoicesPerClient()
        {
            var clienteDictionary = new Dictionary<string, DTOCliente_Fatture>();
            string query = $@"SELECT C.ID_CLIENTE, TRIM(C.NOME) NOME, TRIM(C.COGNOME) COGNOME, TRIM(C.CITTA) CITTA, 
                            F.ID_FATTURA, F.ID_VENDITORE, DATA_VENDITA,TOTALE
                            FROM A_CLIENTE C
                            INNER JOIN A_FATTURA F ON
                            C.ID_CLIENTE = F.ID_CLIENTE
                            ORDER BY C.ID_CLIENTE";
            using(var conn = new OracleConnection(_connectionString))
            {
                var res = await conn.QueryAsync<DTOCliente_Fatture, DTOFattura, DTOCliente_Fatture>(query,
                    (cliente, fatture) =>
                    {
                        if(!clienteDictionary.TryGetValue(cliente.Id_cliente,out DTOCliente_Fatture clienteEntry))
                        {
                            clienteEntry = cliente;
                            clienteEntry.FatturaList = new List<DTOFattura>();
                            clienteDictionary.Add(cliente.Id_cliente, clienteEntry);
                        }
                        if (fatture != null)
                        {
                            clienteEntry.FatturaList.Add(fatture);
                        }
                        return clienteEntry;
                    }, 
                    splitOn:"ID_FATTURA"                   
                    );
                return res.Distinct().ToList();

            }
        }


    }
               
}

