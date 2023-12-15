using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Queries;
using Test.Repositories;
using Oracle.ManagedDataAccess.Client;
using Test.Abstractions;
using Test.InterfacesRepository;

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
        public ActionResult Get()
        {
            return Ok(_cliente_FattureQueries.PrintListInvoicesPerClient());    
        }
    }
}
