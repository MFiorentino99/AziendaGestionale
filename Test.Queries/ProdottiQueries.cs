using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
    public class ProdottiQueries : IHttpCall<DTOProdotto>
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

        public async Task<DTOProdotto> GetOneByID(string nome)
        {
            string find = $@"SELECT RTRIM(NOME) NOME, RTRIM(CATEGORIA) CATEGORIA, COSTO_PRODUZIONE
                            FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(nome))
                {
                    var result = await conn.QuerySingleAsync<DTOProdotto>(find, new { NOME = nome });
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<bool> CreateElement(DTOProdotto prodotto)
        {
            string query = $@"INSERT INTO {_tableName} 
                            (NOME,CATEGORIA,COSTO_PRODUZIONE)
                            VALUES (LOWER(:NOME), :CATEGORIA, :COSTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (!ElementExists(prodotto.Nome))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        NOME = prodotto.Nome,
                        CATEGORIA = prodotto.Categoria,
                        COSTO = prodotto.Costo_produzione
                    });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateOneById(string nome,DTOProdotto prodotto)
        {
            string query = $@"UPDATE {_tableName} SET CATEGORIA=:CATEGORIA, COSTO_PRODUZIONE=:COSTO
                            WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(nome))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        NOME = nome,
                        CATEGORIA = prodotto.Categoria,
                        COSTO = prodotto.Costo_produzione
                    });
                    return true;
                }
                else
                {
                    return false; 
                }
            }
        }
        public async Task<bool> DeleteOneById(string nome)
        {
            string query = $@"DELETE FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ElementExists(nome))
                {
                    await conn.ExecuteAsync(query, new { NOME = nome });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool ElementExists(string id)
        {
            string find = $@"SELECT * FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Query<DTOProdotto>(find, new { NOME = id });
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
    }
}
