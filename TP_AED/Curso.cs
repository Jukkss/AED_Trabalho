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

        public void Insercao(Candidato[] candidatos, int op)
        {
            int j;
            Candidato temp;
            switch (op)
            {
                case 1:
                    for (int i = 1; i < candidatos.Length; i++)
                    {
                        temp = candidatos[i];
                        j = i - 1;
                        while (j >= 0 && temp.Media > candidatos[j].Media)
                        {
                            candidatos[j + 1] = candidatos[j];
                            j--;
                        }
                        candidatos[j + 1] = temp;
                    }
                    break;
                case 2:
                    for (int i = 1; i < candidatos.Length; i++)
                    {
                        temp = candidatos[i];
                        j = i - 1;
                        while (j >= 0 && temp.Notas[0] > candidatos[j].Notas[0])
                        {
                            candidatos[j + 1] = candidatos[j];
                            j--;
                        }
                        candidatos[j + 1] = temp;
                    }
                    break;
                    
            }
        }
        public void PreencherLista (List<Candidato> listacandidatos, Candidato[]vetCandidatos)
        {
            for (int i = 0; i < vetCandidatos.Length;i++)
                listacandidatos.Add(vetCandidatos[i]);
        }

    }
}
