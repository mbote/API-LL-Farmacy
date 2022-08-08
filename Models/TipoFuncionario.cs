using System.ComponentModel.DataAnnotations.Schema;

namespace api_farmacia.Models
{
    public class TipoFuncionario
    {
        public int TipoFuncionarioId { get; set; }
        public string?  tipo { get; set; }
        public ICollection<Funcionario>?  funcionarios { get; set; }
    }
}