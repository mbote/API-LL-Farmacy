
using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services
{
    public class HorarioService
    {
        public List<Horario> getAllHorario(IConfiguration configuration)
        {
            List<Horario> listaHorarios = new List<Horario>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();
                string sql = "select * from Horario";
                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Horario horario = new Horario();
                            horario.HorarioId = Convert.ToInt32(dataReader["HorarioId"]);
                            horario.dia = Convert.ToString(dataReader["dia"]);
                            horario.hora = Convert.ToString(dataReader["hora"]);
                            listaHorarios.Add(horario);
                        }
                    }
                    conexao.Close();
                }
            }
            return listaHorarios;
        }

        public int Create(Horario horario, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string sqlPessoa = $"Insert Into Horario (dia, hora) Values ('{horario.dia}', '{horario.hora}')";
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

        public Horario getHoario(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            Horario horario = new Horario();
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"select * from Horario where HorarioId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        horario.HorarioId = Convert.ToInt32(dataReader["HorarioId"]);
                        horario.dia = Convert.ToString(dataReader["dia"]);
                        horario.hora = Convert.ToString(dataReader["hora"]);
                    }
                }
                connection.Close();
            }
            return horario;
        }
        public int deleteHoario(int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"delete from Horario where HorarioId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                rs = (int)cmd.ExecuteNonQuery();
                connection.Close();
            }
            return rs;
        }
        public int updateHoario(Horario horario, int id, IConfiguration configuration)
        {
            int rs = 0;
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"update Horario set dia ='{horario.hora}', hora='{horario.hora}' where HorarioId='{id}'";
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

