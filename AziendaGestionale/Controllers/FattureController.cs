using AziendaGestionale.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Abstractions;
using Test.InterfacesRepository;
using Test.Models;

namespace AziendaGestionale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FattureController : ControllerBase
    {
        private readonly IFattureQueries _fattureQueries;
        private readonly IFattureRepository _fattureRepository;
        public FattureController(IFattureRepository repository, IFattureQueries queries)
        {
            _fattureQueries = queries;
            _fattureRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOFattura>> GetAll()
        {
            return Ok(_fattureQueries.GetAll().Result);
        }

        [HttpGet("{id}-{data}")]
        public ActionResult<DTOFattura> GetFatturaById(string id, string data)
        {
            var resp = _fattureQueries.GetById(id, data).Result;
            if (resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return NotFound("Elemento non trovato");
            }

        }

        [HttpPost]
        public ActionResult CreateFattura(DTOFattura fattura)
        {
            bool resp = _fattureRepository.CreateFattura(fattura);
            if (resp)
            {
                return Ok("Aggiunta eseguita con successo");
            }
            else
            {
                return BadRequest("Aggiunta fallita");
            }
        }

        [HttpPut("{id}-{data}")]
        public ActionResult UpdateFatturaById(string id, string data, DTOFattura fattura)
        {
            bool resp = _fattureRepository.UpdateFatturaByID(id, data,fattura);
            if (resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }else
            {
                return BadRequest("Aggioirnamento fallito");
            }
        }

        [HttpDelete("{id}-{data}")]
        public ActionResult DeleteFatturaById(string id,string data)
        {
            bool resp = _fattureRepository.DeleteFatturaByID(id, data);
            if (resp)
            {
                return Ok("Eleiminazione eseguita con successo");
            }
            else
            {
                return BadRequest("Eliminazione fallita");
            }
        }

        [HttpPut]
        [Route("/AggiornaTot")]
        public ActionResult UpdateTot()
        {
            bool resp = _fattureRepository.UpdateTot();
            if (resp)
            {
                return Ok("Aggiornamento eseguito con successo");
            }
            else
            {
                return BadRequest("Aggioirnamento fallito");
            }
        }

        [HttpGet]
        [Route("/IncassoAnnuale")]
        public ActionResult AnnualIncome()
        {
            var resp = _fattureQueries.AnnualIncome().Result;
            if(resp != null)
            {
                return Ok(resp);
            }
            else 
            { 
                return BadRequest("Operazine fallita");
            }
        }

        [HttpGet]
        [Route("/IncassoAnnualePerCliente")]
        public ActionResult AnnualIncomePerClient()
        {
            var resp = _fattureQueries.AnnualIncomePerClient().Result;
            if( resp != null)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest("Operazione fallita");
            }
        }

        [HttpGet]
        [Route("/DettagliIncassoAnnualePerCliente")]
        public ActionResult AnnualIncomePerClientDetails()
        {
            var resp = _fattureQueries.AnnualIncomePerClientWithDetails().Result;
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
