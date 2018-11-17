using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos.Models
{
    public class Vertice
    {
        public string Aeroporto { get; set; }
        public List<Aresta> Arestas { get; set; }
        public Status Status { get; set; }
    }

}
