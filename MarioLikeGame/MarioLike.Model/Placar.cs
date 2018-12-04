using System;
using System.Collections.Generic;
using System.Text;

namespace MarioLike.Model
{
    public class Placar
    {
        private int idJogador;
        private string nomeJogador;
        private int score;
        private DateTime dataScore;
        private string tempo;
        public Placar()
        {
        }

        public Placar(int idJogador, string nomeJogador, int score, DateTime dataScore,string tempoJogador)
        {
            this.IdJogador = idJogador;
            this.NomeJogador = nomeJogador;
            this.Score = score;
            this.DataScore = dataScore;
            this.TempoJogador = tempoJogador;
        }

        public int IdJogador { get => idJogador; set => idJogador = value; }
        public string NomeJogador { get => nomeJogador; set => nomeJogador = value; }
        public int Score { get => score; set => score = value; }
        public DateTime DataScore { get => dataScore; set => dataScore = value; }
        public string TempoJogador { get => tempo; set => tempo = value; }
    }
}
