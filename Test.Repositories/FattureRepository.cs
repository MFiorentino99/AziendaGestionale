using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Test.InterfacesRepository;
using Test.Models;
using Dapper;
using ExtensionMethods;

namespace Test.Repositories
{
    public class FattureRepository : IFattureRepository
    {
        private readonly string _tableName = "A_FATTURA";
        private readonly string _connectionString;

        public FattureRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }
        public async Task<bool> CreateFattura(DTOFattura fattura)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_FATTURA, DATA_VENDITA, ID_VENDITORE, ID_CLIENTE, TOTALE) 
                VALUES (:ID_FATTURA, :DATA_VENDITA, :ID_VENDITORE, :ID_CLIENTE, :TOTALE )";
            try
            {
                using (var conn = new OracleConnection(_connectionString))
                {
                    var resp = await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = fattura.Id_fattura,
                        DATA_VENDITA = fattura.Data_vendita,
                        ID_VENDITORE = fattura.Id_venditore,
                        ID_CLIENTE = fattura.Id_cliente,
                        TOTALE = fattura.Totale
                    });
                    return true;
                }
            } catch {
                return false;
            }

        }
        
        public async Task<bool> DeleteFatturaByID(string id, string data)
        {
            string query = $"DELETE FROM {_tableName} WHERE" +
                $" RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA=:DATA_VENDITA ";
            DateTime d = data.ConvertFromString() ;
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.ExecuteAsync(query, new
                {
                    ID_FATTURA = id,
                    DATA_VENDITA = d
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

        public async Task<bool> UpdateFatturaByID(string id, string data, DTOFattura fattura)
        {
            DateTime d = data.ConvertFromString();
            string query = $@"UPDATE {_tableName} SET 
                 ID_VENDITORE =:ID_VENDITORE, ID_CLIENTE =:ID_CLIENTE, TOTALE =:TOTALE
                 WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA = :DATA_VENDITA";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.ExecuteAsync(query, new
                {
                    ID_FATTURA = id,
                    DATA_VENDITA = d,
                    ID_VENDITORE = fattura.Id_venditore,
                    ID_CLIENTE = fattura.Id_cliente,
                    TOTALE = fattura.Totale
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

        public async Task<bool> UpdateTot()
        {
            string query = $@"UPDATE {_tableName} SET {_tableName}.TOTALE = (
                SELECT SUM(A_DETTAGLIO.COSTO * A_DETTAGLIO.QUANTITY) 
                FROM A_DETTAGLIO  
                WHERE {_tableName}.ID_FATTURA = A_DETTAGLIO.ID_FATTURA)";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.ExecuteAsync(query);

                if(resp > 0)
                {
                    return true;
                }
                else
                {
                    return false ;
                }
            }
        }
    }
}
