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
                Console.WriteLine("------------------\n");
            }
            Console.ReadLine();
        }
    }
}
