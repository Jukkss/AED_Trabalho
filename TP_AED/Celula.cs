using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_AED
{
    class Celula
    {
        private Celula prox;
        private Candidato elemento;

        public Candidato Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }

        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public Celula(Candidato candidato)
        {
            elemento = candidato;
            prox = null;
        }
        public Celula()
        {
            elemento = null;
            prox = null;
        }

    }
}
