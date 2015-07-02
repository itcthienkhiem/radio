using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;

using System.Data.OleDb;


namespace OleDbDAL
{
    public class DataAccess
    {
        public static int ExecuteNonQuery(OleDbConnection oleconn, CommandType cmdType, string sql)
        {
            try
            {
                using (OleDbCommand olecmd = new OleDbCommand(sql, oleconn))
                {
                    olecmd.CommandType = cmdType;
                    return olecmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
        }
        public static DataSet ExecuteDataset(OleDbConnection oleconn, CommandType cmdType, string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                using (OleDbCommand olecmd = new OleDbCommand(sql, oleconn))
                {
                    olecmd.CommandType = cmdType;
                    using (OleDbDataAdapter da = new OleDbDataAdapter(olecmd))
                    {
                        da.Fill(ds);
                    }
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }
    }
}
