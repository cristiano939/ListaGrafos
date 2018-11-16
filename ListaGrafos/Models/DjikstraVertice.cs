using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos.Models
{
    public class DjikstraVertice : Vertice
    {
        public static long MAX_DISTANCE = 999999999;

        public string VerticePai { get; set; }
        public long Distancia { get; set; }
    }
}
