using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R4
{
    public class SqlHelper : IDisposable
    {
        private string connectionString = "Data Source=UDemirel;Initial Catalog=R4; User ID=sa;Password=123;MultipleActiveResultSets=True;";
        private SqlConnection cnn = null;
        public static SqlDataReader dr;
        private static SqlDataAdapter da;
        private static DataSet ds;

        public SqlHelper()
        {
            this.strConnection = connectionString;
        }
        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
                cnn = null;
            }
            GC.SuppressFinalize(this);
        }

        public string strConnection
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                this.cnn = new SqlConnection(value);
            }
        }

        public void OpenDBConnection()
        {
            if (cnn.State != ConnectionState.Open)
            {
                cnn.Open();
            }
        }

        public void CloseDBConnection()
        {
            if (cnn != null)
            {
                cnn.Close();
            }
        }

        public SqlCommand GetCommand(string sProcName, CommandType cmdtype, List<SqlParameter> prms)
        {
            SqlCommand cmd = new SqlCommand(sProcName, cnn);
            cmd.CommandType = cmdtype;
            cmd.Parameters.Clear();
            if (prms != null)
            {
                foreach (SqlParameter parameter in prms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            return cmd;
        }

        public IslemSonuc<object> ExecuteScalar(string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            return null;
        }

        public IslemSonuc<SqlDataReader> ExecuteDataReader(string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            return null;
        }

        public IslemSonuc<SqlDataAdapter> ExecuteDataAdapter(string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            return null;
        }

        public IslemSonuc<int> ExecuteNoneQuery(string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            return null;
        }

        public IslemSonuc<DataSet> ExecuteDataSet(string commandText, CommandType commandType, List<SqlParameter> parametreler)
        {
            try
            {
                using (ds = new DataSet())
                {
                    SqlCommand cmd = GetCommand(commandText, commandType, parametreler);
                    using (da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return new IslemSonuc<DataSet>
                    {
                        BasariliMi = true,
                        Veri = ds,
                    };
                }
            }
            catch (Exception err)
            {
                return new IslemSonuc<DataSet>
                {
                    BasariliMi = false,
                    Veri = null,
                    Mesaj = err.Message,
                    MesajIcon = MessageBoxIcon.Error,
                };
            }
        }
    }
}
