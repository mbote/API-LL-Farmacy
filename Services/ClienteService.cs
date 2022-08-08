
using api_farmacia.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_farmacia.Services
{
    public class ClienteService
    {
        public List<Cliente> getAllCliente(IConfiguration configuration)
        {
            List<Cliente> clientes = new List<Cliente>();

            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                string sql = "select * from Cliente c join Pessoa p on c.PessoaId = p.PessoaId";

                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Cliente cliente = new Cliente();
                            cliente.ClienteId = Convert.ToInt32(dataReader["ClienteId"]);
                            cliente.PessoaId = Convert.ToInt32(dataReader["PessoaId"]);
                            cliente.pessoa = new Pessoa();
                            cliente.pessoa.nome = Convert.ToString(dataReader["nome"]);
                            cliente.pessoa.sexo = Convert.ToString(dataReader["sexo"]);
                            cliente.pessoa.bi = Convert.ToString(dataReader["bi"]);
                            cliente.pessoa.email = Convert.ToString(dataReader["email"]);
                            cliente.pessoa.telefone = Convert.ToInt32(dataReader["telefone"]);
                            clientes.Add(cliente);
                        }
                    }
                    conexao.Close();
                }
            }
            return clientes;
        }


        public int Create(Pessoa pessoa, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection conexao = new SqlConnection(stringConexao))
            {
                string sqlPessoa = $"Insert Into Pessoa (nome, sexo, bi, email, telefone) Values ('{pessoa.nome}', '{pessoa.sexo}','{pessoa.bi}', '{pessoa.email}', '{pessoa.telefone}')";
                using (SqlCommand cmd = new SqlCommand(sqlPessoa, conexao))
                {
                    cmd.CommandType = CommandType.Text;
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Select @@Identity";
                    int idPessoa = Int16.Parse("" + cmd.ExecuteScalar());
                    cmd.CommandText = $"Insert Into Cliente (PessoaId) Values ('{idPessoa}')";
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                    return idPessoa;
                }
            }
        }

        public Cliente getCliente(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            Cliente cliente = new Cliente();
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"select * from Cliente c join Pessoa p on c.PessoaId = p.PessoaId where c.ClienteId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        cliente.ClienteId = Convert.ToInt32(dataReader["ClienteId"]);
                        cliente.PessoaId = Convert.ToInt32(dataReader["PessoaId"]);
                        cliente.pessoa = new Pessoa();
                        cliente.pessoa.nome = Convert.ToString(dataReader["nome"]);
                        cliente.pessoa.sexo = Convert.ToString(dataReader["sexo"]);
                        cliente.pessoa.bi = Convert.ToString(dataReader["bi"]);
                        cliente.pessoa.email = Convert.ToString(dataReader["email"]);
                        cliente.pessoa.telefone = Convert.ToInt32(dataReader["telefone"]);
                    }
                }
                connection.Close();
            }
            return cliente;
        }

        public int deleteCliente(int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $" delete p from Cliente c left join Pessoa p on c.PessoaId = p.PessoaId where c.ClienteId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"delete from Cliente  where ClienteId= ('{id}')";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return 0;
        }
        public int updateCliente(Pessoa pessoa, int id, IConfiguration configuration)
        {
            string stringConexao = configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                string sql = $"update Pessoa set " +
                             $"nome ='{pessoa.nome}', " +
                             $"email ='{pessoa.email}', " +
                             $"bi ='{pessoa.bi}', " +
                             $"sexo ='{pessoa.sexo}', " +
                             $"telefone ='{pessoa.telefone}' " +
                             $"from Cliente c left join Pessoa p on c.PessoaId = p.PessoaId where c.ClienteId='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return 0;
        }
    }
}

