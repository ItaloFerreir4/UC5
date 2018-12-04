using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MarioLike.Model;

namespace MarioLikeGame.DAL
{
    public class GamerDAL
    {

        //Declarar o objeto de conexao com o BD
        private SqlConnection conexao;

        //Exibir as mensagens de erro
        public string MensagemErro { get; set; }

        public GamerDAL()
        {
            //Criar o objeto para ler a configuração
            LeitorConfiguracao leitor = new LeitorConfiguracao();

            //Instaciar a conexao,
            conexao = new SqlConnection();
            conexao.ConnectionString = leitor.LerConexao();
        }

        public bool Inserir(Placar placar)
        {
            bool resultado = false;
            //Limpa mensagem de erro
            MensagemErro = "";

            //Declarar comando SQL
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO jogador (Nome_Jogador,Score_Jogador,Data_Score_Jogador,Tempo_Jogador)" +
                "VALUES (@Nome,@Score,@Data,@Tempo)";

            //Criar os parametros
            comando.Parameters.AddWithValue("@Nome", placar.NomeJogador);
            comando.Parameters.AddWithValue("@Score", placar.Score);
            comando.Parameters.AddWithValue("@Data", placar.DataScore);
            comando.Parameters.AddWithValue("@Tempo", placar.TempoJogador);

            //Executar o comando
            try
            {
                //Abrir a conexão
                conexao.Open();
                //Executar o comando
                comando.ExecuteNonQuery();
                //Se chegou ate aqui, então funcionou! :)
                resultado = true;
            }
            catch (Exception ex)
            {
                //Se entrou aqui, então deu ruim! :(
                MensagemErro = ex.Message;
            }
            finally
            {
                //Finalizar fechando a conexao
                conexao.Close();
            }
            return resultado;
        }

        public List<Placar> Listar()
        {
            //Instaciar a lista
            List<Placar> resultado = new List<Placar>();

            //Declarar o comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = " SELECT TOP 10 ID_Jogador,Nome_Jogador,Score_Jogador,Data_Score_Jogador,Tempo_Jogador " +
                " FROM jogador ORDER BY Score_Jogador DESC, Tempo_Jogador";


            //Executar o comando
            try
            {
                //Abrir a conexão
                conexao.Open();

                //Executar o comando e receber o resultado
                SqlDataReader leitor = comando.ExecuteReader();

                //Verificar se encontrou algo
                while (leitor.Read() == true)
                {
                    //Instanciar o objeto
                    Placar placar = new Placar();
                    placar.IdJogador = Convert.ToInt32(leitor["ID_Jogador"]);
                    placar.NomeJogador = leitor["Nome_Jogador"].ToString();
                    placar.Score = Convert.ToInt32(leitor["Score_Jogador"]);
                    placar.DataScore = Convert.ToDateTime(leitor["Data_Score_Jogador"]);
                    placar.TempoJogador = leitor["Tempo_Jogador"].ToString();

                    resultado.Add(placar);
                }

                //Fechar o leitor
                leitor.Close();
            }
            catch (Exception ex)
            {
                //Se entrou aqui, entao deu ruim!
                string mensagem = ex.Message;
            }
            finally
            {
                //Finalizar fechando a conexao
                conexao.Close();
            }
            return resultado;
        }
    }
}
