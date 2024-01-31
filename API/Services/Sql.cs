using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class Sql
    {
        private const string sqlString = "Server=localhost;Database=DB;Trusted_Connection=True;";
        public static SqlConnection connection = new SqlConnection(sqlString);
        public static string message = "";
    }
}
