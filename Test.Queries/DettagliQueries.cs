using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Abstractions;
using Test.Models;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace Test.Queries
{
    public class DettagliQueries : IDettagliQueries
    {
        private readonly string _tableName = "A_DETTAGLIO";
        private readonly string _connectionString;

        public DettagliQueries(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }
        public async Task<IEnumerable<DTODettaglio>> GetAll()
        {
            string query = $@"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(PRODOTTO) PRODOTTO, QUANTITY AS QUANTITA, COSTO 
                            FROM {_tableName}
                            ORDER BY ID_FATTURA";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<DTODettaglio>(query);
                return resp;
            }
        }

        public async Task<DTODettaglio> GetDettaglioById(string fattura, string prodotto)
        {
            string query = $@"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(PRODOTTO) PRODOTTO, QUANTITY AS QUANTITA, COSTO
                            FROM {_tableName} 
                            WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO) = LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QuerySingleAsync<DTODettaglio>(query, new
                {
                    ID_FATTURA = fattura,
                    PRODOTTO = prodotto
                });
                return resp;
            }
        }
    }
}
