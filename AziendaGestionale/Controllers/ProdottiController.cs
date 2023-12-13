using AziendaGestionale.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;



namespace AziendaGestionale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly ProdottiQueries _prodottiQueries;
        public ProdottiController(ProdottiQueries prodottiQueries)
        {
            _prodottiQueries = prodottiQueries;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<DTOProdotto>> GetAll()
        {
            return Ok(_prodottiQueries.GetAll());
        }
        
        [HttpGet("{nome}")]
        public ActionResult<DTOProdotto> GetById(string id)
        {
            var response = _prodottiQueries.GetOneByID(id);
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
            var response = _prodottiQueries.CreateElement(element);
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
            bool resposne = _prodottiQueries.UpdateOneById(nome, element);
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
            bool response = _prodottiQueries.DeleteOneById(nome);
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
