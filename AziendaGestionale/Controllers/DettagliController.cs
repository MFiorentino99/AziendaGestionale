using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Abstractions;
using Test.InterfacesRepository;
using Test.Models;

namespace AziendaGestionale.Controllers
{
    [Route("Dettagli")]
    [ApiController]
    public class DettagliController : ControllerBase
    {
        private readonly IDettagliQueries _dettagliQueries;
        private readonly IDettagliRepository _dettagliRepository;

        public DettagliController(IDettagliQueries dettagliQueries, IDettagliRepository dettagliRepository)
        {
            _dettagliQueries = dettagliQueries;
            _dettagliRepository = dettagliRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTODettaglio>> GetAll()
        {
            return Ok(_dettagliQueries.GetAll().Result);
        }

        [HttpGet("{fattura}-{prodotto}")]
        public ActionResult<DTODettaglio> GetDettaglioById(string fattura, string prodotto)
        {
            var resp = _dettagliQueries.GetDettaglioById(fattura, prodotto).Result;
            if (resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Dettagloi non trovato");
            }
        }

        [HttpPut("{fattura}-{prodotto}")]
        public ActionResult UpdateDettaglioById(string fattura, string prodotto, DTODettaglio dettaglio)
        {
            var resp = _dettagliRepository.UpdateDettaglioById(fattura, prodotto, dettaglio);
            if (resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggiornamento fallito");
            }
        }
        [HttpPost]
        public ActionResult CreateDettaglio(DTODettaglio dettaglio)
        {
            var resp = _dettagliRepository.CreateDettaglio(dettaglio);
            if (resp)
            {
                return Ok("Aggiunta avvenuta con successo");
            }
            else
            {
                return BadRequest("Aggiunta Dettaglio fallita");
            }
        }

        [HttpDelete("{fattura}-{prodotto}")]
        public ActionResult DeleteDettaglioById(string fattura, string prodotto)
        {
            var resp = _dettagliRepository.DeleteDettaglioById(fattura,prodotto);
            if (resp)
            {
                return Ok("Eliminazione avvenuta con successo");
            }
            else
            {
                return BadRequest("Eliminazione fallita");
            }
        }
    }
}
