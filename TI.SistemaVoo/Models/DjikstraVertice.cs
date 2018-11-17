namespace TI.SistemaVoo.Models
{
    public class DjikstraVertice : Vertice
    {
        public static long MAX_DISTANCE = 999999999;

        public string VerticePai { get; set; }
        public long Distancia { get; set; }
    }
}