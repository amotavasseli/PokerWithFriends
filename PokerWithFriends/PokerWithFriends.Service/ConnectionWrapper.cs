using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service
{
    public class ConnectionWrapper
    {
        public SqlCommand SqlWrapper(string proc, SqlConnection con)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = proc; cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
    }
}
