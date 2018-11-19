using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TI.SistemaVoo;

namespace TI.SsistemaVoo.Web.Interfaces
{
    public interface IGrafoHandler
    {
        GrafoRotasVoo GetGrafoFromDB();
    }
}
