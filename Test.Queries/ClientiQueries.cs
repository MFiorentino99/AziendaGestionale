using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Abstractions;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace Test.Queries
{
    public class ClientiQueries : IClientiQueries
    {
        private readonly string _tableName = "A_CLIENTE";
        private readonly string _connectionString;
        public ClientiQueries(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }
        public async Task<IEnumerable<DTOCliente>> GetAll()
        {
            string query = $@"SELECT RTRIM(ID_CLIENTE) ID_CLIENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(CITTA) CITTA
                              FROM {_tableName}
                              ORDER BY ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<DTOCliente>(query);
                return resp;
            }
        }

        public async Task<DTOCliente> GetById(string id)
        {
            string query = $@"SELECT RTRIM(ID_CLIENTE) ID_CLIENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(CITTA) CITTA
                              FROM {_tableName} WHERE RTRIM(ID_CLIENTE) =:ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QuerySingleAsync<DTOCliente>(query, new { ID_CLIENTE = id });
                return resp;
            }
        }
    }
}
