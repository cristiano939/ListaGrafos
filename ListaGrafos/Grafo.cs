using ListaGrafos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos
{
    public class Grafo
    {
        public List<Vertice> Vertices { get; set; }
        public List<Aresta> Arestas { get; set; }

        public bool isRegular()
        {
            if (hasLoops())
            {
                return false;
            }
            if (hasParalel())
            {
                return false;
            }
            return true;
        }
        public bool hasLoops()
        {
            foreach (var aresta in Arestas)
            {
                if (aresta.VerticeD == aresta.VerticeO)
                {
                    return true;
                }

            }
            return false;
        }

        public bool hasParalel()
        {
            foreach (var arestaO in Arestas)
            {
                foreach (var arestaD in Arestas)
                {
                    if (arestaO.VerticeO == arestaD.VerticeD && arestaO.VerticeD == arestaD.VerticeO && arestaO.Peso != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isCompleto()
        {

            foreach (var verticeO in Vertices)
            {
                var verticeList = new List<Vertice>();
                verticeList.AddRange(Vertices);
                verticeList.Remove(verticeO);
                bool encontrado = false;
                foreach (var verticeD in verticeList)
                {
                    foreach (var aresta in verticeO.Arestas)
                    {
                        if (aresta.VerticeD == verticeD.VerticeId)
                        {
                            encontrado = true;
                            break;
                        }
                    }
                    if (!encontrado)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
