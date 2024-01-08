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
    public class DipendentiRepository : IDipendentiRepository
    {
        private readonly string _tableName = "A_DIPENDENTE";
        private readonly string _connectionString;

        public DipendentiRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oracleDB");
        }
        public async Task<bool> CreateDipendente(DTODipendente dipendente)
        {
            string query = $@"INSERT INTO {_tableName} 
                (ID_DIPENDENTE, NOME, COGNOME, STIPENDIO, SETTORE, CATEGORIA) 
                VALUES (:ID_DIPENDENTE, :NOME, :COGNOME, :STIPENDIO, :SETTORE, :CATEGORIA)";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp =await connection.ExecuteAsync(query, new
                {
                    ID_DIPENDENTE = dipendente.Id_dipendente,
                    NOME = dipendente.Nome,
                    COGNOME = dipendente.Cognome,
                    STIPENDIO = dipendente.Stipendio,
                    SETTORE = dipendente.Settore,
                    CATEGORIA = dipendente.Categoria
                });
                if (resp >0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }

        public async Task<bool> DeleteDipendenteById(string id)
        {
            string query = $@"DELETE FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.ExecuteAsync(query, new { ID_DIPENDENTE = id });
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

        public async Task<bool> UpdateDipendenteById(string id, DTODipendente dipendente)
        {
            string query =
                $@"UPDATE {_tableName} SET 
                COGNOME = :COGNOME, NOME = :NOME, STIPENDIO = :STIPENDIO, SETTORE = :SETTORE, CATEGORIA =:CATEGORIA 
                WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";

            using (var connection = new OracleConnection(_connectionString))
            {
                var resp =await connection.ExecuteAsync(query, new
                {
                    id_dipendente = id,
                    nome = dipendente.Nome,
                    cognome = dipendente.Cognome,
                    stipendio = dipendente.Stipendio,
                    settore = dipendente.Settore,
                    categoria = dipendente.Categoria
                });
                if (resp > 0)
                {                   
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }
    }
}
