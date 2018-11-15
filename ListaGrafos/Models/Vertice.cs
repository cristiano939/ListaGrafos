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


       

    }

}
