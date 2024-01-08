using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Abstractions;
using Test.InterfacesRepository;
using Test.Models;

namespace AziendaGestionale.Controllers
{
    [Route("Dipendenti")]
    [ApiController]
    public class DipendentiConntroller : ControllerBase
    {
        private readonly IDipendentiQueries _dipendentiQueries;
        private readonly IDipendentiRepository _dipendentiRepository;

        public DipendentiConntroller(IDipendentiQueries dipendentiQueries, IDipendentiRepository dipendentiRepository)
        {
            _dipendentiQueries = dipendentiQueries;
            _dipendentiRepository = dipendentiRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTODipendente>> GetAll()
        {
            return Ok(_dipendentiQueries.GetAll().Result);
        }

        [HttpGet("{id}")]
        public ActionResult<DTODipendente> GetDipendenteById(string id)
        {
            var resp = _dipendentiQueries.GetById(id).Result;
            if(resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Dipendente non trovato");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDipendente(DTODipendente dipendente)
        {
            bool resp = await _dipendentiRepository.CreateDipendente(dipendente);
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
        public async Task<ActionResult> UpdateDipendenteById(string id,DTODipendente dipendente)
        {
            bool resp =await _dipendentiRepository.UpdateDipendenteById(id, dipendente);
            if (resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggiornamento fallito");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDipendenteById(string id)
        {
            bool resp = await _dipendentiRepository.DeleteDipendenteById(id);
            if (resp)
            {
                return Ok("Eliminazione eseguita con successo");
            }
            else
            {
                return BadRequest("eliminazione fallita");
            }
        }

        [HttpGet][Route("/DipendneteNonInSettore")]
        public ActionResult<IEnumerable<dynamic>> EmployeeOfOtherSectors(string settore)
        {
            var resp = _dipendentiQueries.EmployeeOfOtherSectors(settore).Result;
            if(resp!= null)
            {
                if (resp.Count() > 0)
                {
                    return Ok(resp);
                }else 
                { 
                    return BadRequest("Nessun elemento trovato");
                }
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
        }

        [HttpGet][Route("/MigliorVenditore/{anno_min}-{anno_max}")]
        public ActionResult<IEnumerable<DTODipendente>> BestVendor(int anno_min, int anno_max)
        {
            var resp = _dipendentiQueries.BestVendor(anno_min, anno_max).Result;
            if (resp!= null)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
        }

        [HttpGet][Route("/ConcatenazioneNomeCognome")]
        public ActionResult<IEnumerable<dynamic>> ConcatNameSurname()
        {
            var resp = _dipendentiQueries.ChainedNameSurname().Result;
            return Ok(resp);
        }

        [HttpGet][Route("/ControlloDipendentiNonInGestione")]
        public ActionResult<IEnumerable<dynamic>> CheckEmployeeInGestione()
        {
            var resp = _dipendentiQueries.CheckEmployeeInGestione().Result;
            if(resp!= null)
            {
                if (resp.Count() > 0)
                {
                    return Ok(resp);
                }
                else
                {
                    return Ok("Tutti i Dipendenti sono stati registrati in Gestione");
                }
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
            
        }

        [HttpGet][Route("/CongomePiùLungo")]
        public ActionResult<IEnumerable<dynamic>> LongestSurname()
        {
            var resp = _dipendentiQueries.LongestSurname().Result;
            if(resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
        }

    }
}
