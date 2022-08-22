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
            List<Agenda> agendas = new List<Agenda>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "select * from Agenda ag " +
                             "join Horario h            on ag.AgendaId             = h.AgendaId" +
                             "join ServicoFuncionario s on ag.ServicoFuncionarioId = ag.ServicoFuncionarioId" +
                             "join Servico sc           on sc.ServicoId            = s.ServicoId" +
                             "join Funcioario f         on f.FuncionarioId         = s.FuncionarioId" +
                             "join Pessoa p             on p.PessoaId              = f.PessoaId";
                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataAdapter = cmd.ExecuteReader())
                    {
                        string JSONString = string.Empty;
                        JSONString = JsonConvert.SerializeObject(dataAdapter);
                        return JSONString;
                    }
                }
            }
        }

    }
}