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
        public Cliente_FattureController(ICliente_FattureQueries cliente_FattureQueries, IFileHeplerConversion fhConversion)
        {
            _cliente_FattureQueries = cliente_FattureQueries;
            _fhConversion = fhConversion;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var str = new StringBuilder();
            var queryResp = await  _cliente_FattureQueries.PrintListInvoicesPerClient();
            foreach (var item in queryResp)
            {
                str.Append(_fhConversion.GetStringFromDTO(item));
            }

            return Ok(str.ToString());    
        }
    }
}
