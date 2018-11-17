using NUnit.Framework;
using Shouldly;

namespace TI.SistemaVoo.Tests
{
    [TestFixture]
    public class GrafosTests
    {



        [TestCase("BH")]
        [TestCase("SP")]
        [TestCase("RJ")]
        [TestCase("PALMAS")]
        [TestCase("UBERLANDIA")]
        [TestCase("OSASCO")]
        [TestCase("FORTALEZA")]
        [TestCase("SALVADOR")]
        public void EncontrarVerticePorNome_Test(string nome)
        {
            Grafo Rotas = new Grafo();
            Grafo Voos = new Grafo();
            Rotas.CriaGrafo("rotas.txt");
            Voos.CriaGrafo("voos.txt");

            var vertice = Rotas.EncontraVerticePorNome(nome);
            vertice.ShouldNotBeNull();
            vertice.Aeroporto.ShouldBe(nome);
            return;
        }


        [TestCase("BH","SP")]
        [TestCase("SP","OSASCO")]
        [TestCase("RJ","PALMAS")]
        [TestCase("PALMAS", "PORTO ALEGRE")]
        [TestCase("UBERLANDIA", "BOA VISTA")]
        [TestCase("OSASCO", "NATAL")]
        [TestCase("FORTALEZA", "CURITIBA")]
        [TestCase("SALVADOR", "PORTO ALEGRE")]
        public void isAtingivel_Test(string origem,string destino)
        {
            Grafo Rotas = new Grafo();
            Grafo Voos = new Grafo();
            Rotas.CriaGrafo("rotas.txt");
            Voos.CriaGrafo("voos.txt");

            var vOrigem = Rotas.EncontraVerticePorNome(origem);
            var vDestino = Rotas.EncontraVerticePorNome(destino);
            vOrigem.ShouldNotBeNull();
            vOrigem.Aeroporto.ShouldBe(origem);

            vDestino.ShouldNotBeNull();
            vDestino.Aeroporto.ShouldBe(destino);

            Rotas.isAtingivel(vOrigem,vDestino).ShouldBeTrue();
            return;
        }

    }
}