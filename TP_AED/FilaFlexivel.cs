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
		public void Adicionar (Celula novo)
		{
			ultimo.Prox = novo;
			ultimo = novo;
			ultimo.Prox = null;
		}
		public Celula Remover ()
		{
			Celula tmp = primeiro.Prox;
			primeiro.Prox = primeiro.Prox.Prox;
			tmp.Prox = null ;
			return tmp;
		}
		public FilaFlexivel ()
		{
			primeiro = new Celula ();
			ultimo = primeiro;
		}
		public void Mostrar ()
		{
			for (Celula i = primeiro; i != ultimo; i = i.Prox)
				Console.WriteLine(i.Elemento);
		}

	}
}
