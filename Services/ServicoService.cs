
using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services
{
    public class ServicoService
    {
        public List<Servico> getAllServicos(IConfiguration configuration)
        {
            List<Servico> servicos = new List<Servico>();
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                string sql = "select * from Servicos s join TipoServico t on t.TipoServicoId = s.TipoServicoId";

                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Servico servico = new Servico();
                            servico.ServicoId = Convert.ToInt32(dataReader["ServicoId"]);
                            servico.servico = Convert.ToString(dataReader["servico"]);
                            servico.TipoServicoId = Convert.ToInt32(dataReader["TipoServicoId"]);
                            servico.tipoServico = new TipoServico();
                            servico.tipoServico.tipo = Convert.ToString(dataReader["tipo"]);
                            servico.tipoServico.TipoServicoId = Convert.ToInt32(dataReader["TipoServicoId"]);  
                            servicos.Add(servico);              
                        }
                    }
                    conexao.Close();
                }
            }
            return servicos;
        }

        public int Create(Funcionario funcionario, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string sqlPessoa = $"Insert Into Pessoa (nome, sexo, bi, email, telefone) Values ('{funcionario.pessoa.nome}', '{funcionario.pessoa.sexo}','{funcionario.pessoa.bi}', '{funcionario.pessoa.email}', '{funcionario.pessoa.telefone}')";
                using (SqlCommand cmd = new SqlCommand(sqlPessoa, conexao))
                {
                    cmd.CommandType = CommandType.Text;
                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "Select @@Identity";
                    int idPessoa = Int16.Parse("" + cmd.ExecuteScalar());
                    cmd.CommandText = $"Insert Into Funcionario (PessoaId, numeroOrdem, nif, TipoFuncionarioId) Values ('{idPessoa}','{funcionario.numeroOrdem}', '{funcionario.nif}', '{funcionario.TipoFuncionarioId}')";
                    cmd.ExecuteNonQuery();

                    conexao.Close();
                    return idPessoa;
                }
            }
        }

        public Funcionario getFuncionario(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            Funcionario funcionario = new Funcionario();
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = "select * from Funcionario c " +
                            "join Pessoa p          on c.PessoaId = p.PessoaId " +
                            "join TipoFuncionario t on t.TipoFuncionarioId = c.TipoFuncionarioId " +
                            $"where c.FuncionarioId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        funcionario.FuncionarioId = Convert.ToInt32(dataReader["FuncionarioId"]);
                        funcionario.PessoaId = Convert.ToInt32(dataReader["PessoaId"]);
                        funcionario.pessoa = new Pessoa();
                        funcionario.pessoa.nome = Convert.ToString(dataReader["nome"]);
                        funcionario.pessoa.sexo = Convert.ToString(dataReader["sexo"]);
                        funcionario.pessoa.bi = Convert.ToString(dataReader["bi"]);
                        funcionario.pessoa.email = Convert.ToString(dataReader["email"]);
                        funcionario.pessoa.telefone = Convert.ToInt32(dataReader["telefone"]);
                        funcionario.nif = Convert.ToString(dataReader["nif"]);
                        funcionario.numeroOrdem = Convert.ToString(dataReader["numeroOrdem"]);
                        funcionario.tipoFuncionario = new TipoFuncionario();
                        funcionario.tipoFuncionario.tipo = Convert.ToString(dataReader["tipo"]);
                        funcionario.tipoFuncionario.TipoFuncionarioId = Convert.ToInt32(dataReader["TipoFuncionarioId"]);
                    }
                }
                connection.Close();
            }
            return funcionario;
        }

        public int deleteFuncionario(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"delete p from Funcionariofc left join Pessoa p on f.PessoaId = p.PessoaId where f.FuncionarioId ='{id}'";

                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"delete from Funcionario  where FuncionarioId = ('{id}')";
                cmd.ExecuteNonQuery();

                connection.Close();
            }
            return 1;
        }
        public int updateFuncionario(Funcionario funcionario, int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"update Pessoa set " +
                             $"nome ='{funcionario.pessoa.nome}', " +
                             $"email ='{funcionario.pessoa.email}', " +
                             $"bi ='{funcionario.pessoa.bi}', " +
                             $"sexo ='{funcionario.pessoa.sexo}', " +
                             $"telefone ='{funcionario.pessoa.telefone}' " +
                             $"from Funcionario f join Pessoa p on f.PessoaId = p.PessoaId where f.FuncionarioId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"update Funcionario set " +
                             $"nif ='{funcionario.nif}', " +
                             $"numeroOrdem ='{funcionario.numeroOrdem}', " +
                             $"TipoFuncionarioId ='{funcionario.TipoFuncionarioId}' " +
                             $"where FuncionarioId='{id}'";                
                cmd.ExecuteNonQuery();

                connection.Close();
            }
            return 0;
        }
    }
}

