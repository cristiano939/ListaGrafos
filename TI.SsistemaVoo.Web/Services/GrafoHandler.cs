using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TI.SistemaVoo;
using TI.SistemaVoo.Models;
using TI.SsistemaVoo.Web.Interfaces;

namespace TI.SsistemaVoo.Web.Services
{
    public class GrafoHandler : IGrafoHandler
    {
        private readonly IDBManager _dbManager;

        public GrafoHandler(IDBManager dbManager)
        {
            _dbManager = dbManager;
        }

        public GrafoRotasVoo GetGrafoFromDB()
        {
            var grafoVooRota = new GrafoRotasVoo();
            var arestas = _dbManager.GetRoutesData();
            var vertices = new List<Vertice>();
            foreach (var aresta in arestas)
            {
                if ((from auxVertice in vertices where auxVertice.Aeroporto == aresta.VerticeO select auxVertice).Count() == 0)
                {
                    var vertex = new Vertice { Aeroporto = aresta.VerticeO, Status = Status.NOVO, Arestas = new List<Aresta>() };
                    vertex.Arestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                    vertices.Add(vertex);
                }
                else
                {
                    var vertice = grafoVooRota.EncontraVerticePorNome(aresta.VerticeO, vertices);
                    var index = vertices.IndexOf(vertice);
                    vertices[index].Arestas.Add(new Aresta { Distancia = aresta.Distancia, Duracao = aresta.Duracao, Peso = aresta.Peso, VerticeD = aresta.VerticeD, VerticeO = aresta.VerticeO });
                }
            }
            grafoVooRota.Vertices.AddRange(vertices);
            grafoVooRota.Arestas.AddRange(arestas);
            return grafoVooRota;
        }
    }
}
