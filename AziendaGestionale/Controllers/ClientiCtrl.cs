using AziendaGestionale.Models;
using AziendaGestionale.Models.DTO;
using AziendaGestionale.Models.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace AziendaGestionale.Controllers
{
    [Route("api/ClientiCtrl")]
    [ApiController]
    public class ClientiCtrl : ControllerBase
    {
        private readonly Context _context;
        private readonly string _tableName = "A_CLIENTE";
        private readonly string _connectionString = "User Id=ITS_TEST;Password=ZexpDEV2224;Data Source=localhost:1521/xe";

        public ClientiCtrl(Context context)
        {
            _context = context;
        }

        // GET: api/ClientiCtrl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetCliente()
        {
            string query = $"SELECT RTRIM(ID_CLIENTE) ID_CLIENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(CITTA) CITTA" +
                $" FROM {_tableName}";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = await conn.QueryAsync<ClienteDTO>(query);
                return Ok(resp);
            }

          /*
           * if (_context.Cliente == null)
          {
              return NotFound();
          }
            return await _context.Cliente.Select(c => ItemToDTO(c)).ToListAsync();
          */
        }

        // GET: api/ClientiCtrl/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(string id)
        {
            string query = $"SELECT RTRIM(ID_CLIENTE) ID_CLIENTE, RTRIM(NOME) NOME, RTRIM(COGNOME) COGNOME, RTRIM(CITTA) CITTA" +
                $" FROM {_tableName} WHERE RTRIM(ID_CLIENTE) =:ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                if(ClienteExists(id))
                {
                    var resp = await conn.QuerySingleAsync<ClienteDTO>(query, new { ID_CLIENTE = id });
                    return Ok(resp);
                }
                else
                {
                    return NotFound("Cliente non trovato");
                }
            }


            /*
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return ItemToDTO(cliente);
            */
        }

        // PUT: api/ClientiCtrl/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, ClienteDTO clienteDTO)
        {
            string query = $"UPDATE {_tableName} SET " +
                " NOME =:NOME, COGNOME =:COGNOME, CITTA =:CITTA " +
                "WHERE RTRIM(ID_CLIENTE)=:ID_CLIENTE";
                
            using(var  conn = new OracleConnection(_connectionString))
            {
                if (ClienteExists(id))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_CLIENTE = id,
                        NOME = clienteDTO.Nome,
                        COGNOME = clienteDTO.Cognome,
                        CITTA = clienteDTO.Citta
                    });
                    return Ok("Aggiornamento eseguito con successo");
                }
                else
                {
                    return BadRequest();
                }
            }


            /*
            if (id != clienteDTO.Id_cliente)
            {
                return BadRequest();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            
            cliente.Id_cliente = clienteDTO.Id_cliente;
            cliente.Citta = clienteDTO.Citta;
            cliente.Cognome = clienteDTO.Cognome;
            cliente.Nome = clienteDTO.Nome;

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/ClientiCtrl
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteDTO clienteDTO)
        {
            string query = $"INSERT INTO {_tableName} "+
                $"(ID_CLIENTE, NOME, COGNOME, CITTA) " +
                $"VALUES (:ID_CLIENTE, :NOME, :COGNOME, :CITTA)"; ;
            using (var conn = new OracleConnection(_connectionString))
            {
                if (!ClienteExists(clienteDTO.Id_cliente))
                {
                    await conn.ExecuteAsync(query, new
                    {
                        ID_CLIENTE = clienteDTO.Id_cliente,
                        NOME = clienteDTO.Nome,
                        COGNOME = clienteDTO.Cognome,
                        CITTA = clienteDTO.Citta
                    });
                    return Ok("Inserimento eseguito con successo");
                }
                else
                {
                    return BadRequest("Cliente già esistente");
                }
            }

            /*
            var cliente = new Cliente 
            { 
                Id_cliente = clienteDTO.Id_cliente,
                Nome = clienteDTO.Nome,
                Cognome = clienteDTO.Cognome,
                Citta = clienteDTO.Citta
            };

          if (_context.Cliente == null)
          {
              return Problem("Entity set 'Context.Cliente'  is null.");
          }
            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Id_cliente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Id_cliente }, ItemToDTO(cliente));
            */
        }

        // DELETE: api/ClientiCtrl/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            string query = $"DELETE FROM {_tableName} WHERE RTRIM(ID_CLIENTE) =:ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                if (ClienteExists(id))
                {
                    await conn.ExecuteAsync(query, new {ID_CLIENTE =id});
                    return Ok("Cancellazione eseguita con successo");
                }
                else {
                    return BadRequest();
                }
            }

            /*
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
            */
        }

        private bool ClienteExists(string id)
        {
           // return (_context.Cliente?.Any(e => e.Id_cliente == id)).GetValueOrDefault();
           string find = $"SELECT * FROM {_tableName} WHERE RTRIM(ID_CLIENTE) =:ID_CLIENTE";
            using (var conn = new OracleConnection(_connectionString))
            {
                var resp = conn.Query(find, new {ID_CLIENTE = id});
                if (resp.Count() != 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }
        /*
        private static ClienteDTO ItemToDTO(Cliente c)
        {
            return new ClienteDTO
            {
                Id_cliente = c.Id_cliente,
                Cognome = c.Cognome,
                Citta = c.Citta,
                Nome = c.Nome
            };
        }
        */
    }
}
