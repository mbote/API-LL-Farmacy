using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services{
    public class AgendaService{

        public List<Agenda> getAllAgendas(IConfiguration configuration){
            List<Agenda> agendas = new List<Agenda>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using(SqlConnection conexao = new SqlConnection(stringConexao)){
                conexao.Open();
                string sql = "select * from Agenda ag " + 
                             "join Horario h            on ag.AgendaId             = h.AgendaId" + 
                             "join ServicoFuncionario s on ag.ServicoFuncionarioId = ag.ServicoFuncionarioId";
                using(SqlCommand cmd = new SqlCommand(sql,conexao)){
                    using(SqlDataReader dataAdapter =  cmd.ExecuteReader()){
                        while(dataAdapter.Read()){
                            
                        }
                    }
                }
            }
            return agendas;

        }

    }
}