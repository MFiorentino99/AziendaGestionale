using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.InterfacesRepository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Test.Repositories
{
    public class ClientiRepository : IClientiRepository
    {
        private readonly string _tableName = "A_CLIENTE";
        private readonly string _connectionString;
        public ClientiRepository(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }
        public async Task<bool> CreateCliente(DTOCliente cliente)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_CLIENTE, NOME, COGNOME, CITTA) 
                VALUES (:ID_CLIENTE, :NOME, :COGNOME, :CITTA)";
            try
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    var resp = await conn.ExecuteAsync(query, new
                    {
                        ID_CLIENTE = cliente.Id_cliente,
                        NOME = cliente.Nome,
                        COGNOME = cliente.Cognome,
                        CITTA = cliente.Citta
                    });
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteClienteById(string id)
        {
            string query = $@"DELETE FROM {_tableName} WHERE RTRIM(ID_CLIENTE) =:ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.ExecuteAsync(query, new { ID_CLIENTE = id });
                if (resp > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateClienteById(string id, DTOCliente cliente)
        {
            string query = $@"UPDATE {_tableName} SET 
                NOME =:NOME, COGNOME =:COGNOME, CITTA =:CITTA 
                WHERE RTRIM(ID_CLIENTE)=:ID_CLIENTE";

            using (var conn = new OracleConnection(_connectionString))
            {
                var resp =await conn.ExecuteAsync(query, new
                {
                    ID_CLIENTE = id,
                    NOME = cliente.Nome,
                    COGNOME = cliente.Cognome,
                    CITTA = cliente.Citta
                });
                if (resp > 0)
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
