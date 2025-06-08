===============================
Sistema de Seleção - Univ. Stark
===============================

Este projeto simula o sistema de seleção de candidatos da Universidade Stark, com base em critérios definidos de pontuação e prioridade de curso.

-------------------------------
📝 Descrição Geral
-------------------------------
Na Universidade Stark, cada curso possui um número limitado de vagas, mas uma fila de espera ilimitada.

Cada candidato realiza provas de:
- Redação
- Matemática
- Linguagens

A média simples dessas três provas determina a classificação do candidato:
(média = (Redação + Matemática + Linguagens) / 3)

Em caso de empate na média, o critério de desempate é a maior nota na Redação.

Cada candidato escolhe duas opções de curso.

-------------------------------
📌 Regras de Seleção
-------------------------------
- Se for selecionado na 1ª opção:
  -> entra apenas na lista de selecionados da 1ª opção.
- Se for selecionado na 2ª opção:
  -> entra na lista de selecionados da 2ª opção e na fila de espera da 1ª.
- Se não for selecionado em nenhuma:
  -> entra na fila de espera das duas opções.
- Se for selecionado nas duas opções:
  -> entra apenas na lista da 1ª opção (a vaga da 2ª é liberada).

-------------------------------
📂 Entrada
-------------------------------
O programa deve ler um arquivo `entrada.txt` contendo:
- Cursos: código, nome e número de vagas
- Candidatos: nome, notas (Redação, Matemática, Linguagens), opções 1 e 2

-------------------------------
📤 Saída
-------------------------------
Geração do arquivo `saida.txt` com:
1. Nome do curso e nota de corte (menor média entre os selecionados)
2. Lista de candidatos selecionados (nome e média) em ordem decrescente
3. Lista da fila de espera (nome e média), também ordenada
4. Separação entre cursos com linha em branco

-------------------------------
🛠️ Tecnologias e Estrutura
-------------------------------
- Linguagem: C#
- Estrutura de dados: Listas e Fila Flexível
- Organização por classes: Curso, Candidato, Fila, etc.
- Ordenação eficiente com comparadores personalizados

-------------------------------
📎 Objetivo
-------------------------------
Simular um sistema automatizado e justo de seleção universitária, respeitando critérios do edital e regras de desempate.

-------------------------------
👥 Desenvolvido por
-------------------------------
João Victor Soares Souza - Sistemas de Informação
João Victor Fróis - Sistemas de Informação
Lucas Gabriel Ferreira - Sistemas de Informação 
