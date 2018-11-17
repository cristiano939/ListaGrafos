using System.Collections.Generic;

namespace TI.SistemaVoo.Models
{
    public class Vertice
    {
        public string Aeroporto { get; set; }
        public List<Aresta> Arestas { get; set; }
        public Status Status { get; set; }
    }
}