using NUnit.Framework;
using Shouldly;
using System;

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


        [TestCase("BH", "SP")]
        [TestCase("SP", "OSASCO")]
        [TestCase("RJ", "PALMAS")]
        [TestCase("PALMAS", "PORTO ALEGRE")]
        [TestCase("UBERLANDIA", "BOA VISTA")]
        [TestCase("OSASCO", "NATAL")]
        [TestCase("FORTALEZA", "CURITIBA")]
        [TestCase("SALVADOR", "PORTO ALEGRE")]
        public void isAtingivel_Test(string origem, string destino)
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

            Rotas.isAtingivel(vOrigem, vDestino).ShouldBeTrue();
            return;
        }

        [TestCase("BH", "SP")]
        [TestCase("SP", "OSASCO")]
        [TestCase("RJ", "PALMAS")]
        [TestCase("PALMAS", "PORTO ALEGRE")]
        [TestCase("UBERLANDIA", "BOA VISTA")]
        [TestCase("OSASCO", "NATAL")]
        [TestCase("FORTALEZA", "CURITIBA")]
        [TestCase("SALVADOR", "PORTO ALEGRE")]
        public void MelhorCaminho_Test(string origem, string destino)
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

            Rotas.MelhorCaminho(origem, destino).Count.ShouldBeGreaterThan(0);
            return;
        }

        [TestCase("BH", "SP")]
        [TestCase("SP", "OSASCO")]
        [TestCase("RJ", "PALMAS")]
        [TestCase("PALMAS", "PORTO ALEGRE")]
        [TestCase("UBERLANDIA", "BOA VISTA")]
        [TestCase("OSASCO", "NATAL")]
        [TestCase("FORTALEZA", "CURITIBA")]
        [TestCase("SALVADOR", "PORTO ALEGRE")]
        public void EncontrarCutSet_Test(string origem, string destino)
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

            Rotas.EncontraCutSet(vOrigem, vDestino).Count.ShouldBeGreaterThan(0);
            return;
        }

        [TestCase("2018-11-18 20:05:20","BH", "SP")]
        [TestCase("2018-11-18 20:05:20", "BH", "OSASCO")]
        [TestCase("2018-11-18 20:05:20", "SP", "OSASCO")]
        [TestCase("2018-11-18 20:05:20", "RJ", "PALMAS")]
        [TestCase("2018-11-18 20:05:20", "PALMAS", "PORTO ALEGRE")]
        [TestCase("2018-11-18 20:05:20", "UBERLANDIA", "BOA VISTA")]
        [TestCase("2018-11-18 20:05:20", "OSASCO", "NATAL")]
        [TestCase("2018-11-18 20:05:20", "FORTALEZA", "CURITIBA")]
        [TestCase("2018-11-18 20:05:20", "SALVADOR", "PORTO ALEGRE")]
        public void EncontrarHorarioMaximo_Test(DateTime horarioMax, string origem, string destino)
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

            Rotas.CalculaHoraMaxSaida(horarioMax, origem, destino).ShouldNotBeNull();
            Rotas.CalculaHoraMaxSaida(horarioMax, origem, destino).ShouldNotBe(DateTime.MinValue);
            return;
        }

    }
}