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

namespace AziendaGestionale.Controllers
{
    //[Route("api/FattureCtrl")]
    //[ApiController]
    public class FattureCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _tableName = "A_FATTURA";
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";

        public FattureCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/FattureCtrl
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<FatturaDTO>>> GetFattura()
        {
            string query = $"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(ID_VENDITORE) ID_VENDITORE, RTRIM(ID_CLIENTE) ID_CLIENTE, DATA_VENDITA, TOTALE" +
                $" FROM {_tableName}";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<FatturaDTO>(query);
                return Ok(resp);
            }   
            /*
          if (_context.Fattura == null)
          {
              return NotFound();
          }
            return await _context.Fattura.Select(f => ItemToDTO(f)).ToListAsync();
            */
        }

        // GET: api/FattureCtrl/5
        //[HttpGet("{id}/{data}")]
        public async Task<ActionResult<FatturaDTO>> GetFattura(string id, string data)
        {
            string query = $"SELECT RTRIM(ID_FATTURA) ID_FATTURA, RTRIM(ID_VENDITORE) ID_VENDITORE, RTRIM(ID_CLIENTE) ID_CLIENTE, DATA_VENDITA, TOTALE" +
                $" FROM {_tableName} WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA = :DATA_VENDITA ";
            DateTime d = DataConverter(data);
            using(var conn = new OracleConnection(_connectionString))
            {
             if(FatturaExists(id,d))
                {
                    var resp = await conn.QuerySingleAsync<FatturaDTO>(query, new
                    {
                        ID_FATTURA = id,
                        DATA_VENDITA = d
                    });
                    return Ok(resp);
                }  
                else
                {
                    return NotFound("Fattura non trovata");
                }
            }
            /*
          if (_context.Fattura == null)
          {
              return NotFound();
          }
            var data_fattura = DataConverter(data);
            var fattura = await _context.Fattura.FindAsync(id,data_fattura);

            if (fattura == null)
            {
                return NotFound();
            }

            return ItemToDTO(fattura);
            */
        }

        // PUT: api/FattureCtrl/5
        //[HttpPut("{id}/{data}")]
        public async Task<IActionResult> PutFattura(string id,string data, FatturaDTO fatturaDTO)
        {
            DateTime d = DataConverter(data);
            string query = $"UPDATE {_tableName} SET " +
                $" ID_VENDITORE =:ID_VENDITORE, ID_CLIENTE =:ID_CLIENTE, TOTALE =:TOTALE " +
                $" WHERE RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA = :DATA_VENDITA";
            using(var conn = new OracleConnection(_connectionString))
            {
                if (FatturaExists(id, d))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = id,
                        DATA_VENDITA = d,
                        ID_VENDITORE = fatturaDTO.Id_venditore,
                        ID_CLIENTE = fatturaDTO.Id_cliente,
                        TOTALE = fatturaDTO.Totale
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }
                else { 
                    return NotFound("Fattura non trovata");
                }
            }
            /*
            if (id != fatturaDTO.Id_fattura)
            {
                return BadRequest();
            }
            var data_fattura = DataConverter(data);
            var fattura = await _context.Fattura.FindAsync(id,data_fattura);
            if (fattura == null)
            {
                return NotFound();
            }

            fattura.Id_fattura=id;
            fattura.Data_vendita = data_fattura;
            fattura.Id_venditore = fatturaDTO.Id_venditore;
            fattura.Id_cliente = fatturaDTO.Id_cliente;
            fattura.Totale = fatturaDTO.Totale;

            _context.Entry(fattura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FatturaExists(id,data_fattura))
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

        // POST: api/FattureCtrl
        //[HttpPost]
        public async Task<ActionResult<Fattura>> PostFattura(FatturaDTO fatturaDTO)
        {
            
            string query = $"INSERT INTO {_tableName} " +
                $"(ID_FATTURA, DATA_VENDITA, ID_VENDITORE, ID_CLIENTE, TOTALE) " +
                $"VALUES (:ID_FATTURA, :DATA_VENDITA, :ID_VENDITORE, :ID_CLIENTE, :TOTALE )";
            using(var conn = new OracleConnection(_connectionString))
            {
                if(!FatturaExists(fatturaDTO.Id_fattura,fatturaDTO.Data_vendita))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = fatturaDTO.Id_fattura,
                        DATA_VENDITA = fatturaDTO.Data_vendita,
                        ID_VENDITORE = fatturaDTO.Id_venditore,
                        ID_CLIENTE = fatturaDTO.Id_cliente,
                        TOTALE = fatturaDTO.Totale
                    });
                    return Ok("Inswrimento eseguito con sufìccesso");
                }else
                {
                    return BadRequest();
                }
               
            }
            /*
          if (_context.Fattura == null)
          {
              return Problem("Entity set 'Context.Fattura'  is null.");
          }

            var fattura = new Fattura
            {
                Id_fattura = fatturaDTO.Id_fattura,
                Data_vendita = fatturaDTO.Data_vendita,
                Id_cliente = fatturaDTO.Id_cliente,
                Id_venditore = fatturaDTO.Id_venditore,
                Totale = fatturaDTO.Totale
            };
            _context.Fattura.Add(fattura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FatturaExists(fattura.Id_fattura, fattura.Data_vendita))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFattura", new { id = fattura.Id_fattura, data_fattura = fattura.Data_vendita }, fattura);
            */
        }

        // DELETE: api/FattureCtrl/5
        //[HttpDelete("{id}/{data}")]
        public async Task<IActionResult> DeleteFattura(string id,string data)
        {
            string query = $"DELETE FROM {_tableName} WHERE" +
                $" RTRIM(ID_FATTURA)=:ID_FATTURA AND DATA_VENDITA=:DATA_VENDITA ";
            DateTime d = DataConverter(data);
            using (var conn = new OracleConnection(_connectionString))
            {
                if (FatturaExists(id, d))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_FATTURA = id,
                        DATA_VENDITA = d
                    });
                    return Ok("Cancellazione eseguita con successo");
                }
                else
                {
                    return NotFound("Fattura non trovata");
                }
            }
            /*
            if (_context.Fattura == null)
            {
                return NotFound();
            }
            DateOnly data_fattura = DataConverter(data);
            var fattura = await _context.Fattura.FindAsync(id,data_fattura);
            if (fattura == null)
            {
                return NotFound();
            }

            _context.Fattura.Remove(fattura);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        //[HttpPut("/AggiornaTOT")]
        public async Task<ActionResult> UpdateTOT()
        {
            string query = $"UPDATE a_fattura SET a_fattura.totale = (" +
                $"SELECT SUM(d.costo*d.quantity) " +
                $"FROM a_dettaglio d " +
                $"WHERE a_fattura.id_fattura = d.id_fattura)";
            using (var connection = new OracleConnection(_connectionString))
            {
                var resp = await connection.ExecuteAsync(query);
                return Ok(resp);
            }
        }

       // [HttpGet("/IncassoAnnuale")]
        public async Task<ActionResult> AnnualIncome()
        {
            string query = $"SELECT EXTRACT(YEAR FROM DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO " +
                $"FROM {_tableName} " +
                $"GROUP BY (EXTRACT(YEAR FROM DATA_VENDITA))";
            using (var connection = new OracleConnection(_connectionString))
            {
                var res = await connection.QueryAsync(query);
                return Ok(res);
            }
        }

      //  [HttpGet("/IncassoAnnualePerCliente")]
        public async Task<ActionResult> AnnualIncomePerClient()
        {
            string query = $"SELECT ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO_CLIENTE " +
                $"FROM {_tableName} " +
                $"GROUP BY ROLLUP(ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA))" +
                $"ORDER BY ID_CLIENTE,EXTRACT(YEAR FROM DATA_VENDITA) DESC";
            using (var connection = new OracleConnection(_connectionString))
            {
                var res = await connection.QueryAsync(query);
                return Ok(res);
            }
        }

      //  [HttpGet("/DettagliIncassoAnnualePerCliente")]
        public async Task<ActionResult> AnnualIncomePerClientDetails()
        {
            string query = $"SELECT TRIM(F.ID_CLIENTE) ID_CLIENTE,TRIM(C.NOME) AS NOME,TRIM(C.COGNOME) AS COGNOME,EXTRACT(YEAR FROM F.DATA_VENDITA) AS ANNO, SUM(TOTALE) AS TOTALE_ANNUO_CLIENTE " +
                $"FROM {_tableName} F " +
                $"INNER JOIN A_CLIENTE C ON C.ID_CLIENTE = F.ID_CLIENTE " +
                $"GROUP BY ROLLUP(F.ID_CLIENTE,EXTRACT(YEAR FROM F.DATA_VENDITA), C.NOME,C.COGNOME) " +
                $"ORDER BY F.ID_CLIENTE,EXTRACT(YEAR FROM F.DATA_VENDITA) DESC ";
            using (var connection = new OracleConnection(_connectionString))
            {
                var res = await connection.QueryAsync(query);
                return Ok(res);
            }
        }

        private bool FatturaExists(string id, DateTime d)
        {
            //return (_context.Fattura?.Any(e => e.Id_fattura == id && e.Data_vendita == d)).GetValueOrDefault();
            string find = $"SELECT * FROM {_tableName} WHERE " +
                $"RTRIM(ID_FATTURA) =:ID_FATTURA AND DATA_VENDITA =:DATA_VENDITA";
            using (var conn  = new OracleConnection(_connectionString))
            {
                var resp = conn.Query<FatturaDTO>(find, new
                {
                    ID_FATTURA = id,
                    DATA_VENDITA = d
                });
                if (resp.Count() != 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }

        private DateTime DataConverter(string data)
        {

            DateTime d = DateTime.Today;
            try
            {
                return DateTime.ParseExact(data,"dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException e){ BadRequest(e.Message); }
            return d;
        }
        /*
 1       public static FatturaDTO ItemToDTO(Fattura fattura)
        {
            return new FatturaDTO
            {
                Id_fattura = fattura.Id_fattura,
                Data_vendita = fattura.Data_vendita,    
                Id_cliente = fattura.Id_cliente,    
                Id_venditore = fattura.Id_venditore,
                Totale = fattura.Totale
            };
        }
        */
    }
}
