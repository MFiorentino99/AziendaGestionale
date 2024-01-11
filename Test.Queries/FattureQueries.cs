using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Test.Abstractions;
using Test.Models;
using Dapper;
using System.Globalization;
using ExtensionMethods;

namespace Test.Queries
{
    public class FattureQueries : IFattureQueries
    {
        private readonly string _tableName = "A_FATTURA";
        private readonly string _connectionString;
        public FattureQueries(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }
        public async Task<IEnumerable<dynamic>> AnnualIncome()
        {
            string query = $@"SELECT EXTRACT(YEAR FROM DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO 
                FROM {_tableName} 
                GROUP BY (EXTRACT(YEAR FROM DATA_VENDITA))
                ORDER BY ANNO";
            using (var connection = new OracleConnection(_connectionString))
            {
                return await connection.QueryAsync(query);     
            }
        }

        public async Task<IEnumerable<dynamic>> AnnualIncomePerClient()
        {
            string query = $@"SELECT ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO_CLIENTE 
                FROM {_tableName} 
                GROUP BY ROLLUP(ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA))
                ORDER BY ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA) DESC";
            using (var connection = new OracleConnection(_connectionString))
            {
                return await connection.QueryAsync(query);
                
            }
        }

        public async Task<IEnumerable<dynamic>> AnnualIncomePerClientWithDetails()
        {
            string query = $@"SELECT TRIM(F.ID_CLIENTE) ID_CLIENTE,TRIM(C.NOME) AS NOME,TRIM(C.COGNOME) AS COGNOME,
                EXTRACT(YEAR FROM F.DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO_CLIENTE 
                FROM {_tableName} F 
                INNER JOIN A_CLIENTE C ON C.ID_CLIENTE = F.ID_CLIENTE 
                GROUP BY ROLLUP(F.ID_CLIENTE,EXTRACT(YEAR FROM F.DATA_VENDITA), C.NOME,C.COGNOME) 
                ORDER BY F.ID_CLIENTE,EXTRACT(YEAR FROM F.DATA_VENDITA) DESC ";
            using (var connection = new OracleConnection(_connectionString))
            {
                return await connection.QueryAsync(query); 
            }
        }


        public async Task<IEnumerable<DTOFattura>> GetAll()
        {
            string query = $@"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(ID_VENDITORE) ID_VENDITORE,
                            RTRIM(ID_CLIENTE) ID_CLIENTE, DATA_VENDITA, TOTALE
                            FROM {_tableName}
                            ORDER BY ID_FATTURA";
            using (var conn = new OracleConnection(_connectionString))
            {
                return await conn.QueryAsync<DTOFattura>(query);
            }
        }

        public async Task<DTOFattura?> GetById(string id, string data)
        {
            string query = $@"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(ID_VENDITORE) ID_VENDITORE,
                            RTRIM(ID_CLIENTE) ID_CLIENTE, DATA_VENDITA, TOTALE
                            FROM {_tableName} WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA = :DATA_VENDITA ";
            DateTime d;
            try
            {
                d = data.ConvertFromString();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
            using (var conn = new OracleConnection(_connectionString))
            {
                return await conn.QuerySingleAsync<DTOFattura>(query, new
                {
                    ID_FATTURA = id,
                    DATA_VENDITA = d
                });
               
            }
        }
    }
}
