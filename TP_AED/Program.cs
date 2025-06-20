using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TP_AED
{
    internal class Program
    {
        static Dictionary<int, Curso> LerCursos()
        {
            try
            {
                StreamReader lerEntrada = new StreamReader("entrada.txt", Encoding.UTF8);
                string linha = lerEntrada.ReadLine();
                string[] numeros = linha.Split(';');

                Dictionary<int, Curso> cursosDic = new Dictionary<int, Curso>();

                for (int i = 0; i < int.Parse(numeros[0]); i++)
                {
                    linha = lerEntrada.ReadLine();
                    string[] curso = linha.Split(';');
                    cursosDic.Add(int.Parse(curso[0]), new Curso(int.Parse(curso[0]), curso[1], int.Parse(curso[2])));
                }
                lerEntrada.Close();
                return cursosDic;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static Candidato[] LerCandidatos()
        {
            try
            {
                StreamReader lerEntrada = new StreamReader("entrada.txt", Encoding.UTF8);
                string linha = lerEntrada.ReadLine();
                string[] numeros = linha.Split(';');

                Candidato[] candidatosVet = new Candidato[int.Parse(numeros[1])];
                for (int i = 0; i < int.Parse(numeros[0]); i++)
                    lerEntrada.ReadLine();

                for (int i = 0; i < candidatosVet.Length; i++)
                {
                    linha = lerEntrada.ReadLine();
                    string[] candidato = linha.Split(';');
                    candidatosVet[i] = new Candidato(candidato[0], double.Parse(candidato[1]), double.Parse(candidato[2]), double.Parse(candidato[3]), int.Parse(candidato[4]), int.Parse(candidato[5]));
                    //Soares
                    candidatosVet[i].IndiceOriginal = i;
                }
                lerEntrada.Close();

                return candidatosVet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static void Insercao(Candidato[] candidatos)
        {
            for (int i = 1; i < candidatos.Length; i++)
            {
                Candidato temp = candidatos[i];
                int j = i - 1;

                while (j >= 0 &&
                      (candidatos[j].Media < temp.Media || (candidatos[j].Media == temp.Media && candidatos[j].Notas[0] < temp.Notas[0])
                      ))
                {
                    candidatos[j + 1] = candidatos[j];
                    j--;
                }
                candidatos[j + 1] = temp;
            }
        }
        static void PreencherVagas(Candidato[] candidatos, Dictionary<int, Curso> cursosDic)
        {
            Insercao(candidatos);

            foreach (Candidato c in candidatos)
            {
                Curso op1 = cursosDic[c.OpcoesdeCurso[0]];
                Curso op2 = cursosDic[c.OpcoesdeCurso[1]];

                if ((op1.VagasDisponiveis > 0) && (op2.VagasDisponiveis > 0))
                {
                    op1.AdicionarSelecionado(c);
                    op1.VagasDisponiveis--;
                }
                else if (op1.VagasDisponiveis > 0)
                {
                    op1.AdicionarSelecionado(c);
                    op1.VagasDisponiveis--;
                }
                else if (op2.VagasDisponiveis > 0)
                {
                    op2.AdicionarSelecionado(c);
                    op1.AdicionarFilaEspera(c);
                    op2.VagasDisponiveis--;
                }
                else
                {
                    op1.AdicionarFilaEspera(c);
                    op2.AdicionarFilaEspera(c);
                }
            }
        }
        // Soares
        static void QuickSort(Candidato[] vet, int esquerda, int direita)
        {
            if (esquerda < direita)
            {
                int pivo = Particionar(vet, esquerda, direita);
                QuickSort(vet, esquerda, pivo - 1);
                QuickSort(vet, pivo + 1, direita);
            }
        }
        // Soares
        static int Particionar(Candidato[] vet, int esquerda, int direita)
        {
            Candidato pivo = vet[direita];
            int i = esquerda - 1;

            for (int j = esquerda; j < direita; j++)
            {
                if (CompararCandidatos(vet[j], pivo) > 0) 
                {
                    i++;
                    Trocar(vet, i, j);
                }
            }
            Trocar(vet, i + 1, direita);
            return i + 1;
        }
        // Soares
        static int CompararCandidatos(Candidato a, Candidato b)
        {
            int cmpMedia = b.Media.CompareTo(a.Media);
            if (cmpMedia != 0)
                return cmpMedia;

            int cmpNotaRed = b.Notas[0].CompareTo(a.Notas[0]); 
            if (cmpNotaRed != 0)
                return cmpNotaRed;

            return a.IndiceOriginal.CompareTo(b.IndiceOriginal); 
        }
        // Soares
        static void Trocar(Candidato[] vet, int i, int j)
        {
            Candidato temp = vet[i];
            vet[i] = vet[j];
            vet[j] = temp;
        }
        // Soares
        static void OrdenarCandidatosQuick(Candidato[] vet)
        {
            QuickSort(vet, 0, vet.Length - 1);
        }
        // Soares
        static double CalcularNotaDeCorte(List<Candidato> selecionados)
        {
            if (selecionados.Count == 0)
                return 0.0;

            double menorMedia = selecionados[0].Media;
            foreach (Candidato c in selecionados)
            {
                if (c.Media < menorMedia)
                    menorMedia = c.Media;
            }
            return menorMedia;
        }
        // Soares
        static void GerarArquivoSaida(Dictionary<int, Curso> cursosDic)
        {
            StreamWriter sw = new StreamWriter("saida.txt", false, Encoding.UTF8);

            foreach (KeyValuePair<int, Curso> keyValuePair in cursosDic)
            {
                Curso curso = keyValuePair.Value;

                Candidato[] selecionados = curso.ListaSelecionados.ToArray();
                OrdenarCandidatosQuick(selecionados);

                Candidato[] filaEspera = curso.FilaDeEspera.CandidatosEmLista().ToArray();
                OrdenarCandidatosQuick(filaEspera);

                double notaDeCorte = CalcularNotaDeCorte(curso.ListaSelecionados);
                sw.WriteLine($"{curso.Nome} {notaDeCorte.ToString("n1")}");
                sw.WriteLine("Selecionados:");
                foreach (Candidato c in selecionados)
                    sw.WriteLine($"{c.Nome} {c.Media.ToString("n1")}");

                sw.WriteLine("Fila de Espera:");    
                foreach (Candidato c in filaEspera)
                    sw.WriteLine($"{c.Nome} {c.Media.ToString("n1")}");

                sw.WriteLine();
            }
            sw.Close();
        }

        static void Main(string[] args)
        {
            Dictionary<int, Curso> cursosDic = LerCursos();

            Candidato[] candidatosVet = LerCandidatos();

            PreencherVagas(candidatosVet, cursosDic);

            foreach(Candidato c in candidatosVet)
                Console.WriteLine($"{c.Nome} {c.Media.ToString("n1")} {c.Notas[0]}");
            Console.WriteLine();

            foreach (KeyValuePair<int, Curso> keyValuePair in cursosDic)
            {
                Console.WriteLine($"{keyValuePair.Key} {keyValuePair.Value.Nome}:");
                foreach (Candidato c in keyValuePair.Value.ListaSelecionados)
                {
                    Console.WriteLine($"{c.Nome} {c.Media.ToString("n1")} {c.OpcoesdeCurso[0]} {c.OpcoesdeCurso[1]}");
                }
                Console.WriteLine($"\nFila de Espera:");
                keyValuePair.Value.FilaDeEspera.Mostrar();
                // Soares
                Console.WriteLine($"\nNota de corte: {CalcularNotaDeCorte(keyValuePair.Value.ListaSelecionados).ToString("n1")}");
                Console.WriteLine("------------------\n");
            }
            // Soares
            GerarArquivoSaida(cursosDic);
            Console.ReadLine();
        }
    }
}
