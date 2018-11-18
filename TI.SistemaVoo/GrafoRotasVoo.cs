using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TI.SistemaVoo.Models;

namespace TI.SistemaVoo
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
                return EncontraMelhorCaminho(origem, destino);
            }

        }

        public List<Vertice> EncontraCutSet(Vertice Origem, Vertice Destino)
        {
            var cutSets = new List<Vertice>();
            var vertexes = new List<Vertice>();
            foreach (var vertice in Vertices)
            {
                vertexes.Add(new Vertice
                {
                    Aeroporto = vertice.Aeroporto,
                    Status = Status.NOVO,
                    Arestas = CopiarArestas(vertice.Arestas)
                });
            }
            if (!isAtingivel(Origem, Destino))
            {
                return cutSets;
            }

            while (vertexes.Count > 0)
            {
                var rVertice = SelecionaVerticeARemover(Origem.Aeroporto, Destino.Aeroporto, vertexes);
                RemoverVertice(rVertice, vertexes);
                if (!isAtingivel(Origem, Destino, vertexes))
                {
                    cutSets.Add(rVertice);
                    return cutSets;
                }
                else
                {
                    cutSets.Add(rVertice);
                }
            }

            return cutSets;
        }

        public DateTime CalculaHoraMaxSaida(DateTime horaDesejada, string origem, string destino)
        {
            if (isAtingivel(EncontraVerticePorNome(origem), EncontraVerticePorNome(destino)))
            {
                TimeSpan intervalo = TimeSpan.FromMinutes(0);
                var caminho = EncontraMelhorCaminho(origem, destino);
                for (int i = 0; i < caminho.Count - 1; i++)
                {
                    if (caminho[i].Aeroporto != caminho.Last().Aeroporto)
                    {
                        foreach (var aresta in caminho[i].Arestas)
                        {
                            if (aresta.VerticeD == caminho[i + 1].Aeroporto)
                            {
                                intervalo = intervalo.Add(aresta.Duracao);
                                break;
                            }
                        }
                    }
                }
                horaDesejada = horaDesejada.Add(-intervalo);
                return horaDesejada;
            }
            else
            {
                return DateTime.MinValue;
            }

        }

        public Grafo ArvoreGeradoraMin(List<Vertice> vertices, List<Aresta> arestas)
        {
            LimpaStatus(vertices);
            var minVertices = new List<Vertice>();
            var minArestas = new List<Aresta>();

            arestas = arestas.OrderBy(aresta => aresta.Distancia).ToList();
            while (minVertices.Count < vertices.Count)
            {
                foreach (var aresta in arestas)
                {
                    if ((from vertice in minVertices where vertice.Aeroporto == aresta.VerticeO select vertice).Count() == 0)
                    {
                        minArestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                        var auxVertex = (from vertice in Vertices where vertice.Aeroporto == aresta.VerticeO select vertice).First();
                        var vertexAdd = new Vertice { Aeroporto = auxVertex.Aeroporto, Status = Status.NOVO, Arestas = new List<Aresta>() };
                        vertexAdd.Arestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                        minVertices.Add(vertexAdd);

                    }
                    if ((from vertice in minVertices where vertice.Aeroporto == aresta.VerticeD select vertice).Count() == 0)
                    {
                        minArestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                        var auxVertex = (from vertice in Vertices where vertice.Aeroporto == aresta.VerticeD select vertice).First();
                        var vertexAdd = new Vertice { Aeroporto = auxVertex.Aeroporto, Status = Status.NOVO, Arestas = new List<Aresta>() };
                        vertexAdd.Arestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                        minVertices.Add(vertexAdd);
                    }
                }

            }
            return new Grafo { Arestas = minArestas, Vertices = minVertices };

        }


        public List<Vertice> EncontraMelhorCaminho(string origem, string destino)
        {
            LimpaStatus();
            bool TodosVisitados = false;
            var visitados = new List<DjikstraVertice>();
            var vOrigem = EncontraVerticePorNome(origem);
            var caminho = new List<Vertice>();
            visitados.Add(new DjikstraVertice
            {
                Aeroporto = vOrigem.Aeroporto,
                Status = Status.NOVO,
                Arestas = CopiarArestas(vOrigem.Arestas),
                Distancia = 0
            });

            while (!TodosVisitados)
            {
                int count = visitados.Count;
                for (int i = 0; i < count; i++)
                {
                    foreach (var aresta in visitados[i].Arestas)
                    {
                        if (EncontraVerticePorNome(aresta.VerticeD, visitados) == null)
                        {
                            var auxVertice = EncontraVerticePorNome(aresta.VerticeD);
                            visitados.Add(new DjikstraVertice
                            {
                                Aeroporto = aresta.VerticeD,
                                Arestas = CopiarArestas(auxVertice.Arestas),
                                Status = Status.VISITADO,
                                Distancia = visitados[i].Distancia + aresta.Distancia,
                                VerticePai = visitados[i].Aeroporto

                            });

                        }
                        else
                        {
                            var auxDVertice = EncontraVerticePorNome(aresta.VerticeD, visitados);
                            var index = visitados.IndexOf(auxDVertice);
                            if (visitados[index].Distancia > visitados[i].Distancia + aresta.Distancia)
                            {
                                visitados[index].Distancia = visitados[i].Distancia + aresta.Distancia;
                                visitados[index].VerticePai = visitados[i].Aeroporto;
                            }
                        }
                    }
                    visitados[i].Status = Status.FINALIZADO;

                }

                TodosVisitados = (from vertice in visitados where vertice.Status == Status.FINALIZADO select vertice).Count() == visitados.Count ? true : false;
            }
            DjikstraVertice verticeAtual = EncontraVerticePorNome(destino, visitados);
            caminho.Add(verticeAtual);
            while (verticeAtual.VerticePai != origem)
            {
                verticeAtual = EncontraVerticePorNome(verticeAtual.VerticePai, visitados);
                caminho.Add(verticeAtual);
            }
            caminho.Add(vOrigem);
            caminho.Reverse();
            return caminho;

        }

        public Vertice EncontraVerticePorNome(string nome)
        {
            return EncontraVerticePorNome(nome, Vertices);
        }

        public Vertice EncontraVerticePorNome(string nome, List<Vertice> listaVertices)
        {
            try
            {
                Vertice vertice = null;
                var vertices = from vertex in listaVertices where vertex.Aeroporto == nome select vertex;
                if (vertices.Count() > 0)
                {
                    vertice = vertices.First();
                }
                return vertice;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public DjikstraVertice EncontraVerticePorNome(string nome, List<DjikstraVertice> listaVertices)
        {
            DjikstraVertice vertice = null;
            var vertices = from vertex in listaVertices where vertex.Aeroporto == nome select vertex;
            if (vertices.Count() > 0)
            {
                vertice = vertices.First();
            }
            return vertice;
        }




        public bool isAtingivel(Vertice vOrigem, Vertice vDestino)
        {
            return isAtingivel(vOrigem, vDestino, Vertices);
        }

        public bool isAtingivel(Vertice vOrigem, Vertice vDestino, List<Vertice> vertices)
        {
            foreach (var aresta in vOrigem.Arestas)
            {
                if (aresta.VerticeD == vDestino.Aeroporto)
                {
                    return true;
                }

            }

            var vertex = BuscaLargura(vOrigem, vDestino, vertices);
            if (vertex != null)
            {
                return true;
            }

            return false;
        }

        private List<Aresta> CopiarArestas(List<Aresta> oArestas)
        {
            var arestaList = new List<Aresta>();
            foreach (var aresta in oArestas)
            {
                arestaList.Add(new Aresta
                {
                    Distancia = aresta.Distancia,
                    Duracao = aresta.Duracao,
                    Peso = aresta.Peso,
                    VerticeD = aresta.VerticeD,
                    VerticeO = aresta.VerticeO
                });
            }
            return arestaList;
        }

        private Vertice SelecionaVerticeARemover(string origem, string destino, List<Vertice> listaVertices)
        {
            var vertexes = new List<Vertice>();
            vertexes.AddRange(listaVertices);
            var vOrigem = EncontraVerticePorNome(origem, vertexes);
            var vDestino = EncontraVerticePorNome(destino, vertexes);
            listaVertices.Remove(vOrigem);
            listaVertices.Remove(vDestino);
            var rVertice = listaVertices.OrderByDescending(x => x.Arestas.Count).First();
            return rVertice;

        }

        private void RemoverVertice(Vertice vertice, List<Vertice> vertices)
        {
            foreach (var vertex in vertices)
            {
                var arestasRemover = new List<Aresta>();
                foreach (var aresta in vertex.Arestas)
                {
                    if (aresta.VerticeD == vertex.Aeroporto)
                    {
                        arestasRemover.Add(aresta);
                    }
                }
                foreach (var aresta in arestasRemover)
                {
                    vertex.Arestas.Remove(aresta);
                }

            }
            vertices.Remove(vertice);
            vertices.RemoveAll(x => x == null);

        }

        private Vertice BuscaLargura(Vertice vOrigem, Vertice vDestino, List<Vertice> listaVertice)
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
                            visitados.Add(EncontraVerticePorNome(aresta.VerticeD, listaVertice));
                            visitados.RemoveAll(x => x == null);
                        }
                    }
                    visitados[i].Status = Status.FINALIZADO;

                }
                TodosVisitados = (from vertice in visitados where vertice.Status == Status.FINALIZADO select vertice).Count() == visitados.Count ? true : false;
            }
            return null;
        }

        private Vertice BuscaLargura(Vertice vOrigem, Vertice vDestino)
        {
            return BuscaLargura(vOrigem, vDestino, Vertices);
        }

        private void LimpaStatus()
        {
            LimpaStatus(Vertices);
        }

        private void LimpaStatus(List<Vertice> vertices)
        {
            foreach (var vertex in vertices)
            {
                vertex.Status = Status.NOVO;
            }
        }

        private bool isAdjacente(Vertice origem, Vertice destino)
        {
            return (from vertice in origem.Arestas where vertice.VerticeD == destino.Aeroporto select vertice).Count() > 0 ? true : false;
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
