
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;
using Microsoft.Extensions.Configuration;

namespace api_farmacia.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class ServicoController : ControllerBase
    {

        public IConfiguration _configuration { get; }

        public ServicoController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "Servico")]
        public List<Servico> getAllServicos()
        {
            return new ServicoService().getAllServicos(_configuration);
        }

        [HttpPost]
        public int Create(Funcionario funcionario)
        {
            return new FuncionarioService().Create(funcionario, _configuration);
        }


        [HttpGet("{id}")]
        public Funcionario getFuncionario(int id)
        {
            return new FuncionarioService().getFuncionario(id, _configuration);
        }

        [HttpDelete("{id}")]
        public int deleteFuncionario(int id)
        {
            return new FuncionarioService().deleteFuncionario(id, _configuration);
        }

        [HttpPut("{id}")]
        public int updateFuncionario(int id, [FromBody] Funcionario funcionario)
        {
            return new FuncionarioService().updateFuncionario(funcionario, id, _configuration);
        }

    }
}