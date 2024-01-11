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
using System.Globalization;
using ExtensionMethods;

namespace Test.Repositories
{
    public class GestioneRepository : IGestioneRepository
    {
        private readonly string _connectionString;
        private readonly string _tableName = "A_GESTIONE";
        public GestioneRepository(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("oracleDB");
        }
        public async Task<bool> CreateGestione(DTOGestione gestione)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_DIPENDENTE, DATA_ASSEGNAZIONE, SETTORE, CATEGORIA)
                VALUES (:ID_DIPENDENTE, :DATA_ASSEGNAZIONE, :SETTORE, :CATEGORIA)";
            using (var conn = new OracleConnection(_connectionString))
            {
                
                 var resp =await  conn.ExecuteAsync(query, new
                            {
                            ID_DIPENDENTE = gestione.Id_dipendente,
                            DATA_ASSEGNAZIONE = gestione.Data_assegnazione,
                            SETTORE = gestione.Settore,
                            CATEGORIA = gestione.Categoria
                            });
                if (resp > 0) 
                { 
                    return true;
                }
                else { 
                    return false;
                }
            }
        }


        public async Task<bool> DeleteGestioneById(string id, string date)
        {
            string query = $@"DELETE FROM {_tableName} 
                                WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            DateTime d;
            try
            {
                d= date.ConvertFromString();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp =await conn.ExecuteAsync(query, new
                {
                    ID_DIPENDENTE = id,
                    DATA_ASSEGNAZIONE = d
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

        public async Task<bool> UpdateGestioneById(string id, string date, DTOGestione gestione)
        {
            DateTime d;
            try
            {
                d= date.ConvertFromString();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            string query = $@"UPDATE {_tableName} SET 
                SETTORE=:SETTORE, CATEGORIA=:CATEGORIA 
                WHERE RTRIM(ID_DIPENDENTE)=:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
              
                    var resp = await conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d,
                        SETTORE = gestione.Settore,
                        CATEGORIA = gestione.Categoria
                    })
        ;
                if(resp > 0 ) 
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> CategoriaToLowerCase()
        {
            string query = $@"UPDATE {_tableName} SET SETTORE = LOWER(TRIM(SETTORE)),
                 CATEGORIA = LOWER(TRIM(CATEGORIA))";
            using (var conn = new OracleConnection(_connectionString))
            {
                var res =await conn.ExecuteAsync(query);
                if (res > 0)
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
