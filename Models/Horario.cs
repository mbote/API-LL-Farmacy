using System.ComponentModel.DataAnnotations.Schema;
namespace api_farmacia.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }
        public string?  dia { get; set; }
        public string?  hora { get; set; }

        public ICollection<Agenda>?  agenda { get; set; }
    }
}