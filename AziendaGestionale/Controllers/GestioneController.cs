using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;
using Test.Repositories;
using Oracle.ManagedDataAccess.Client;
using Test.Abstractions;
using Test.Repositories;
using Test.InterfacesRepository;

namespace AziendaGestionale.Controllers
{
    [Route("Gestione")]
    [ApiController]
    public class GestioneController : ControllerBase
    {
        private readonly IGestioneQuery _gestioneQueries;
        private readonly IGestioneRepository _gestioneRepository;
        public GestioneController(IGestioneQuery queries, IGestioneRepository repository) 
        {
            _gestioneQueries = queries;
            _gestioneRepository = repository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DTOGestione>> GetAll()
        {
           return Ok(_gestioneQueries.GetAll().Result);
        }

        
        [HttpGet("{id}/{data}")]
        public ActionResult<DTOGestione> GetById(string id,string data)
        {
            var resp = _gestioneQueries.GetGestioneById(id,data).Result;
            if(resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Elemento non trovato");
            }
            
        }

        
        [HttpPost]
        public async Task<ActionResult> PostAsync(DTOGestione gestione)
        {
            bool response = await _gestioneRepository.CreateGestione(gestione);
            if (response)
            {
                return Ok("Aggiunta eseguita con successo");
            }
            else
            {
                return BadRequest("Aggiunta fallita");
            }
        }

        
        [HttpPut("{id}/{data}")]
        public async Task<ActionResult> PutAsync(string id,string data, DTOGestione gestione)
        {
            bool resp = await _gestioneRepository.UpdateGestioneById(id,data,gestione);
            if(resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggiornamento fallito");
            }
        }

        
        [HttpDelete("{id}/{data}")]
        public async Task<ActionResult> DeleteAsync(string id, string data)
        {
            bool resp = await _gestioneRepository.DeleteGestioneById(id, data);
            if (resp)
            {
                return Ok("Cancellazione eseguita con successo");
            }
            else
            {
                return BadRequest("Cancellazione Fallita");
            }
        }

        [HttpPut]
        [Route("/CategoriaInMinuscolo")]
        public async Task<ActionResult> CategoriaToLowerCaseAsync()
        {
            bool resp = await _gestioneRepository.CategoriaToLowerCase();
            if (resp)
            {
                return Ok("Operazione eseguita con successo");
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
        }

        [HttpGet("/SettoreInDataFattura")]
        public ActionResult RoleInInvoiceDate()
        {
            var resp = _gestioneQueries.RoleInInvoiceDate().Result;
            return Ok(resp);
        }
    }
}
