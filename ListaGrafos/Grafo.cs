using ListaGrafos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos
{
    public class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        public List<Aresta> Arestas { get; set; }

        public void CriaGrafo(string filePath)
        {
            InicializaGrafo();
            LeituraDeArquivo(filePath);

        }

        public List<Vertice> MelhorCaminho(string origem, string destino)
        {
            var caminho = new List<Vertice>();
            Vertice vOrigem = null, vDestino = null;
            vOrigem = EncontraVerticePorNome(origem);
            vDestino = EncontraVerticePorNome(destino);
            if (vOrigem == null || vDestino == null)
            {
                return null;
            }

            if (isAdjacente(vOrigem, vDestino))
            {

                caminho.Add(vOrigem);
                caminho.Add(vDestino);
                return caminho;
            }
            else
            {

            }
            return null;
        }

        public bool isAtingivel(Vertice vOrigem, Vertice vDestino)
        {
            foreach (var aresta in vOrigem.Arestas)
            {
                if (aresta.VerticeD == vDestino.Aeroporto)
                {
                    return true;
                }

            }

            var vertex = BuscaLargura(vOrigem, vDestino);
            if (vertex != null)
            {
                return true;
            }

            return false;
        }

        private Vertice BuscaLargura(Vertice vOrigem, Vertice vDestino)
        {
            LimpaStatus();
            var visitados = new List<Vertice>();

            visitados.Add(vOrigem);

            bool TodosVisitados = false;
            while (!TodosVisitados) // quase n^3
            {
                foreach (var vertex in visitados)
                {
                    foreach (var aresta in vertex.Arestas)
                    {
                        if (aresta.VerticeD == vDestino.Aeroporto)
                        {
                            return vDestino;
                        }
                    }
                }

                int count = visitados.Count;

                for (int i = 0; i < count; i++)
                {

                    foreach (var aresta in visitados[i].Arestas)
                    {
                        if (EncontraVerticePorNome(aresta.VerticeD, visitados) == null)
                        {
                            visitados.Add(EncontraVerticePorNome(aresta.VerticeD));
                        }
                    }
                    visitados[i].Status = Status.FINALIZADO;

                }
                TodosVisitados = (from vertice in visitados where vertice.Status == Status.FINALIZADO select vertice).Count() == visitados.Count ? true : false;
            }
            return null;
        }

        private void LimpaStatus()
        {
            foreach (var vertex in Vertices)
            {
                vertex.Status = Status.NOVO;
            }
        }

        private bool isAdjacente(Vertice origem, Vertice destino)
        {
            return (from vertice in origem.Arestas where vertice.VerticeD == destino.Aeroporto select vertice).Count() > 0 ? true : false;
        }

        public Vertice EncontraVerticePorNome(string nome)
        {
            return EncontraVerticePorNome(nome, Vertices);
        }

        public Vertice EncontraVerticePorNome(string nome, List<Vertice> listaVertices)
        {
            Vertice vertice = null;
            var vertices = from vertex in listaVertices where vertex.Aeroporto == nome select vertex;
            if (vertices.Count() > 0)
            {
                vertice = vertices.First();
            }
            return vertice;
        }



        private void InicializaGrafo()
        {
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
        }

        private void LeituraDeArquivo(string filePath)
        {

            string line;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    PreencheVertice(line);
                }
            }
        }
        private void PreencheVertice(string line)
        {
            var splitedLine = line.Split('/');
            if (Vertices.Count == 0)
            {
                primeiroVertice(splitedLine);
            }
            else
            {
                InsereVertice(splitedLine);
            }
        }

        private void primeiroVertice(string[] splitedLine)
        {
            Vertices.Add(new Vertice
            {
                Aeroporto = splitedLine[0],
                Arestas = new List<Aresta>()
            });

            Vertices[0].Arestas.Add(new Aresta
            {
                VerticeO = Vertices[0].Aeroporto,
                VerticeD = splitedLine[1],
                Duracao = TimeSpan.Parse(splitedLine[2]),
                Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0,
                Peso = (splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0) + TimeSpan.Parse(splitedLine[2]).TotalHours

            });
            Arestas.Add(new Aresta
            {
                VerticeO = Vertices[0].Aeroporto,
                VerticeD = splitedLine[1],
                Duracao = TimeSpan.Parse(splitedLine[2]),
                Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0,
                Peso = (splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0) + TimeSpan.Parse(splitedLine[2]).TotalHours
            });
        }

        private void InsereVertice(string[] splitedLine)
        {
            var vertexes = from vertex in Vertices where vertex.Aeroporto == splitedLine[0] select vertex;
            if (vertexes.Count() > 0)
            {
                var index = Vertices.IndexOf(vertexes.First());
                Vertices[index].Arestas.Add(new Aresta
                {
                    VerticeO = splitedLine[0],
                    VerticeD = splitedLine[1],
                    Duracao = TimeSpan.Parse(splitedLine[2]),
                    Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0,
                    Peso = (splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0) + TimeSpan.Parse(splitedLine[2]).TotalHours
                });
                Arestas.Add(new Aresta
                {
                    VerticeO = splitedLine[0],
                    VerticeD = splitedLine[1],
                    Duracao = TimeSpan.Parse(splitedLine[2]),
                    Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0,
                    Peso = (splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0) + TimeSpan.Parse(splitedLine[2]).TotalHours
                });
            }
            else
            {
                var vertice = new Vertice
                {
                    Aeroporto = splitedLine[0],
                    Arestas = new List<Aresta>()
                };

                vertice.Arestas.Add(new Aresta
                {
                    VerticeO = vertice.Aeroporto,
                    VerticeD = splitedLine[1],
                    Duracao = TimeSpan.Parse(splitedLine[2]),
                    Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0
                });
                Vertices.Add(vertice);
                Arestas.Add(new Aresta
                {
                    VerticeO = vertice.Aeroporto,
                    VerticeD = splitedLine[1],
                    Duracao = TimeSpan.Parse(splitedLine[2]),
                    Distancia = splitedLine.Length == 4 ? long.Parse(splitedLine[3]) : 0
                });
            }
        }

    }
}

