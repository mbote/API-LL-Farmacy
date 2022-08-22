using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

using Newtonsoft.Json;

namespace api_farmacia.Services
{
    public class AgendaService
    {

        public string getAllAgendas(IConfiguration configuration)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<Agenda> agendas = new List<Agenda>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "select p.nome, sc.servico, t.tipo, h.dia, h.hora, ag.AgendaId " +
                             "from Agenda ag " +
                             "join Horario h             on ag.HorarioId             = h.HorarioId " +
                             "join ServicoFuncionario s  on ag.ServicoFuncionarioId  = ag.ServicoFuncionarioId " +
                             "join servicos sc           on sc.ServicoId             = s.ServicoId " +
                             "join Funcionario f         on f.FuncionarioId          = s.FuncionarioId " +
                             "join Pessoa p              on p.PessoaId               = f.PessoaId " +
                             "join TipoFuncionario t     on t.TipoFuncionarioId      = f.TipoFuncionarioId";
                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        string JSONString = string.Empty;
                        var dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        JSONString = JsonConvert.SerializeObject(dataTable);
                        return JSONString;
                    }
                }
            }
        }

        public int Create(Agenda agenda, IConfiguration configuration){
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using(SqlConnection conexao = new SqlConnection(stringConexao)){
                string sql = $"insert into Agenda(HorarioId,ServicoFuncionarioId) values('{agenda.HorarioId}', '{agenda.ServicoFuncionarioId}')";
                using(SqlCommand cmd = new SqlCommand(sql, conexao)){
                    cmd.CommandType = CommandType.Text;
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";
                    int idAgenda = Int16.Parse("" + cmd.ExecuteScalar());
                    conexao.Close();
                    return idAgenda;
                }
            }
        }
    }
}