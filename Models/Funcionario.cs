using System.ComponentModel.DataAnnotations.Schema;

namespace api_farmacia.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }

        [ForeignKey("PessoaId")]
        public int PessoaId { get; set; }
       
        public string?  numeroOrdem { get; set; }
        public string?  nif { get; set; }

        [ForeignKey("TipoFuncionarioId")]
        public int TipoFuncionarioId { get; set; }

        public Pessoa? pessoa {get; set;}
        public TipoFuncionario? tipoFuncionario {get; set;}

        public ICollection<ServicoFuncionario>?  servicoFuncionario { get; set; }
    }
}