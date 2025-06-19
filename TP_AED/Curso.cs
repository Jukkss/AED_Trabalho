using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_AED
{
    class Curso
    {
        private int codigo;
        private string nome;
        private int vagasDisponiveis;
        private List<Candidato> listaSelecionados;
        private FilaFlexivel filaDeEspera;
        private Candidato[] candidatos;

        public int VagasDisponiveis
        {
            get { return vagasDisponiveis; }
            set { vagasDisponiveis = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public List<Candidato> ListaSelecionados
        {
            get { return listaSelecionados; }
        }

        public FilaFlexivel FilaDeEspera
        {
            get { return filaDeEspera; }
        }

        public Curso(int codigo, string nome, int vagasDisponiveis)
        {
            listaSelecionados = new List<Candidato>();
            filaDeEspera = new FilaFlexivel();
            this.codigo = codigo;
            this.nome = nome;
            this.vagasDisponiveis = vagasDisponiveis;
        }

        public void AdicionarSelecionado(Candidato candidato)
        {
            listaSelecionados.Add(candidato);
        }

        public void AdicionarFilaEspera(Candidato candidato)
        {
            Celula celula = new Celula(candidato);
            filaDeEspera.Adicionar(celula);
        }
    }
}
