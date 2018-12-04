using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarioLikeGame.DAL;
using MarioLike.Model;

namespace MarioLikeGame
{
    public partial class frmTelaJogo : Form
    {
        //Declarando a DAL
        GamerDAL gamerDAL;
        
        //Criar um atributo para pegar o nome do jogador
        public string nomeGamer { get; set; }

        //Atributos para controle da movimentação do personagem
        private bool paraCima;
        private bool paraBaixo;
        private bool paraDireita;
        private bool paraEsquerda;

        //Variavel de condição de vitoria/derrota
        private bool vitoria = false;

        //Variavel de pontos
        private int pontos = 0;

        //Variavel de tempo
        private int segundos = 0;
        private int minutos = 0;

        //Atributo responsável pela velocidade de locomoção do personagem
        private int velocidade = 10;

        //Biblioteca do windows media player
        //WMPLib.WindowsMediaPlayer Tocador = new WMPLib.WindowsMediaPlayer();
        List<System.Windows.Media.MediaPlayer> sounds = new  List<System.Windows.Media.MediaPlayer>();

        public frmTelaJogo()
        {
            InitializeComponent();
            pcbGameOverl.Visible = false;
            pcbVitoria.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaJogo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmTelaJogo_Load(object sender, EventArgs e)
        {
            //Audio("Musica 1.mp3", "Play");
            playSound("Musica 1.mp3");

        }

        //Movimentar o personagem quando pressiono a tecla
        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                paraEsquerda = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                paraDireita = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                paraCima = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                paraBaixo = true;
            }
        }

        //Parar o movimento do personagem quando soltar a tecla
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    paraEsquerda = false;
                    break;
                case Keys.Right:
                    paraDireita = false;
                    break;
                case Keys.Up:
                    paraCima = false;
                    break;
                case Keys.Down:
                    paraBaixo = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pcbGameOverl.Visible = false;
            pcbVitoria.Visible = false;
            lblPontos.Text = "Pontos: " + pontos;
            timer2.Enabled = true;

            if (paraEsquerda)
            {
                //Movimenta o personagem para esquerda
                personagem.Left -= velocidade;
            }

            if (paraDireita)
            {
                //Movimenta o personagem para Direita
                personagem.Left += velocidade;
            }

            //Movimenta o personagem para cinma
            if (paraCima)
            {
                personagem.Top -= velocidade;
            }

            //Movimenta o personagem para baixo
            if (paraBaixo)
            {
                personagem.Top += velocidade;
            }

            //Posicionamento do personagem dentro da área do formulário (tela)
            if (personagem.Width == 62 && personagem.Height == 73)
            {
                if (personagem.Left < 0)
                {
                    personagem.Left = 0;
                }

                if (personagem.Left > 1180)
                {
                    personagem.Left = 1180;
                }

                if (personagem.Top < 70)
                {
                    personagem.Top = 70;
                }

                if (personagem.Top > 870)
                {
                    personagem.Top = 870;
                }
            }

            if (personagem.Width == 82 && personagem.Height == 93)
            {
                if (personagem.Left < 0)
                {
                    personagem.Left = 0;
                }

                if (personagem.Left > 1160)
                {
                    personagem.Left = 1160;
                }

                if (personagem.Top < 70)
                {
                    personagem.Top = 70;
                }

                if (personagem.Top > 850)
                {
                    personagem.Top = 850;
                }
            }


            if (personagem.Width == 102 && personagem.Height == 113)
            {
                if (personagem.Left < 0)
                {
                    personagem.Left = 0;
                }

                if (personagem.Left > 1140)
                {
                    personagem.Left = 1140;
                }

                if (personagem.Top < 70)
                {
                    personagem.Top = 70;
                }

                if (personagem.Top > 830)
                {
                    personagem.Top = 830;
                }
            }

            if (personagem.Width == 122 && personagem.Height == 133)
            {
                if (personagem.Left < 0)
                {
                    personagem.Left = 0;
                }

                if (personagem.Left > 1120)
                {
                    personagem.Left = 1120;
                }

                if (personagem.Top < 70)
                {
                    personagem.Top = 70;
                }

                if (personagem.Top > 810)
                {
                    personagem.Top = 810;
                }
            }
            //Loop para checar todos os controles inseridos no form
            foreach (Control item in this.Controls)
            {

                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag == "inimigo")
                {

                    //Checar colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        playSound("smb_gameover.wav");
                        vitoria = false; 
                            this.Controls.Remove(item);
                            GameOver(vitoria);
   
                        
                    }
                }

                if (item is PictureBox && (string)item.Tag == "coletaveis" || (string)item.Tag == "coletaveiscrescer" || (string)item.Tag == "coletaveisvelo")
                {
                    //Checar colisão com as PictureBox
                    if (((PictureBox)item).Bounds.IntersectsWith(personagem.Bounds))
                    {
                        this.Controls.Remove(item);
                        pontos++;                     
                        //Audio("smb_coin.war", "Play");
                        if (pontos == 16)
                        {                        
                            stopSound();
                            playSound("Vitoria1mp3");
                            //Audio("Vitoria1.mp3", "Play");
                            vitoria = true;
                                GameOver(vitoria);                              

                        }

                        if ((string)item.Tag == "coletaveiscrescer")
                        {
                            personagem.Height += 20;
                            personagem.Width += 20;
                            playSound("smb_powerup.wav");
                        }
                        if ((string)item.Tag == "coletaveisvelo")
                        {
                            velocidade += 10;
                            playSound("smb_powerup_appears.wav");
                        }
                        if((string)item.Tag == "coletaveis")
                        {
                            playSound("smb_coin.wav");
                        }

                    }
                }

            }
            
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void GameOver(bool ganhou)
        {
            stopSound();
            lblPontos.Text = "Pontos: " + pontos;
            personagem.Visible = false;
            btnRestart.Visible = true;
            btnRestart.Focus();
            GravaHiScore();
            if (ganhou)
            {
                RemovePictureBox();
                pcbVitoria.Visible = true;
            }
            else
            {
                RemovePictureBox();
                //Audio("smb_gameover.war", "Play");
                pcbGameOverl.Visible = true;

            }
            timer1.Stop();
            timer2.Stop();
        }

        private void RemovePictureBox()
        {
            foreach (Control item in this.Controls)
            {

                //Verifica se o jogador colidiu com o inimigo, caso positivo GameOver
                if (item is PictureBox && (string)item.Tag != "GameOver")
                {
                    ((PictureBox)item).Image = null;
                   
                }

            }
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTelaInicial frmTelaInicial = new frmTelaInicial();
            frmTelaInicial.Closed += (s, args) => this.Close();
            frmTelaInicial.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            segundos++;
            if (segundos == 60)
            {
                    segundos = 0;
                    minutos++;
            }
            lblTempo.Text = "Tempo: " + minutos.ToString("00") + ":" + segundos.ToString("00");
        }

        //private void Audio(string caminho, string estadoMP)
        //{
        //    //Verifica se ocorreu erros ao instanciar o windows media player
        //    Tocador.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Tocador_MediaError);

        //    Tocador.URL = caminho;
        //    if (estadoMP.Equals("Play"))
        //    {
        //        Tocador.controls.play();
        //    }
        //    else if (Tocador.Equals("Stop"))
        //    {
        //        Tocador.controls.stop();
        //    }

        //}

        //private void Tocador_MediaError(object pMediaObject)
        //{
        //    MessageBox.Show("Não é possivel executar o arquivo de midia");
        //    this.Close();
        //}

        private void playSound(string nome)
        {
            string url = Application.StartupPath + @"/" + nome;
            var sound = new System.Windows.Media.MediaPlayer();
            sound.Open(new Uri(url));
            sound.Play();
            sounds.Add(sound);

        }

        private void stopSound()
        {
            for (int i = sounds.Count - 1; i >= 0 ; i--)
            {
                sounds[i].Stop();
                sounds.RemoveAt(i);

            }
        }


        public void GravaHiScore()
        {
            //Instanciar a DAL
            gamerDAL = new GamerDAL();

            Placar placar = new Placar();

            var frm = new frmTelaInicial();

            placar.NomeJogador = this.nomeGamer;

            if (!this.nomeGamer.Equals(""))
            {
                placar.NomeJogador = this.nomeGamer;
            }
            else
            {
                placar.NomeJogador = "Anonimo";
            }

            placar.Score = pontos;
            placar.DataScore = DateTime.Now;
            placar.TempoJogador = lblTempo.Text;

            //Chama a função da DAL passando o objeto populado com parâmetro
            if (!gamerDAL.Inserir(placar))
            {
                //Deu Ruim! Exibe Mensagem para o usoario... :(
                MessageBox.Show("Erro ao inserir os dados: \r\n\r\n " + gamerDAL.MensagemErro, "Mario Like Game");
            }

        }

    }
}
