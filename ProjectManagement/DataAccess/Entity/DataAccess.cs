/*      Utility Version 2.0.0.0 for VS 2010     */
/*      Created By      :Nguyen Trong Thanh     */
/*      Created Date    :2009-02-02       */
/*      Update Date     :2013-05-12       */
/*      Description     :                       */
using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Entity
{
    public class DataAccess
    {
        private SqlConnection _connection;
        public SqlConnection Connection { get; set; }

        public SqlTransaction Transaction { get; set; }
        public DataAccess() { }
        public DataSet GetDataSetStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            var ds = new DataSet();
            var command = new SqlCommand(storedProcedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (value[i] != null)
                        command.Parameters.Add(new SqlParameter("@" + param[i], value[i]));
                    else
                        command.Parameters.Add(new SqlParameter("@" + param[i], System.DBNull.Value));
                }
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            da.Fill(ds);
            return ds;
        }
        public DataTable GetDataTable(string sSql)
        {
            var dt = new DataTable();
            var command = new SqlCommand(sSql)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            IDataReader reader = command.ExecuteReader();
            if (reader != null)
            {
                dt.Load(reader);
            }
            return dt;
        }
        public object GetField(string strSql)
        {
            object str = null;
            var command = new SqlCommand(strSql)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };
            IDataReader reader = command.ExecuteReader();
            if (reader != null && reader.Read())
            {
                str = reader.GetValue(0);
            }
            return str;
        }
        public object GetIdMax(string sSql)
        {
            var command = new SqlCommand(sSql)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };
            return command.ExecuteScalar();

        }
        public bool ExecuteNonQuery(string sSql)
        {
            var command = new SqlCommand(sSql)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };

            return Convert.ToBoolean(command.ExecuteNonQuery());
        }
        public object ExecuteNonQueryScalar(string sSql)
        {
            var command = new SqlCommand(sSql)
            {
                CommandType = CommandType.Text,
                Connection = Connection
            };
            command.ExecuteNonQuery();
            command.CommandText = "SELECT @@IDENTITY";
            return command.ExecuteScalar();
        }
        public DataTable GetDataTableStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            var dt = new DataTable();
            var command = new SqlCommand(storedProcedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (value[i] != null)
                        command.Parameters.Add(new SqlParameter("@" + param[i], value[i]));
                    else
                        command.Parameters.Add(new SqlParameter("@" + param[i], System.DBNull.Value));
                }
            }
            IDataReader reader = command.ExecuteReader();
            if (reader != null)
            {
                dt.Load(reader);
            }
            return dt;
        }
        public bool ExecuteNonQueryStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            var dt = new DataTable();
            var command = new SqlCommand(storedProcedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (value[i] != null)
                        command.Parameters.Add(new SqlParameter("@" + param[i], value[i]));
                    else
                        if (param[i].ToString().IndexOf("IMAGE_CONTENT", StringComparison.Ordinal) >= 0)
                        {
                            command.Parameters.Add(new SqlParameter("@" + param[i], SqlDbType.Image));
                            command.Parameters["@" + param[i]].Value = DBNull.Value;
                        }
                        else
                            command.Parameters.Add(new SqlParameter("@" + param[i], System.DBNull.Value));
                }
            }
            return Convert.ToBoolean(command.ExecuteNonQuery());
        }
        public bool ExecuteNonQueryStoredProcedure(object[] param, object[] value, string storedProcedureName, ref decimal returnId, string returnFieldName)
        {
            bool rt;
            var command = new SqlCommand(storedProcedureName)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (value[i] != null)
                        command.Parameters.Add(new SqlParameter("@" + param[i], value[i]));
                    else
                        if (param[i].ToString().IndexOf("IMAGE_CONTENT", StringComparison.Ordinal) >= 0)
                        {
                            command.Parameters.Add(new SqlParameter("@" + param[i], SqlDbType.Image));
                            command.Parameters["@" + param[i]].Value = DBNull.Value;
                        }
                        else
                            command.Parameters.Add(new SqlParameter("@" + param[i], System.DBNull.Value));
                }
            }
            SqlParameter paramId = new SqlParameter("@" + returnFieldName, SqlDbType.Decimal, 10);
            paramId.Direction = ParameterDirection.Output;
            command.Parameters.Add(paramId);
            rt = Convert.ToBoolean(command.ExecuteNonQuery());
            returnId = (decimal)paramId.Value;

            return rt;
        }
    }

}
