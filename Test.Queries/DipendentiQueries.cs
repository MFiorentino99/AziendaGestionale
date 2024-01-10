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
    public class DipendentiQueries : IDipendentiQueries
    {
        private readonly string _tableName = "A_DIPENDENTE";
        private readonly string _connectionString;
        public DipendentiQueries(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }
        public async Task<IEnumerable<DTODipendente>> BestVendor(int min, int max)
        {
            string query =
                $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA, STIPENDIO 
                FROM {_tableName} WHERE ID_DIPENDENTE IN( 
                    SELECT ID_VENDITORE FROM (
                         SELECT ID_VENDITORE,COUNT(ID_VENDITORE) AS N_Vendite FROM A_FATTURA
                         WHERE EXTRACT(YEAR FROM DATA_VENDITA) BETWEEN :ANNO_MIN AND :ANNO_MAX GROUP BY(ID_VENDITORE))
                         WHERE N_Vendite IN 
                            (SELECT MAX(N_Vendite) 
                            FROM( 
                                SELECT ID_VENDITORE,COUNT(ID_VENDITORE) AS N_Vendite 
                                FROM A_FATTURA 
                                WHERE EXTRACT(YEAR FROM DATA_VENDITA) BETWEEN :ANNO_MIN AND :ANNO_MAX 
                                GROUP BY(ID_VENDITORE))))";
            using (var connecction = new OracleConnection(_connectionString))
            {
                var res = await connecction.QueryAsync<DTODipendente>(query, new
                {
                    ANNO_MIN = min,
                    ANNO_MAX = max
                });
                return res;
            }
        }

        public async Task<IEnumerable<dynamic>> ChainedNameSurname()
        {
            string query = $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE,
                            RTRIM(NOME) || ' ' || RTRIM(COGNOME) AS CREDENZIALI FROM {_tableName}
                            ORDER BY ID_DIPENDENTE";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.QueryAsync(query);
                return resp;
            }
        }

        public async Task<IEnumerable<dynamic>> CheckEmployeeInGestione()
        {
            string query =
                $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(Nome) NOME, RTRIM(Cognome) COGNOME 
                FROM {_tableName} 
                WHERE ID_DIPENDENTE NOT IN (
                    SELECT ID_DIPENDENTE
                    FROM A_GESTIONE ) "
                //" AND SETTORE IS NULL"
                ;
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync(query);
                return resp;
            }
        }

        public async Task<IEnumerable<dynamic>> EmployeeOfOtherSectors(string sector)
        {
            string query = $@"SELECT RTRIM(D.NOME) NOME, D.SETTORE FROM {_tableName} D 
                            WHERE RTRIM(D.SETTORE)<>:SETTORE ";
            using (var connection = new OracleConnection(_connectionString))
            {
                var res = await connection.QueryAsync(query, new { SETTORE = sector });
                return res;
            }
        }

        public async Task<IEnumerable<DTODipendente>> GetAll()
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                string query =
                    $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME,
                       STIPENDIO, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA
                       FROM {_tableName}
                       ORDER BY ID_DIPENDENTE";
                var resp = await connection.QueryAsync<DTODipendente>(query);
                return resp;
            }
        }

        public async Task<DTODipendente> GetById(string id)
        {
            string query =
                 $@"SELECT ID_DIPENDENTE, RTRIM(NOME) NOME,RTRIM(COGNOME) COGNOME,STIPENDIO,
                    RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA 
                    FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.QuerySingleAsync<DTODipendente>(query, new { ID_DIPENDENTE = id });
                return resp;
            }
        }

        public async Task<IEnumerable<DTODipendente>> LongestSurname()
        {
            string query = $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(Nome) NOME, RTRIM(Cognome) COGNOME,LENGTH(TRIM(COGNOME)) AS COGNOME_LENGTH,
                 RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA, STIPENDIO 
                 FROM {_tableName}
                 WHERE LENGTH(TRIM(COGNOME)) = (
                    SELECT MAX(COGNOME_LENGTH) 
                    FROM (
                        SELECT ID_DIPENDENTE, LENGTH(TRIM(COGNOME)) AS COGNOME_LENGTH 
                        FROM {_tableName}))";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.QueryAsync<DTODipendente>(query);
                return resp;
            }
        }
    }
}
