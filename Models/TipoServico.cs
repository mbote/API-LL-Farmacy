using System.ComponentModel.DataAnnotations.Schema;

namespace api_farmacia.Models
{
    public class TipoServico
    {
        public  int TipoServicoId { get; set; }
        public  string?  tipo { get; set; }

        public ICollection<Servico>?  Servicos { get; set; }
    }
}