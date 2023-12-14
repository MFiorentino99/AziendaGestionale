using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.InterfacesRepository;
using Test.Models;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace Test.Repositories
{
    public class DettagliRepository : IDettagliRepository
    {
        private readonly string _tableName = "A_DETTAGLIO";
        private readonly string _connectionString;

        public DettagliRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }

        public bool CreateDettaglio(DTODettaglio dettaglio)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_FATTURA, PRODOTTO, COSTO, QUANTITY ) 
                VALUES (:ID_FATTURA, LOWER(:PRODOTTO), :COSTO, :QUANTITA)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
                {
                    ID_FATTURA = dettaglio.Id_fattura,
                    PRODOTTO = dettaglio.Prodotto,
                    COSTO = dettaglio.Costo,
                    QUANTITA = dettaglio.Quantita
                });
                if (resp >0)
                {                  
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool DeleteDettaglioById(string fattura, string prodotto)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO)=LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
                {
                    ID_FATTURA = fattura,
                    PRODOTTO = prodotto
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

        public bool UpdateDettaglioById(string fattura, string prodotto, DTODettaglio dettaglio)
        {
            string query = $@"UPDATE {_tableName} SET 
                 COSTO =:COSTO, QUANTITY  =:QUANTITA 
                 WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO) =LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Execute(query, new
                {
                    ID_FATTURA = fattura,
                    PRODOTTO = prodotto,
                    COSTO = dettaglio.Costo,
                    QUANTITA = dettaglio.Quantita
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
