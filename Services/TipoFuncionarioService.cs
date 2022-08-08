
using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services
{
    public class TipoFuncionarioService
    {
        public List<TipoFuncionario> getAllTipoFuncionario(IConfiguration configuration)
        {
            List<TipoFuncionario> listaDeTipoDeFuncionarios = new List<TipoFuncionario>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "select * from TipoFuncionario";
                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            TipoFuncionario tipoFuncionario = new TipoFuncionario();
                            tipoFuncionario.TipoFuncionarioId = Convert.ToInt32(dataReader["TipoFuncionarioId"]);
                            tipoFuncionario.tipo = Convert.ToString(dataReader["tipo"]);
                            listaDeTipoDeFuncionarios.Add(tipoFuncionario);
                        }
                    }
                    conexao.Close();
                }
            }
            return listaDeTipoDeFuncionarios;
        }

        public int Create(TipoFuncionario tipoFuncionario, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string sqlPessoa = $"Insert Into TipoFuncionario (tipo) Values ('{tipoFuncionario.tipo}')";
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

        public TipoFuncionario getTipoFuncionario(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            TipoFuncionario tipoFuncionario = new TipoFuncionario();
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"select * from TipoFuncionario where TipoFuncionarioId ='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tipoFuncionario.TipoFuncionarioId = Convert.ToInt32(dataReader["TipoFuncionarioId"]);
                        tipoFuncionario.tipo = Convert.ToString(dataReader["tipo"]);
                    }
                }
                connection.Close();
            }
            return tipoFuncionario;
        }
        public int deleteTipoFuncionario(int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"delete from  TipoFuncionario where TipoFuncionarioId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                rs = (int)cmd.ExecuteNonQuery();
                connection.Close();
            }
            return rs;
        }
        public int updateTipoFuncionario(TipoFuncionario tipoFuncionario, int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"update TipoFuncionario set tipo ='{tipoFuncionario.tipo}' where TipoFuncionarioId='{id}'";
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

