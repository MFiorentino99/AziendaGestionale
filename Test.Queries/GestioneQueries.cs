using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Abstractions;
using Test.Models;
using Dapper;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace Test.Queries
{
    public class GestioneQueries : IGestioneQuery
    {
        private readonly string _connectionString;
        private readonly string _tableName = "A_GESTIONE";
        public GestioneQueries(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }        

        public async Task<IEnumerable<DTOGestione>> GetAll()
        {
            string query = $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, DATA_ASSEGNAZIONE, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA
                     FROM {_tableName}";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<DTOGestione>(query);
                return resp;

            }
        }

        public async Task<DTOGestione> GetGestioneById(string id, string date)
        {
            string query = $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, DATA_ASSEGNAZIONE, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA
                FROM {_tableName} 
                WHERE ID_DIPENDENTE=:ID_DIPENDENTE AND RTRIM(DATA_ASSEGNAZIONE)=:DATA_ASSEGNAZIONE";
            DateTime d = DateConverter(date);
            using (var conn = new OracleConnection(_connectionString))
            {
                    var resp = await conn.QuerySingleAsync<DTOGestione>(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d
                    });
                return resp;
            }
        }

        public DateTime DateConverter(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return DateTime.Parse("00/00/0000", CultureInfo.InvariantCulture);
            }
        }

        public async Task<IEnumerable<dynamic>> RoleInInvoiceDate()
        {
            string query = $@"SELECT DISTINCT RTRIM(T.ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(G.SETTORE) SETTORE,
               RTRIM(G.CATEGORIA) CATEGORIA,T.DATA_RECENTE,
               RTRIM(T.ID_FATTURA) ID_FATTURA, RTRIM(T.DATA_VENDITA) DATA_VENDITA 
               FROM (
                   SELECT G.ID_DIPENDENTE,MAX(G.DATA_ASSEGNAZIONE) DATA_RECENTE,F.DATA_VENDITA, F.ID_FATTURA 
                   FROM {_tableName} G 
                   JOIN A_FATTURA F ON G.ID_DIPENDENTE=F.ID_VENDITORE 
                   WHERE F.DATA_VENDITA >= G.DATA_ASSEGNAZIONE 
                   GROUP BY (G.ID_DIPENDENTE,F.DATA_VENDITA, F.ID_FATTURA) )T
               JOIN {_tableName} G ON RTRIM(G.ID_DIPENDENTE)=RTRIM(T.ID_DIPENDENTE) 
               WHERE T.DATA_RECENTE = G.DATA_ASSEGNAZIONE 
               ORDER BY ID_DIPENDENTE ASC, DATA_VENDITA DESC";
            using (var conn = new OracleConnection(_connectionString))
            {
                var res = await conn.QueryAsync(query);
                return res;
            }
        }
    }
}
