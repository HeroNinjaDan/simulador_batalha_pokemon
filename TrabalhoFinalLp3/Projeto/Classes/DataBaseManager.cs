using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace TrabalhoFinalLp3
{
    class DataBaseManager
    {
        public string stringDeConexao;
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataReader leitor;

        public DataBaseManager(string nomeBanco)
        {
            string caminhoDoBanco = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"));
            stringDeConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + caminhoDoBanco + @"bin\Debug\Projeto\BD\" + nomeBanco + @".mdf';Integrated Security=True";


            conexao = new SqlConnection(stringDeConexao);
        }


        public DataTable ConsultarBanco(string comandoDQL)
        {
            try
            {
                conexao.Open();
                comando = new SqlCommand(comandoDQL, conexao);

                leitor = comando.ExecuteReader();

                DataTable tabela = new DataTable();

                tabela.Load(leitor);

                return tabela;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conexao != null)
                    conexao.Close();

                if (leitor != null)
                    leitor.Close();
            }
        }
    }
}
