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
        private readonly ICliente_FattureQueries _cliente_FattureQueries;
        public Cliente_FattureController(ICliente_FattureQueries cliente_FattureQueries)
        {
            _cliente_FattureQueries = cliente_FattureQueries;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var str = new StringBuilder();
            var queryResp = await  _cliente_FattureQueries.PrintListInvoicesPerClient();
            foreach (var item in queryResp)
            {
                str.Append(FileHelperConversion.GetStringFromDTO(item));
            }

            return Ok(str.ToString());    
        }
    }
}
