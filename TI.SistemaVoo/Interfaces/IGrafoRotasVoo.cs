using System;
using System.Collections.Generic;
using System.Text;
using TI.SistemaVoo.Models;

namespace TI.SistemaVoo.Interfaces
{
    public interface IGrafoRotasVoo
    {
        void CriaGrafo(string filePath);
        List<Vertice> MelhorCaminho(string origem, string destino);
        List<Vertice> EncontraCutSet(Vertice Origem, Vertice Destino);
        DateTime CalculaHoraMaxSaida(DateTime horaDesejada, string origem, string destino);
        GrafoRotasVoo ArvoreGeradoraMin(List<Vertice> vertices, List<Aresta> arestas);
        List<Vertice> EncontraMelhorCaminho(string origem, string destino);
        Vertice EncontraVerticePorNome(string nome);
        Vertice EncontraVerticePorNome(string nome, List<Vertice> listaVertices);
        DjikstraVertice EncontraVerticePorNome(string nome, List<DjikstraVertice> listaVertices);
        bool isAtingivel(Vertice vOrigem, Vertice vDestino);
        bool isAtingivel(Vertice vOrigem, Vertice vDestino, List<Vertice> vertices);
    }
}
