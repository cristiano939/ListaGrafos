using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGrafos
{
    class Program
    {
        public static Grafo Rotas;
        public static Grafo Voos;

        static void Main(string[] args)
        {
            Rotas = new Grafo();
            Voos = new Grafo();

            Rotas.CriaGrafo("rotas.txt");
            Voos.CriaGrafo("voos.txt");

            var BH = Rotas.EncontraVerticePorNome("BH");
            var OSASCO = Rotas.EncontraVerticePorNome("OSASCO");
            var atingivel = Rotas.isAtingivel(BH, OSASCO);

            var melhorCaminho = Rotas.MelhorCaminho(BH.Aeroporto, OSASCO.Aeroporto);
            var cutSet = Rotas.EncontraCutSet(BH, OSASCO);
        }
    }
}
