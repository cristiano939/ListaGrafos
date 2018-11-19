using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TI.SistemaVoo.Models;
using TI.SsistemaVoo.Web.Interfaces;

namespace TI.SsistemaVoo.Web.Services
{
    public class DBManager : IDBManager
    {
        private readonly IConfiguration _configuration;

        public DBManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Aresta> GetRoutesData()
        {
            var arestas = new List<Aresta>();
            using (var connection = new SqlConnection(_configuration["dbconfig:connectionstring"]))
            {
                var cmd = new SqlCommand("[GetRoutesData]", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                                       
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Routes");
                    DataTable dt = ds.Tables["Routes"];

                    foreach (DataRow row in dt.Rows)
                    {
                        arestas.Add
                            (
                            new Aresta
                            {
                                Distancia = Convert.ToInt32(row["Distancia"]),
                                Duracao = TimeSpan.FromMinutes(Convert.ToInt32(row["Duracao"])),
                                VerticeD = Convert.ToString(row["AeroportoD"]),
                                VerticeO = Convert.ToString(row["AeroportoO"]),

                            }
                            );
                    }
                }

            }
            return arestas;
        }

    }
}
