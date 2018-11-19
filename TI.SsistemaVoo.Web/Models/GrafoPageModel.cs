using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TI.SistemaVoo;

namespace TI.SsistemaVoo.Web.Models
{
    public class GrafoPageModel : PageModel
    {
        public GrafoRotasVoo Grafo { get; set; }

        public GrafoPageModel()
        {

        }

        public GrafoPageModel(GrafoRotasVoo grafo)
        {
            Grafo = grafo;
        }
    }
}
