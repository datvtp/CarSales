using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.utils
{
    public class DBUtils
    {
        public static SqlConnection GetConnection()
        {
            string strConnection = "server=.;database=CarManagement;uid=sa;pwd=gooner";
            SqlConnection cnn = new SqlConnection(strConnection);
            return cnn;
        }
    }
}
