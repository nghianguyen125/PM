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

namespace Business
{
    public class DataAccessService : ConnectBase
    {
        public DataSet GetDataSetStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            var rt = new DataSet();
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.GetDataSetStoredProcedure(param, value, storedProcedureName);
                connection.Close();
            }
            return rt;
        }
        public DataTable GetDataTable(string sSql)
        {
            DataTable rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.GetDataTable(sSql);
                connection.Close();
            }
            return rt;
        }
        public object GetField(string strSql)
        {
            object rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.GetField(strSql);
                connection.Close();
            }
            return rt;
        }
        public object GetIdMax(string strSql)
        {
            object rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.GetIdMax(strSql);
                connection.Close();
            }
            return rt;
        }
        public bool ExecuteNonQuery(string sSql)
        {
            bool rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.ExecuteNonQuery(sSql);
                connection.Close();
            }
            return rt;
        }
        public object ExecuteNonQueryScalar(string sSql)
        {
            object rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.ExecuteNonQueryScalar(sSql);
                connection.Close();
            }
            return rt;
        }
        public DataTable GetDataTableStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            var rt = new DataTable();
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.GetDataTableStoredProcedure(param, value, storedProcedureName);
                connection.Close();
            }
            return rt;
        }
        public bool ExecuteNonQueryStoredProcedure(object[] param, object[] value, string storedProcedureName)
        {
            bool rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.ExecuteNonQueryStoredProcedure(param, value, storedProcedureName);
                connection.Close();
            }
            return rt;
        }
        public bool ExecuteNonQueryStoredProcedure(object[] param, object[] value, string storedProcedureName, ref decimal returnId, string returnFieldName)
        {
            bool rt;
            var entity = new Entity.DataAccess();
            using (SqlConnection connection = GetConnection())
            {
                entity.Connection = connection;
                connection.Open();
                rt = entity.ExecuteNonQueryStoredProcedure(param, value, storedProcedureName, ref returnId, returnFieldName);
                connection.Close();
            }
            return rt;
        }
    }

}
