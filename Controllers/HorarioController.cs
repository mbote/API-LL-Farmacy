
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;

namespace api_farmacia.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HorarioController : ControllerBase
    {
        public IConfiguration _configuration { get; }

        public HorarioController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "Horario")]
        public List<Horario> getgetAllHorario()
        {
            return new HorarioService().getAllHorario(_configuration);
        }

        [HttpPost]
        public int Create(Horario horario)
        {
            return new HorarioService().Create(horario, _configuration);

        }

        [HttpGet("{id}")]
        public Horario getHoario(int id)
        {
            return new HorarioService().getHoario(id, _configuration);
        }

        [HttpDelete("{id}")]
        public int deleteHorario(int id)
        {
            return new HorarioService().deleteHoario(id, _configuration);
        }

        [HttpPut("{id}")]
        public int updateHorario( int id, [FromBody]Horario horario)
        {
            return new HorarioService().updateHoario(horario, id, _configuration);
        }
    }
}