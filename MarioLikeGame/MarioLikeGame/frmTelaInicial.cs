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

namespace MarioLikeGame
{
    public partial class frmTelaInicial : Form
    {
        GamerDAL gamerDAL;
        public frmTelaInicial()
        {
            InitializeComponent();
        }

        private void PreencherGrid()
        {

            //Declarando a DAL
            GamerDAL gamerDAL;

            //Instanciando a DAL na construção do formulario
            gamerDAL = new GamerDAL();

            //Limpando o dataSource
            dgListaRecord.DataSource = null;

            //Listando a DAL
            dgListaRecord.DataSource = gamerDAL.Listar();

            //Removendo a coluna id_Jogador
            dgListaRecord.Columns.Remove("IdJogador");

            dgListaRecord.CurrentRow.DefaultCellStyle.BackColor = Color.Red;

            dgListaRecord.EnableHeadersVisualStyles = false;
        }

        private void pbMario2_Click(object sender, EventArgs e)
        {

        }

        private void lblNomeJogo_Click(object sender, EventArgs e)
        {

        }

        private void frmTelaInicial_Load(object sender, EventArgs e)
        {
            PreencherGrid();
            txtNome.Focus();
            txtNome.MaxLength = 20;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Erro: \r\n\r\n Por favor colocar o nome!!","Mario Like Game");
            }
            else
            {
                this.Hide();
                var frmTelaJogo = new frmTelaJogo();
                frmTelaJogo.nomeGamer = txtNome.Text;
                frmTelaJogo.Closed += (s, args) => this.Close();
                frmTelaJogo.Show();
            }          

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgListaRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
