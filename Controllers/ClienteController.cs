
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;
using Microsoft.Extensions.Configuration;

namespace api_farmacia.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : ControllerBase
    {

        public IConfiguration _configuration { get; }

        public ClienteController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "Cliente")]
        public List<Cliente> getAllCliente()
        {
            return new ClienteService().getAllCliente(_configuration);
        }

        [HttpPost]
        public int Create(Pessoa pessoa)
        {
            return new ClienteService().Create(pessoa, _configuration);
        }

        [HttpGet("{id}")]
        public Cliente getHoario(int id)
        {
            return new ClienteService().getCliente(id, _configuration);
        }

        [HttpDelete("{id}")]
        public int deleteCliente(int id)
        {
            return new ClienteService().deleteCliente(id, _configuration);
        }

        
        [HttpPut("{id}")]
        public int updateCliente( int id, [FromBody]Pessoa cliente)
        {
            return new ClienteService().updateCliente(cliente, id, _configuration);
        }
    }
}