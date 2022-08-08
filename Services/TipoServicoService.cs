
using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services
{
    public class TipoServicoService
    {
        public List<TipoServico> getAllTipoServico(IConfiguration configuration)
        {
            List<TipoServico> listaDeTipoDeServico = new List<TipoServico>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "select * from TipoServico";
                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            TipoServico tipoServico = new TipoServico();
                            tipoServico.TipoServicoId = Convert.ToInt32(dataReader["TipoServicoId"]);
                            tipoServico.tipo = Convert.ToString(dataReader["tipo"]);
                            listaDeTipoDeServico.Add(tipoServico);
                        }
                    }
                    conexao.Close();
                }
            }
            return listaDeTipoDeServico;
        }

        public int Create(TipoServico tipoServico, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string sqlPessoa = $"Insert Into TipoServico (tipo) Values ('{tipoServico.tipo}')";
                using (SqlCommand cmd = new SqlCommand(sqlPessoa, conexao))
                {
                    cmd.CommandType = CommandType.Text;
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                    return 1;
                }
            }
        }

        public TipoServico getTipoServico(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            TipoServico tipoServico = new TipoServico();
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"select * from TipoServico where TipoServicoId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tipoServico.TipoServicoId = Convert.ToInt32(dataReader["TipoServicoId"]);
                        tipoServico.tipo = Convert.ToString(dataReader["tipo"]);
                    }
                }
                connection.Close();
            }
            return tipoServico;
        }
        public int deleteTipoServico(int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"delete from  TipoServico where TipoServicoId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                rs = (int)cmd.ExecuteNonQuery();
                connection.Close();
            }
            return rs;
        }
        public int updateTipoServico(TipoServico tipoServico, int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"update TipoServico set tipo ='{tipoServico.tipo}' where TipoServicoId='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    rs = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return rs;
        }

    }
}

