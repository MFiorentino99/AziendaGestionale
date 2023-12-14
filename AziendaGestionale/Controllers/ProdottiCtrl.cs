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
    /*
    [Route("api/ProdottiCtrl")]
    [ApiController]
    */
    public class ProdottiCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _tableName = "A_PRODOTTO";
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";
        public ProdottiCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/ProdottiCtrl
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<ProdottoDTO>>> GetProdotto()
        {
            string query = $"SELECT RTRIM(NOME) NOME, RTRIM(CATEGORIA) CATEGORIA, COSTO_PRODUZIONE" +
                $" FROM {_tableName}";
            using(var conn = new OracleConnection(_connectionString))
            {
                var resp =await conn.QueryAsync<ProdottoDTO>(query);
                return Ok(resp);
            }
            /*
          if (_context.Prodotto == null)
          {
              return NotFound();
          }
            return await _context.Prodotto.Select(p=>ItemToDTO(p)).ToListAsync();
            */
        }

       

        // GET: api/ProdottiCtrl/5
        //[HttpGet("{nome}")]
        public async Task<ActionResult<ProdottoDTO>> GetProdotto(string nome)
        {
            string find = $"SELECT RTRIM(NOME) NOME, RTRIM(CATEGORIA) CATEGORIA, COSTO_PRODUZIONE" +
                $" FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if(ProdottoExists(nome))
                {
                    var resp = await conn.QuerySingleAsync<ProdottoDTO>(find, new { NOME = nome });
                    return Ok(resp);

                }
                else
                {
                    return NotFound("Prodotto non trovato");
                }
            }

            /*
          if (_context.Prodotto == null)
          {
              return NotFound();
          }
            var prodotto = await _context.Prodotto.FindAsync(nome);

            if (prodotto == null)
            {
                return NotFound();
            }

            return ItemToDTO(prodotto);
            */
        }

        // PUT: api/ProdottiCtrl/5
        //[HttpPut("{nome}")]
        public async Task<IActionResult> PutProdotto(string nome, ProdottoDTO prodottoDTO)
        {
            string query = $"UPDATE {_tableName} SET CATEGORIA=:CATEGORIA, COSTO_PRODUZIONE=:COSTO " +
                $"WHERE RTRIM(NOME)=LOWER(:NOME)";
            using(var conn = new OracleConnection(_connectionString))
            {
                if (ProdottoExists(nome))
                {
                    await conn.ExecuteAsync(query, new { 
                        NOME = nome,
                        CATEGORIA = prodottoDTO.Categoria,
                        COSTO = prodottoDTO.Costo_produzione
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }
                else
                {
                    return NotFound("Prodotto non trovato");
                }
            }

            /*
            if (nome != prodottoDTO.Nome)
            {
                return BadRequest();
            }

            var prodotto =  await _context.Prodotto.FindAsync(nome);
            if(prodotto == null)
            {
                return NotFound();
            }
            
            _context.Entry(prodottoDTO).State = EntityState.Modified;

            prodotto.Categoria = prodottoDTO.Categoria;
            prodotto.Costo_produzione = prodottoDTO.Costo_produzione;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdottoExists(nome))
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

        // POST: api/ProdottiCtrl
        //[HttpPost]
        public async Task<ActionResult<ProdottoDTO>> PostProdotto(ProdottoDTO prodottoDTO)
        {
            string query = $"INSERT INTO {_tableName} " +
                $"(NOME,CATEGORIA,COSTO_PRODUZIONE) " +
                $"VALUES (LOWER(:NOME), :CATEGORIA, :COSTO)";
            using(var conn = new OracleConnection(_connectionString))
            {
                if (!ProdottoExists(prodottoDTO.Nome))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        NOME = prodottoDTO.Nome,
                        CATEGORIA = prodottoDTO.Categoria,
                        COSTO = prodottoDTO.Costo_produzione
                    });
                    return Ok("Inserimentoeseguito con successo");
                }
                else
                {
                    return BadRequest("Prodotto già presente");
                }
            }
            /*
            var prodotto = new Prodotto
            {
                Nome = prodottoDTO.Nome,
                Costo_produzione = prodottoDTO.Costo_produzione,
                Categoria = prodottoDTO.Categoria
            };

          if (_context.Prodotto == null)
          {
              return Problem("Entity set 'Context.Prodotto'  is null.");
          }
            _context.Prodotto.Add(prodotto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdottoExists(prodotto.Nome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProdotto", new { nome = prodotto.Nome }, ItemToDTO(prodotto));
            */
        }

        // DELETE: api/ProdottiCtrl/5
        //[HttpDelete("{nome}")]
        public async Task<IActionResult> DeleteProdotto(string nome)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ProdottoExists(nome))
                {
                    await conn.ExecuteAsync(query, new { NOME = nome });
                    return Ok("Cancellazione eseguita con successo");
                }
                else
                {
                    return NotFound("Prodottto non trovato");
                }
            }
            /*
            if (_context.Prodotto == null)
            {
                return NotFound();
            }
            var prodotto = await _context.Prodotto.FindAsync(nome);
            if (prodotto == null)
            {
                return NotFound();
            }

            _context.Prodotto.Remove(prodotto);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        private bool ProdottoExists(string id)
        {
            //return (_context.Prodotto?.Any(e => e.Nome == nome)).GetValueOrDefault();

            string find = $"SELECT * FROM {_tableName} WHERE RTRIM(NOME)=LOWER(:NOME)";
            using(var conn = new OracleConnection(_connectionString))
            { 
                var resp = conn.Query<ProdottoDTO>(find, new { NOME = id});
                if(resp.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*
        public static ProdottoDTO ItemToDTO(Prodotto p)
        {
           return new ProdottoDTO
            {
                Nome = p.Nome,
                Categoria = p.Categoria,
                Costo_produzione = p.Costo_produzione
            };
        }
        */
    }
}
