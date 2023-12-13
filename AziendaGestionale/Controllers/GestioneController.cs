using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;
using Test.Repositories;


namespace AziendaGestionale.Controllers
{
    [Route("api/[Gestione]")]
    [ApiController]
    public class GestioneController : ControllerBase
    {
        private readonly GestioneQueries _gestioneQueries;
        private readonly GestioneRepository _gestioneRepository;
        public GestioneController(GestioneQueries queries,GestioneRepository repository) 
        {
            _gestioneQueries = queries;
            _gestioneRepository = repository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DTOGestione>> GetAll()
        {
           return Ok(_gestioneQueries.GetAll());
        }

        
        [HttpGet("{id}-{data}")]
        public ActionResult<DTOGestione> GetById(string id,string date)
        {
            var resp = _gestioneQueries.GetGestioneById(id,date);
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
        public ActionResult Post(DTOGestione gestione)
        {
            bool response = _gestioneRepository.CreateGestione(gestione);
            if (response)
            {
                return Ok("Aggiunta eseguita con successo");
            }
            else
            {
                return BadRequest("Aggiunta fallita");
            }
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(string id,string date, DTOGestione gestione)
        {
            bool resp = _gestioneRepository.UpdateGestioneById(id,date,gestione);
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
        public ActionResult Delete(string id, string date)
        {
            bool resp = _gestioneRepository.DeleteGestioneById(id, date);
            if (resp)
            {
                return Ok("Cancellazione eseguita con successo");
            }
            else
            {
                return BadRequest("Cancellazione Fallita");
            }
        }

        [HttpPut("/CategoriaInMinuscolo")]
        public ActionResult CategoriaToLowerCase()
        {
            bool resp = _gestioneRepository.CategoriaToLowerCase();
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
            var resp = _gestioneQueries.RoleInInvoiceDate();
            return Ok(resp);
        }
    }
}
