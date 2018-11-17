using System;

namespace TI.SistemaVoo.Models
{
    public class Aresta
    {
        public string VerticeO { get; set; }
        public string VerticeD { get; set; }
        public double Peso { get; set; }
        public TimeSpan Duracao { get; set; }
        public long Distancia { get; set; }
    }
}