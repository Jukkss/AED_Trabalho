using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_AED
{
    class FilaFlexivel
    {
        private Celula primeiro;
        private Celula ultimo;

        public Celula Ultimo
        {
            get { return ultimo; }
            set { ultimo = value; }
        }

        public Celula Primeiro
        {
            get { return primeiro; }
            set { primeiro = value; }
        }

        public void Adicionar(Celula novo)
        {
            if (primeiro == ultimo)
            {
                primeiro.Prox = novo;
                ultimo = novo;
            }
            else
            {
                ultimo.Prox = novo;
                ultimo = novo;
            }
            ultimo.Prox = null;
        }
        public Celula Remover()
        {
            Celula tmp = primeiro.Prox;
            primeiro.Prox = primeiro.Prox.Prox;
            tmp.Prox = null;
            return tmp;
        }
        public FilaFlexivel()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }
        public void Mostrar()
        {
            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
                Console.WriteLine($"{i.Elemento.Nome} {i.Elemento.Media.ToString("n1")} {i.Elemento.OpcoesdeCurso[0]} {i.Elemento.OpcoesdeCurso[1]}");
        }
        public List<Candidato> CandidatosEmLista()
        {
            List<Candidato> lista = new List<Candidato>();
            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
            {
                lista.Add(i.Elemento);
            }
            return lista;
        }
    }
}
