using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos.Models
{
    public class Vertice
    {
        public int VerticeId { get; set; }
        public Item Item { get; set; }
        public List<Aresta> Arestas { get; set; }
        public int Grau { get; set; }


        public bool isAdjacente(Vertice v1, Vertice v2)
        {
            bool adj1 = false, adj2 = false;
            foreach (var aresta in v1.Arestas)
            {
                if (aresta.VerticeD == v2.VerticeId)
                {
                    adj1 = true;
                }
            }
            foreach (var aresta in v2.Arestas)
            {
                if (aresta.VerticeD == v1.VerticeId)
                {
                    adj2 = true;
                }
            }
            if (adj1 == true && adj2 == true)
            {
                return true;
            }
            return false;
        }

        public int getGrau(Vertice v)
        {
            int grau = v.Arestas.Count;

            foreach (var arestaChegada in Arestas)
            {
                foreach (var arestaSaida in v.Arestas)
                    if (arestaChegada.VerticeD == v.VerticeId && arestaChegada.VerticeO != arestaSaida.VerticeD)
                    {
                        grau += 1;
                    }
            }
            return grau;
        }

    }

}
