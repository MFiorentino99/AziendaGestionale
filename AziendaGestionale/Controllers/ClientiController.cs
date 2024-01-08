using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.InterfacesRepository;
using Test.Abstractions;

namespace AziendaGestionale.Controllers
{
    [Route("Clienti")]
    [ApiController]
    public class ClientiController : ControllerBase
    {
        private readonly IClientiQueries _clientiQueries;
        private readonly IClientiRepository _clientiRepository;
        public ClientiController(IClientiQueries clientiQueries, IClientiRepository clientiRepository)
        {
            _clientiQueries = clientiQueries;
            _clientiRepository = clientiRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DTOCliente>> GetAll()
        {
            return Ok(_clientiQueries.GetAll().Result);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOCliente> GetClienteById(string id)
        {
            var resp = _clientiQueries.GetById(id).Result;
            if(resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Cliente non trovato");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCliente(DTOCliente cliente)
        {
            bool resp = await _clientiRepository.CreateCliente(cliente);
            if(resp)
            {
                return Ok("Aggiunta eseguita con successo");
            }
            else
            {
                return BadRequest("Aggiunta fallita");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClienteById(string id,DTOCliente cliente)
        {
            bool resp =await _clientiRepository.UpdateClienteById(id, cliente);
            if(resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggiornamento fallito");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClienteById(string id)
        {
            bool resp =await _clientiRepository.DeleteClienteById(id);
            if( resp)
            {
                return Ok("Eliminazione eseguita con successo");
            }
            else
            {
                return BadRequest("Eliinazione fallita");
            }
        }
    }
}
