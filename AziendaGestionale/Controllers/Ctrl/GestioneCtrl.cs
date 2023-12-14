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
using System.Globalization;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace AziendaGestionale.Controllers.Ctrl
{
    // [Route("api/GestioneCtrl")]
    // [ApiController]
    public class GestioneCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";
        private readonly string _tableName = "A_GESTIONE";

        public GestioneCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/GestioneCtrl
        // [HttpGet]
        public async Task<ActionResult<IEnumerable<GestioneDTO>>> GetGestione()
        {
            string query = $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, DATA_ASSEGNAZIONE, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA" +
                $" FROM {_tableName}";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<GestioneDTO>(query);
                if (resp.Count() > 0)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest("Non ci sono elementi nel DB");
                }

            }
            /*
          if (_context.Gestione == null)
          {
              return NotFound();
          }
            return await _context.Gestione.Select(g => ItemToDTO(g)).ToListAsync();
            */
        }

        // GET: api/GestioneCtrl/5
        //[HttpGet("{id}/{data}")]
        public async Task<ActionResult<GestioneDTO>> GetGestione(string id, string data)
        {
            string query = $"SELECT RTRIM(ID_DIPENDENTE) ID_DIPENDENTE, DATA_ASSEGNAZIONE, RTRIM(SETTORE) SETTORE, RTRIM(CATEGORIA) CATEGORIA " +
                $"FROM {_tableName} WHERE ID_DIPENDENTE=:ID_DIPENDENTE AND RTRIM(DATA_ASSEGNAZIONE)=:DATA_ASSEGNAZIONE";
            DateTime d = DateConvert(data);
            using (var conn = new OracleConnection(_connectionString))
            {
                if (GestioneExists(id, d))
                {
                    var resp = await conn.QuerySingleAsync<GestioneDTO>(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d
                    });
                    return Ok(resp);
                }
                else
                {
                    return NotFound("Non trovato");
                }
            }
            /*
          if (_context.Gestione == null)
          {
              return NotFound();
          }

            var data_assegnazione = DateConvert(data);
            var gestione = await _context.Gestione.FindAsync(id,data_assegnazione);

            if (gestione == null)
            {
                return NotFound();
            }

            return ItemToDTO(gestione);
            */
        }

        // PUT: api/GestioneCtrl/5
        //[HttpPut("{id}/{data}")]
        public async Task<IActionResult> PutGestione(string id, string data, GestioneDTO gestioneDTO)
        {
            DateTime d = DateConvert(data);
            string query = $"UPDATE {_tableName} SET " +
                $"SETTORE=:SETTORE, CATEGORIA=:CATEGORIA " +
                $"WHERE RTRIM(ID_DIPENDENTE)=:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (GestioneExists(id, d))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d,
                        SETTORE = gestioneDTO.Settore,
                        CATEGORIA = gestioneDTO.Categoria
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }
                else
                {
                    return NotFound("Elemento non trovato");
                }
            }

            /*
            if (id != gestioneDTO.Id_dipendente && data != gestioneDTO.Data_assegnazione.ToString() )
            {
                return BadRequest();
            }

            var data_assegnazione = DateConvert(data);
            var g = await _context.Gestione.FindAsync(id, data_assegnazione);

            if(g == null)
            {
                return NotFound();
            }

            g.Id_dipendente = id;
            g.Settore = gestioneDTO.Settore;
            g.Data_assegnazione = data_assegnazione;
            g.Categoria = gestioneDTO.Categoria;

            _context.Entry(g).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GestioneExists(id,data_assegnazione))
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

        // POST: api/GestioneCtrl
        //[HttpPost]
        public async Task<ActionResult<Gestione>> PostGestione(GestioneDTO gestioneDTO)
        {
            string query = $"INSERT INTO {_tableName} " +
                $"(ID_DIPENDENTE, DATA_ASSEGNAZIONE, SETTORE, CATEGORIA) " +
                $"VALUES (:ID_DIPENDENTE, :DATA_ASSEGNAZIONE, :SETTORE, :CATEGORIA)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (!GestioneExists(gestioneDTO.Id_dipendente, gestioneDTO.Data_assegnazione))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = gestioneDTO.Id_dipendente,
                        DATA_ASSEGNAZIONE = gestioneDTO.Data_assegnazione,
                        SETTORE = gestioneDTO.Settore,
                        CATEGORIA = gestioneDTO.Categoria
                    });
                    return Ok("Aggiunta eseguita con successo");
                }
                else { return BadRequest("Elemento già presente"); }
            }


            /*
          if (_context.Gestione == null)
          {
              return Problem("Entity set 'Context.Gestione'  is null.");
          }
          
          var gestione = new Gestione
          {
              Id_dipendente = gestioneDTO.Id_dipendente,
              Data_assegnazione = gestioneDTO.Data_assegnazione,
              Categoria = gestioneDTO.Categoria,
              Settore = gestioneDTO.Settore
          };

            _context.Gestione.Add(gestione);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GestioneExists(gestione.Id_dipendente,gestione.Data_assegnazione))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGestione", new { id = gestione.Id_dipendente , data_assegnazione = gestione.Data_assegnazione}, gestioneDTO);
            */
        }

        // DELETE: api/GestioneCtrl/5
        //[HttpDelete("{id}/{data}")]
        public async Task<IActionResult> DeleteGestione(string id, string data)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE) =:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            DateTime d = DateConvert(data);
            using (var conn = new OracleConnection(_connectionString))
            {
                if (GestioneExists(id, d))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_DIPENDENTE = id,
                        DATA_ASSEGNAZIONE = d
                    });
                    return Ok("Cancellazione eseguita con successo");
                }
                else
                {
                    return NotFound("Elemento non trovato");
                }
            }
            /*
            if (_context.Gestione == null)
            {
                return NotFound();
            }

            DateOnly data_assegnazione = DateConvert(data);
            var gestione = await _context.Gestione.FindAsync(id,data_assegnazione);
            if (gestione == null)
            {
                return NotFound();
            }

            _context.Gestione.Remove(gestione);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        // [HttpPut("/CategoriaInMinuscolo")]
        public async Task<ActionResult> CategoriaToLowerCase()
        {
            string query = $"UPDATE {_tableName} SET SETTORE = LOWER(TRIM(SETTORE))," +
                $" CATEGORIA = LOWER(TRIM(CATEGORIA))";
            using (var conn = new OracleConnection(_connectionString))
            {
                var res = await conn.ExecuteAsync(query);
                if (res > 0)
                {
                    return Ok("Aggioramento eseguito con successo");
                }
                else
                {
                    return BadRequest("Aggiornamento fallito");
                }

            }
        }
        // [HttpGet("/IncaricoADataFattura")]
        public async Task<ActionResult> RoleInInvoiceDate()
        {
            string query = $"SELECT DISTINCT RTRIM(T.ID_DIPENDENTE) ID_DIPENDENTE, RTRIM(G.SETTORE) SETTORE," +
                $"RTRIM(G.CATEGORIA) CATEGORIA,T.DATA_RECENTE," +
                $"RTRIM(T.ID_FATTURA) ID_FATTURA, RTRIM(T.DATA_VENDITA) DATA_VENDITA " +
                $"FROM (" +
                    $"SELECT G.ID_DIPENDENTE,MAX(G.DATA_ASSEGNAZIONE) DATA_RECENTE,F.DATA_VENDITA, F.ID_FATTURA " +
                    $"FROM {_tableName} G " +
                    $"JOIN A_FATTURA F ON G.ID_DIPENDENTE=F.ID_VENDITORE " +
                    $"WHERE F.DATA_VENDITA >= G.DATA_ASSEGNAZIONE " +
                    $"GROUP BY (G.ID_DIPENDENTE,F.DATA_VENDITA, F.ID_FATTURA) )T" +
                $" JOIN {_tableName} G ON RTRIM(G.ID_DIPENDENTE)=RTRIM(T.ID_DIPENDENTE) " +
                $"WHERE T.DATA_RECENTE = G.DATA_ASSEGNAZIONE " +
                $"ORDER BY ID_DIPENDENTE ASC, DATA_VENDITA DESC";
            using (var conn = new OracleConnection(_connectionString))
            {
                var res = await conn.QueryAsync(query);
                return Ok(res);
            }
        }
        private bool GestioneExists(string id, DateTime data)
        {
            //return (_context.Gestione?.Any(e => e.Id_dipendente == id && e.Data_assegnazione == date)).GetValueOrDefault();
            string query = $"SELECT * FROM {_tableName} WHERE RTRIM(ID_DIPENDENTE)=:ID_DIPENDENTE AND DATA_ASSEGNAZIONE=:DATA_ASSEGNAZIONE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Query<GestioneDTO>(query, new
                {
                    ID_DIPENDENTE = id,
                    DATA_ASSEGNAZIONE = data
                });
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

        private DateTime DateConvert(string d)
        {
            DateTime today = DateTime.Today;
            try
            {
                return DateTime.ParseExact(d, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                BadRequest(e.Message);
                return DateTime.Parse("00/00/0000", CultureInfo.InvariantCulture);
            }


        }
        /*
        public static GestioneDTO ItemToDTO(Gestione g) 
        {
            return new GestioneDTO 
            { 
                Id_dipendente = g.Id_dipendente,
                Data_assegnazione = g.Data_assegnazione,
                Categoria = g.Categoria,
                Settore = g.Settore
            };
        }
        */
    }
}
