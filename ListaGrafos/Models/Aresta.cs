using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos.Models
{
    public class Aresta
    {
        public string VerticeO { get; set; }
        public string VerticeD { get; set; }
        public int Peso { get; set; }
        public TimeSpan Duracao { get; set; }
        public long Distancia { get; set; }
    }
}
