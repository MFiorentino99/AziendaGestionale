using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AziendaGestionale.Models;
using AziendaGestionale.Models.Entities;
using AziendaGestionale.Models.DTO;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using Dapper;
using System.Drawing;

namespace AziendaGestionale.Controllers
{
    //[Route("api/DipendentiCtrl")]
    //[ApiController]
    public class DipendentiCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";
        private readonly string _tableName = "A_DIPENDENTE";
        public DipendentiCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/DipendentiCtrl
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<DipendenteDTO>>> GetDipendente()
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                string query =
                    $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, STIPENDIO, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA" +
                    $" FROM {_tableName}";
                var resp = await connection.QueryAsync<DipendenteDTO>(query); 
                return Ok(resp);
            }

            /*
            if (_context.Dipendente == null)
            {
                return NotFound();
            }
              return await _context.Dipendente.Select(d => ItemToDTO(d)).ToListAsync();
         */

        }

        // GET: api/DipendentiCtrl/5
        //[HttpGet("{id}")]
        public async Task<ActionResult<DipendenteDTO>> GetDipendente(string id)
        {/*
          if (_context.Dipendente == null)
          {
              return NotFound();
          }
            var dipendente = await _context.Dipendente.FindAsync(id);

            if (dipendente == null)
            {
                return NotFound();
            }

            return ItemToDTO(dipendente);
           */
           
             string query =
                $"SELECT ID_DIPENDENTE, RTRIM(NOME) NOME,RTRIM(COGNOME) COGNOME,STIPENDIO,RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA " +
                $"FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";
            using (var connection = new OracleConnection(_connectionString))
            {
                if (DipendenteExists(id))
                {
                    var resp = await connection.QuerySingleAsync<DipendenteDTO>(query, new { ID_DIPENDENTE = id });
                    return Ok(resp);

                }
                else
                {
                    return NotFound("Dipendente non trovato");
                }
            }
               
        }

        // PUT: api/DipendentiCtrl/5
  
        //[HttpPut("{id}")]
        public async Task<IActionResult> PutDipendente(string id, DipendenteDTO dipendenteDTO)
        {
            string query = 
                $"UPDATE {_tableName} SET " +
                $"COGNOME = :COGNOME, NOME = :NOME, STIPENDIO = :STIPENDIO, SETTORE = :SETTORE, CATEGORIA =:CATEGORIA " +
                $"WHERE rtrim(ID_DIPENDENTE) =:ID_DIPENDENTE";
          
            using (var connection = new OracleConnection(_connectionString))
            {
                if (DipendenteExists(id))
                {
                    var resp = await  connection.ExecuteAsync(query, new
                         {
                        id_dipendente = id,
                        nome = dipendenteDTO.Nome,
                        cognome = dipendenteDTO.Cognome,
                        stipendio = dipendenteDTO.Stipendio,
                        settore = dipendenteDTO.Settore,
                        categoria = dipendenteDTO.Categoria
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }else
                {
                    return NotFound("Dipendente non trovato");
                }
              
            };
           
            /*  if (id != dipendenteDTO.Id_dipendente)
              {
                  return BadRequest();
              }

              var dipendente = await _context.Dipendente.FindAsync(id);
              _context.Entry(dipendente).State = EntityState.Modified;

              dipendente.Id_dipendente = id;
              dipendente.Cognome = dipendenteDTO.Cognome;
              dipendente.Nome = dipendenteDTO.Nome;
              dipendente.Stipendio = dipendenteDTO.Stipendio;
              dipendente.Settore = dipendenteDTO.Settore;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!DipendenteExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
            */
        }

        // POST: api/DipendentiCtrl
        //[HttpPost]
        public async Task<ActionResult<Dipendente>> PostDipendente(DipendenteDTO dipendenteDTO)
        {
            string query = $"INSERT INTO {_tableName} " +
                $"(ID_DIPENDENTE, NOME, COGNOME, STIPENDIO, SETTORE, CATEGORIA) " +
                $"VALUES (:ID_DIPENDENTE, :NOME, :COGNOME, :STIPENDIO, :SETTORE, :CATEGORIA)";
            using (var connection = new OracleConnection(_connectionString))
            {
                if (!DipendenteExists(dipendenteDTO.Id_dipendente))
                {
                    var resp =await  connection.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = dipendenteDTO.Id_dipendente,
                        NOME = dipendenteDTO.Nome,
                        COGNOME = dipendenteDTO.Cognome,
                        STIPENDIO = dipendenteDTO.Stipendio,
                        SETTORE = dipendenteDTO.Settore,
                        CATEGORIA = dipendenteDTO.Categoria
                    });
                    return Ok("Inserimento eseguito con successo");
                }
                else
                {
                    return BadRequest("Dipendente già esistente");
                }
            };
               
            /*
            var dipendente = new Dipendente
            {
                Id_dipendente = dipendenteDTO.Id_dipendente,
                Cognome = dipendenteDTO.Cognome,
                Nome = dipendenteDTO.Nome,
                Settore = dipendenteDTO.Settore,
                Stipendio = dipendenteDTO.Stipendio
            };
          if (_context.Dipendente == null)
          {
              return Problem("Entity set 'Context.Dipendente'  is null.");
          }
            _context.Dipendente.Add(dipendente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DipendenteExists(dipendente.Id_dipendente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDipendente", new { id = dipendente.Id_dipendente }, ItemToDTO(dipendente));
            */
        }

        // DELETE: api/DipendentiCtrl/5
        //[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDipendente(string id)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";
            using(var connection = new OracleConnection(_connectionString))
            {
                if (DipendenteExists(id))
                {
                    var resp = await connection.ExecuteAsync(query, new { ID_DIPENDENTE = id });
                    return Ok("Cancellazione eseguita con successo");
                }else 
                {  
                    return BadRequest(); 
                }
            }
            /*
            if (_context.Dipendente == null)
            {
                return NotFound();
            }
            var dipendente = await _context.Dipendente.FindAsync(id);
            if (dipendente == null)
            {
                return NotFound();
            }

            _context.Dipendente.Remove(dipendente);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        //[HttpGet]
        //[Route("/NoSettore/{settoreId}")]
        public async Task<IActionResult> NoVendor(string settoreId)
        {
            string query = $"SELECT RTRIM(D.NOME) NOME, D.SETTORE FROM {_tableName} D " +
                $"WHERE RTRIM(D.SETTORE)<>:SETTORE ";
          
            using (var connection = new OracleConnection(_connectionString))
            {
                var res = await connection.QueryAsync(query, new {settore = settoreId });
                return Ok(res);
            }
        }

        //[HttpGet]
        //[Route("/MigliorVenditore/{min}-{max}")]
        public  async Task<ActionResult> BestVendor(int min,int max)
        {
            string query = 
                $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA, STIPENDIO " +
                $"FROM {_tableName} WHERE ID_DIPENDENTE IN( " +
                    $"SELECT ID_VENDITORE FROM (" +
                        $" SELECT ID_VENDITORE,COUNT(ID_VENDITORE) AS N_Vendite FROM A_FATTURA" +
                        $" WHERE EXTRACT(YEAR FROM DATA_VENDITA) BETWEEN :ANNO_MIN AND :ANNO_MAX GROUP BY(ID_VENDITORE))" +
                        $" WHERE N_Vendite IN " +
                            $" (SELECT MAX(N_Vendite) " +
                            $"FROM( " +
                                $"SELECT ID_VENDITORE,COUNT(ID_VENDITORE) AS N_Vendite " +
                                $"FROM A_FATTURA " +
                                $"WHERE EXTRACT(YEAR FROM DATA_VENDITA) BETWEEN :ANNO_MIN AND :ANNO_MAX " +
                                $"GROUP BY(ID_VENDITORE))))";
            using (var connecction = new OracleConnection(_connectionString))
            {
                var res = await connecction.QueryAsync<DipendenteDTO>(query, new
                {
                    ANNO_MIN = min,
                    ANNO_MAX = max
                });
                return Ok(res);
            }
        }


        //[HttpGet]
        //[Route("/Concat_NomeCognome")]
        public async Task<ActionResult> Concat()
        {
            string query = $"SELECT RTRIM(NOME) || ' ' || RTRIM(COGNOME) AS CREDENZIALI FROM {_tableName}";
            using (var connection = new OracleConnection( _connectionString))
            {
                var resp = await connection.QueryAsync(query);
                return Ok(resp);
            }
        }

        //[HttpGet]
        //[Route("/CognomeLungo")]
        public async Task<ActionResult<DipendenteDTO>> cognomeLungo()
        {
            string query = $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(Nome) NOME, RTRIM(Cognome) COGNOME,LENGTH(TRIM(COGNOME)) AS COGNOME_LENGTH, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA, STIPENDIO " +
                $" FROM {_tableName}" +
                $" WHERE LENGTH(TRIM(COGNOME)) = (" +
                    $"SELECT MAX(COGNOME_LENGTH) " +
                    $"FROM (" +
                        $"SELECT ID_DIPENDENTE, LENGTH(TRIM(COGNOME)) AS COGNOME_LENGTH " +
                        $"FROM {_tableName}))";


            using (var connection = new OracleConnection (_connectionString))
            {
                var resp = await connection.QueryAsync(query); 
                return Ok(resp);
            }
        }

        //[HttpGet("/InGestione")]
        public async Task<ActionResult<DipendenteDTO>> CheckInGestione()
        {
            string query =
                $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(Nome) NOME, RTRIM(Cognome) COGNOME " +
                $"FROM {_tableName} " +
                $"WHERE ID_DIPENDENTE NOT IN (" +
                    $"SELECT ID_DIPENDENTE" +
                    $" FROM A_GESTIONE ) " 
                //+ $" AND SETTORE IS NULL"
                ;
            using (var conn=new OracleConnection(_connectionString))
            {
                var resp =await conn.QueryAsync(query);
                return Ok(resp);
            }
        }

        private bool DipendenteExists(string id)
        {
            //return (_context.Dipendente?.Any(e => e.Id_dipendente == id)).GetValueOrDefault();
            string find = $"SELECT * FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE";
            using (var connection = new OracleConnection(_connectionString))
            {
                var dip = connection.Query<DipendenteDTO>(find, new { ID_DIPENDENTE = id });
                if (dip.Count()==0)
                { 
                    return false; 
                }
                else {
                    return true; 
                }
            }
        }
/*
        private static DipendenteDTO ItemToDTO(Dipendente dipendente)
        {
            return new DipendenteDTO
            { 
               Id_dipendente = dipendente.Id_dipendente,
               Cognome = dipendente.Cognome,
               Nome = dipendente.Nome,  
               Settore = dipendente.Settore,
               Stipendio = dipendente.Stipendio
            };
        }
*/
    }
}
