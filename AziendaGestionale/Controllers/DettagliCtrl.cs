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
using Dapper;

namespace AziendaGestionale.Controllers
{
    [Route("api/DettagliCtrl")]
    [ApiController]
    public class DettagliCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";
        private readonly string _tableName = "A_DETTAGLIO";
        public DettagliCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/DettagliCtrl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DettaglioDTO>>> GetDettaglio()
        {
            string query = $"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(PRODOTTO) PRODOTTO, QUANTITY AS QUANTITA, COSTO" +
                $" FROM {_tableName}";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<DettaglioDTO>(query);
                return Ok(resp);
            }
            /*
          if (_context.Dettaglio == null)
          {
              return NotFound();
          }
            return await _context.Dettaglio.Select(d => ItemToDTO(d)).ToListAsync();
            */
        }

        // GET: api/DettagliCtrl/5
        [HttpGet("{id}-{prodotto}")]
        public async Task<ActionResult<DettaglioDTO>> GetDettaglio(string id, string prodotto)
        {
            string query = $" SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(PRODOTTO) PRODOTTO, QUANTITY AS QUANTITA, COSTO FROM {_tableName} " +
                $"WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO) = LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (DettaglioExists(id, prodotto))
                {
                    var resp = await conn.QuerySingleAsync<DettaglioDTO>(query, new
                    {
                        ID_FATTURA = id,
                        PRODOTTO = prodotto
                    });
                    return Ok(resp);
                }
                else
                {
                    return NotFound("Dettaglio non trovato");
                }
               
            }

            /*
          if (_context.Dettaglio == null)
          {
              return NotFound();
          }
            var dettaglio = await _context.Dettaglio.FindAsync(id,prodotto);

            if (dettaglio == null)
            {
                return NotFound();
            }

            return ItemToDTO(dettaglio);
            */
        }

        // PUT: api/DettagliCtrl/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{prodotto}")]
        public async Task<IActionResult> PutDettaglio(string id, string prodotto, DettaglioDTO dettaglioDTO)
        {
            string query = $"UPDATE {_tableName} SET " +
                $" COSTO =:COSTO, QUANTITY  =:QUANTITA "+
                $"WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO) =LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (DettaglioExists(id, prodotto))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = id,
                        PRODOTTO = prodotto,
                        COSTO = dettaglioDTO.Costo,
                        QUANTITA = dettaglioDTO.Quantita
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }
                else
                {
                    return NotFound("Dettaglio non esistente");
                }
            }
            /*
            if (id != dettaglioDTO.Id_fattura && prodotto != dettaglioDTO.Prodotto)
            {
                return BadRequest();
            }
            var dettaglio = await _context.Dettaglio.FindAsync(id, prodotto);
            _context.Entry(dettaglio).State = EntityState.Modified;

            dettaglio.Id_fattura = id;
            dettaglio.Prodotto = prodotto;
            dettaglio.Quantita = dettaglioDTO.Quantita;
            dettaglio.Costo = dettaglioDTO.Costo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DettaglioExists(id,prodotto))
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

        // POST: api/DettagliCtrl
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DettaglioDTO>> PostDettaglio(DettaglioDTO dettaglioDTO)
        {
            string query = $"INSERT INTO {_tableName} " +
                $"(ID_FATTURA, PRODOTTO, COSTO, QUANTITY ) " +
                $"VALUES (:ID_FATTURA, LOWER(:PRODOTTO), :COSTO, :QUANTITA)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (!DettaglioExists(dettaglioDTO.Id_fattura, dettaglioDTO.Prodotto))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = dettaglioDTO.Id_fattura,
                        PRODOTTO = dettaglioDTO.Prodotto,
                        COSTO = dettaglioDTO.Costo,
                        QUANTITA = dettaglioDTO.Quantita
                    });
                    return Ok("inserimento eseguito con successo");
                }
                else
                {
                    return BadRequest("Dettaglio già esistente");
                }

            }

            /*
          if (_context.Dettaglio == null)
          {
              return Problem("Entity set 'Context.Dettaglio'  is null.");
          }

            var dettaglio = new Dettaglio
            {
                Prodotto = dettaglioDTO.Prodotto,
                Costo = dettaglioDTO.Costo,
                Quantita = dettaglioDTO.Quantita,   
                Id_fattura = dettaglioDTO.Id_fattura
            };
            _context.Dettaglio.Add(dettaglio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DettaglioExists(dettaglio.Id_fattura,dettaglio.Prodotto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDettaglio", new { id = dettaglio.Id_fattura, prodotto = dettaglio.Prodotto }, dettaglioDTO);
            */
        }

        // DELETE: api/DettagliCtrl/5
        [HttpDelete("{id}-{prodotto}")]
        public async Task<IActionResult> DeleteDettaglio(string id, string prodotto)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO)=LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (DettaglioExists(id, prodotto))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = id,
                        PRODOTTO = prodotto
                    });
                    return Ok("Cancellazione eseguita con successo");
                }
                else
                {
                    return BadRequest();
                }
            }
            /*
            if (_context.Dettaglio == null)
            {
                return NotFound();
            }
            var dettaglio = await _context.Dettaglio.FindAsync(id,prodotto);
            if (dettaglio == null)
            {
                return NotFound();
            }

            _context.Dettaglio.Remove(dettaglio);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        private bool DettaglioExists(string id, string prod)
        {
            //return (_context.Dettaglio?.Any(e => e.Id_fattura == id && e.Prodotto == prodotto)).GetValueOrDefault();
            string find = $"SELECT * FROM {_tableName} WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND RTRIM(PRODOTTO) =LOWER(:PRODOTTO)";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Query<DettaglioDTO>(find, new
                {
                    ID_FATTURA = id,
                    PRODOTTO = prod
                });
                 if(resp.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;   
                }
            }
            /*
            private static DettaglioDTO ItemToDTO(Dettaglio d)
            {
                return new DettaglioDTO
                {
                    Id_fattura = d.Id_fattura,
                    Costo = d.Costo,
                    Prodotto = d.Prodotto,
                    Quantita = d.Quantita
                };
            }
            */
        }
    }
}
