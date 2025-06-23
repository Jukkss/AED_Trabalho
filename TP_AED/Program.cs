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
        static void PreencherVagas(Candidato[] candidatos, Dictionary<int, Curso> cursosDic)
        {
            // Soares
            Quicksort(candidatos,0,candidatos.Length-1);

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
        static void Quicksort(Candidato[] candidatos, int esq, int dir)
        {
            int i = esq, j = dir;
            Candidato pivo = candidatos[(esq + dir) / 2]; 

            while (i <= j)
            {
                while (CompararCandidatos(candidatos[i], pivo) > 0) // i avança até achar um elemento menor que o pivo
                    i++;
                while (CompararCandidatos(candidatos[j], pivo) < 0) // j recua até achar um elemento maior que o pivo
                    j--;

                if (i <= j) // Troca quando i e j param, i (para caso ache um elemento menor que o pivo na esqueda); j (para caso ache um elemento maior que o pivo na direita)
                {
                    Trocar(candidatos, i, j); // Troca os elementos
                    i++;
                    j--;
                }
            }

            if (esq < j)
                Quicksort(candidatos, esq, j);
            if (i < dir)
                Quicksort(candidatos, i, dir);
        }
        // Soares
        static int CompararCandidatos(Candidato a, Candidato b)
        {
            int cmpMedia = a.Media.CompareTo(b.Media); // Compara a media de dois candidatos, caso a > b retorna 1; caso a = b retorna 0; caso a < b retorna -1
            if (cmpMedia != 0) // Caso as medias não sejam iguais, retornam o indice equivalente para a diferença entre os elementos (1 ou -1)
                return cmpMedia;

            return a.Notas[0].CompareTo(b.Notas[0]); // Com médias iguais, o retorno será do comparativo entre as redações dos candidatos, desempate
        }
        // Soares
        static void Trocar(Candidato[] vet, int i, int j)
        {
            Candidato temp = vet[i];
            vet[i] = vet[j];
            vet[j] = temp;
        }
        // Soares
        static double CalcularNotaDeCorte(List<Candidato> selecionados)
        {
            // Comando simples para percorrer os selecionados e retornar a menor nota entre eles, independe da quantidade de selecionados
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
        static void GerarArquivoSaida(Dictionary<int, Curso> cursosDic)
        {
            StreamWriter sw = new StreamWriter("saida.txt", false, Encoding.UTF8);

            foreach (KeyValuePair<int, Curso> keyValuePair in cursosDic) // Loop para percorrer cada curso pela sua chave
            {
                Curso curso = keyValuePair.Value;

                // Transforma a lista de selecionados em um vetor para percorrer dentro do foreach
                Candidato[] selecionados = curso.ListaSelecionados.ToArray();
                Candidato[] filaEspera = curso.FilaDeEspera.CandidatosEmLista().ToArray();

                // Calcula nota de corte do curso respectivo
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
                Console.WriteLine($"\nNota de corte: {CalcularNotaDeCorte(keyValuePair.Value.ListaSelecionados).ToString("n1")}");
                Console.WriteLine("------------------\n");
            }
            GerarArquivoSaida(cursosDic);
            Console.ReadLine();
        }
    }
}
