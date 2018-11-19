using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TI.SistemaVoo.Models;

namespace TI.SsistemaVoo.Web.Interfaces
{
    public interface IDBManager
    {
        List<Aresta> GetRoutesData();
    }
}
