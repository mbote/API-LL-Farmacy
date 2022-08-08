
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;

namespace api_farmacia.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TipoFuncionarioController : ControllerBase
    {
        public IConfiguration _configuration { get; }

        public TipoFuncionarioController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "TipoFuncionario")]
        public List<TipoFuncionario> getAllTipoServico()
        {
            return new TipoFuncionarioService().getAllTipoFuncionario(_configuration);
        }

        [HttpPost]
        public int Create(TipoFuncionario tipoFuncionario)
        {
            return new TipoFuncionarioService().Create(tipoFuncionario, _configuration);
        }

        [HttpGet("{id}")]
        public TipoFuncionario getTipoFuncionario(int id)
        {
            return new TipoFuncionarioService().getTipoFuncionario(id, _configuration);
        }

        [HttpDelete("{id}")]
        public int deleteTipoServico(int id)
        {
            return new TipoFuncionarioService().deleteTipoFuncionario(id, _configuration);
        }

        [HttpPut("{id}")]
        public int updateTipoFuncionario(int id, [FromBody] TipoFuncionario tipoFuncionario)
        {
            return new TipoFuncionarioService().updateTipoFuncionario(tipoFuncionario, id, _configuration);
        }
    }
}