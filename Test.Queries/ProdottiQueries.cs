using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Abstractions;
using Test.Models;
using Dapper;

namespace Test.Queries
{
    public class ProdottiQueries : IProdottiQueries
    {
        private readonly string _tableName = "A_PRODOTTO";
        private readonly string _connectionString;
        public ProdottiQueries(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }


        public async Task<IEnumerable<DTOProdotto>> GetAll()
        {
            string query = $@"SELECT RTRIM(NOME) NOME, RTRIM(CATEGORIA) CATEGORIA, COSTO_PRODUZIONE
                            FROM {_tableName}";
            using(var conn = new OracleConnection(_connectionString))
            {
                var result = await conn.QueryAsync<DTOProdotto>(query); 
                return result;
            }
        }


        public async Task<DTOProdotto> GetProdottoByID(string nome)
        {
            string find = $@"SELECT RTRIM(NOME) NOME, RTRIM(CATEGORIA) CATEGORIA, COSTO_PRODUZIONE
                            FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {

                var res = await conn.QuerySingleAsync<DTOProdotto>(find, new { NOME = nome });
                return res;
            }
        }
    }
}
