using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_AED
{
    class Candidato
    {
        private string nome;
        private double[] notas = new double[3];
        private int[] opcoesDeCurso = new int[2];
        private double media;
        //Soares
        public int IndiceOriginal { get; set; }

        public double Media
        {
            get { return media; }
        }

        public int[] OpcoesdeCurso
        {
            get { return opcoesDeCurso; }
            set { opcoesDeCurso = value; }
        }

        public double[] Notas
        {
            get { return notas; }
            set { notas = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Candidato(string nome, double notaRedacao, double notaMatematica, double notaLinguagens, int codPrimeiraop, int codSegundaop)
        {
            this.nome = nome;
            notas[0] = notaRedacao;
            notas[1] = notaMatematica;
            notas[2] = notaLinguagens;
            opcoesDeCurso[0] = codPrimeiraop;
            opcoesDeCurso[1] = codSegundaop;
            media = (notaRedacao + notaMatematica + notaLinguagens) / 3.0;
        }
    }
}
