using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace HamburgueriaApp
{
    public static class Conexao
    {
        private static readonly string connString =
        "Host=localhost;Port=5432;Username=postgres;Password=juju2020;Database=TSI_Hamburgueria";

        public static NpgsqlConnection ObterConexao()
        {
            return new NpgsqlConnection(connString);
        }
    }
}
