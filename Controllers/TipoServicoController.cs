
using Microsoft.AspNetCore.Mvc;
using api_farmacia.Models;
using api_farmacia.Services;

namespace api_farmacia.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TipoServicoController : ControllerBase
    {
        public IConfiguration _configuration { get; }

        public TipoServicoController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet(Name = "TipoServico")]
        public List<TipoServico> getAllTipoServico()
        {
            return new TipoServicoService().getAllTipoServico(_configuration);
        }

        [HttpPost]
        public int Create(TipoServico tipoServico)
        {
            return new TipoServicoService().Create(tipoServico, _configuration);
        }

        [HttpGet("{id}")]
        public TipoServico getTipoServico(int id)
        {
            return new TipoServicoService().getTipoServico(id, _configuration);
        }

        [HttpDelete("{id}")]
        public int deleteTipoServico(int id)
        {
            return new TipoServicoService().deleteTipoServico(id, _configuration);
        }

        [HttpPut("{id}")]
        public int updateTipoServico(int id, [FromBody] TipoServico tipoServico)
        {
            return new TipoServicoService().updateTipoServico(tipoServico, id, _configuration);
        }
    }
}