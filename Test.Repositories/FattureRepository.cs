using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InterfacesRepository;
using Test.Models;
using Dapper;

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
        public bool CreateFattura(DTOFattura fattura)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_FATTURA, DATA_VENDITA, ID_VENDITORE, ID_CLIENTE, TOTALE) 
                VALUES (:ID_FATTURA, :DATA_VENDITA, :ID_VENDITORE, :ID_CLIENTE, :TOTALE )";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
                {
                    ID_FATTURA = fattura.Id_fattura,
                    DATA_VENDITA = fattura.Data_vendita,
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

        public DateTime DateConverter(string data)
        {
            try
            {
                return DateTime.ParseExact(data, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return DateTime.Parse("00/00/0000", CultureInfo.InvariantCulture);
            }
        }

        public bool DeleteFatturaByID(string id, string data)
        {
            string query = $"DELETE FROM {_tableName} WHERE" +
                $" RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA=:DATA_VENDITA ";
            DateTime d = DateConverter(data);
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
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

        public bool UpdateFatturaByID(string id, string data, DTOFattura fattura)
        {
            DateTime d = DateConverter(data);
            string query = $@"UPDATE {_tableName} SET 
                 ID_VENDITORE =:ID_VENDITORE, ID_CLIENTE =:ID_CLIENTE, TOTALE =:TOTALE
                 WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA = :DATA_VENDITA";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
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
                return resp > 0;
                /*
                if(resp > 0)
                {
                    return true;
                }
                else
                {
                    return false ;
                }*/
            }
        }
    }
}
