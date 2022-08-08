using System.Data.SqlClient;

namespace api_farmacia.Services{

    public class Connection{
        public string? strConexao {get; set;}
        public SqlConnection conexao {get; set;}

        public Connection(){
           // this.strConexao = @"Server=localhost\\SQLEXPRESS;Database=farmacia;Trusted_Connection=True;MultipleActiveResultSets=true";
            this.strConexao = @"Data Source=localhost\\SQLEXPRESS;Initial Catalog=farmacia;Integrated Security=True";
        }



        public SqlConnection criarConexao(){
            this.conexao = new SqlConnection();
            this.conexao.ConnectionString = this.strConexao;
            return this.conexao;
        }

        public void abrirConexao(){
            this.conexao.Open();
        }

        public void fecharConexao(){
            this.conexao.Close();
        }
        
    }
}
