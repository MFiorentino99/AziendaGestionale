using AziendaGestionale.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;
using Test.Repositories;
using Oracle.ManagedDataAccess.Client;
using Test.Abstractions;
using Test.InterfacesRepository;


namespace AziendaGestionale.Controllers
{
    [Route("Prodotti")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly IProdottiQueries _prodottiQueries;
        private readonly IProdottiRepository _prodottiRepository;
        public ProdottiController(IProdottiQueries prodottiQueries,IProdottiRepository prodottiRepository)
        {
            _prodottiQueries = prodottiQueries;
            _prodottiRepository = prodottiRepository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DTOProdotto>> GetAll()
        {
            return Ok(_prodottiQueries.GetAll().Result);
        }
        
        [HttpGet("{nome}")]
        public ActionResult<DTOProdotto> GetById(string nome)
        {
            var response = _prodottiQueries.GetProdottoByID(nome).Result;
            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound("Elemento non presente");
            }
        }

        [HttpPost]
        public ActionResult Post(DTOProdotto element)
        {
            var response = _prodottiRepository.CreateProdotto(element);
            if (response )
            {
                return Ok("Inserimento eseguito con successo");
            }
            else
            {
                return BadRequest("Inseirmento fallito");
            }
        }

        [HttpPut("{nome}")]
        public ActionResult Put(string nome, DTOProdotto element)
        {
            bool resposne = _prodottiRepository.UpdateProdottoById(nome,element);
            if (resposne)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggiornamento fallito");
            }
        }

        // DELETE api/<ProdottiController>/5
        [HttpDelete("{nome}")]
        public ActionResult Delete(string nome)
        {
            bool response = _prodottiRepository.DeleteProdototById(nome);
            if(response)
            {
                return Ok("Cancellazione eseguita con successo");
            }
            else
            {
                return BadRequest("Cancellazione fallita");
            }
        }


    }
}
