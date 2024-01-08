using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InterfacesRepository;
using Test.Models;
using Dapper;

namespace Test.Repositories
{
    public class ProdottiRepository : IProdottiRepository
    {
        private readonly string _tableName = "A_PRODOTTO";
        private readonly string _connectionString;

        public ProdottiRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }

        public async Task<bool> CreateProdotto(DTOProdotto prodotto)
        {
            string query = $@"INSERT INTO {_tableName} 
                            (NOME,CATEGORIA,COSTO_PRODUZIONE)
                            VALUES (LOWER(:NOME), :CATEGORIA, :COSTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
               var response =await conn.ExecuteAsync(query, new
                    {
                        NOME = prodotto.Nome,
                        CATEGORIA = prodotto.Categoria,
                        COSTO = prodotto.Costo_produzione
                    });
                if (response > 0) 
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteProdototById(string id)
        {
            string query = $@"DELETE FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                
                    var resp =await conn.ExecuteAsync(query, new { NOME = id });
                if(resp > 0) 
                { 
                   return true;
                }
                else
                {
                   return false;
                }
            }
        }

        public async Task<bool> UpdateProdottoById(string id, DTOProdotto prodotto)
        {
            string query = $@"UPDATE {_tableName} SET CATEGORIA=:CATEGORIA, COSTO_PRODUZIONE=:COSTO
                            WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var response =await conn.ExecuteAsync(query, new
                {
                    NOME = id,
                    CATEGORIA = prodotto.Categoria,
                    COSTO = prodotto.Costo_produzione
                });
                if(response > 0)
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
