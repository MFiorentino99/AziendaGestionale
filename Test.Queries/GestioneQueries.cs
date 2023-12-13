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
    public class GestioneQueries : IHttpCallMoreThanOneId<DTOGestione>
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
        
        public async Task<DTOGestione> GetOneByIDs(string id, string id2)
        {
            DateTime date = DateConvert(id2);
            string query = $@"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, DATA_ASSEGNAZIONE, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA 
                            FROM {_tableName} WHERE ID_DIPENDENTE=:ID_DIPENDENTE AND RTRIM(DATA_ASSEGNAZIONE)=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(id, date))
                {
                    var resp = await conn.QuerySingleAsync<DTOGestione>(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = date
                    });
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool CreateElement(DTOGestione element)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_DIPENDENTE, DATA_ASSEGNAZIONE, SETTORE, CATEGORIA) 
                VALUES (:ID_DIPENDENTE, :DATA_ASSEGNAZIONE, :SETTORE, :CATEGORIA)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (!ElementExists(element.Id_dipendente, element.Data_assegnazione))
                {
                    conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = element.Id_dipendente,
                        DATA_ASSEGNAZIONE = element.Data_assegnazione,
                        SETTORE = element.Settore,
                        CATEGORIA = element.Categoria
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateByIds(string id, string id2, DTOGestione element)
        {
            DateTime d = DateConvert(id2);
            string query = $@"UPDATE {_tableName} 
                            SET SETTORE=:SETTORE, CATEGORIA=:CATEGORIA 
                            WHERE RTRIM(ID_DIPENDENTE)=:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(id, d))
                {
                    conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d,
                        SETTORE = element.Settore,
                        CATEGORIA = element.Categoria
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteOneByIds(string id, string id2)
        {
            string query = $@"DELETE FROM {_tableName} 
                              WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            DateTime d = DateConvert(id2);
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(id, d))
                {
                    conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool ElementExists(string id, DateTime date)
        {
            
            string query = $@"SELECT * FROM {_tableName} 
                            WHERE RTRIM(ID_DIPENDENTE)=:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Query<DTOGestione>(query, new
                {
                    ID_DIPENDENTE = id,
                    DATA_ASSEGNAZIONE = date
                });
                if (resp.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private DateTime DateConvert(string d)
        {
            DateTime today = DateTime.Today;
            try
            {
                return DateTime.ParseExact(d, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return DateTime.Parse("00/00/0000", CultureInfo.InvariantCulture);
            }


        }

    }
}
