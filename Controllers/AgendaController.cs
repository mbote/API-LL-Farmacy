
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;


namespace api_farmacia.Controllers
{

    [ApiController]
    [Route("Controller")]
    public class AgendaController : ControllerBase
    {

        public IConfiguration _configuration { get; }

        public AgendaController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "Agenda")]
        public string getAllAgendas()
        {
            return new AgendaService().getAllAgendas(_configuration);
        }
    }
}