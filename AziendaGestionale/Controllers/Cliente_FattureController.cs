using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;
using Test.Repositories;
using Oracle.ManagedDataAccess.Client;
using Test.Abstractions;
using Test.InterfacesRepository;
using ApplicationLayer;
using System.Text;

namespace AziendaGestionale.Controllers
{
    [Route("api/Cliente_Fatture")]
    [ApiController]
    public class Cliente_FattureController : ControllerBase
    {
        private readonly IFileHeplerConversion _fhConversion;
        private readonly ICliente_FattureQueries _cliente_FattureQueries;
        private readonly IClientiRepository _clientiRepository;
        private readonly IFattureRepository _fattureRepository;
        public Cliente_FattureController(ICliente_FattureQueries cliente_FattureQueries, IFileHeplerConversion fhConversion, IClientiRepository clientiRepository, IFattureRepository fattureRepository)
        {
            _cliente_FattureQueries = cliente_FattureQueries;
            _fhConversion = fhConversion;
            _clientiRepository = clientiRepository;
            _fattureRepository = fattureRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            
            var queryResp = await  _cliente_FattureQueries.PrintListInvoicesPerClient();
            var result =_fhConversion.GetStringFromDTO(queryResp);
            /*
            foreach (var item in queryResp)
            {
             str.Append();
             
            }
            
            _fhConversion.SaveRecordText();
            */
            return Ok(result.ToString());    
        }

        [HttpPut]
        public async Task<ActionResult> ImportText(string recordsText)
        {
            bool controlErrorFatture = false;
            bool controlErrorClienti = false;

            List<DTOFattura> listFatture= _fhConversion.GetDTOFromString(recordsText, out List<DTOCliente> listClienti);
            
            foreach(DTOFattura fattura in listFatture)
            {
                if(! await _fattureRepository.CreateFattura(fattura))
                {
                    controlErrorFatture = true;
                };
            }
            foreach(DTOCliente cliente in listClienti)
            {
                if (! await _clientiRepository.CreateCliente(cliente))
                {
                    controlErrorClienti = true;
                }
            }
            if(controlErrorFatture && controlErrorClienti)
            {
                return BadRequest("operazione fallita");
            }else if (controlErrorClienti)
            {
                return BadRequest("Errore nell'nserimento di Clienti");
            }else if(controlErrorFatture) 
            {
                return BadRequest("errore nell'aggiunta di Fatture");
            }
            else
            {
                return Ok("Operazione riuscita");
            }
            
        }
    }
}
