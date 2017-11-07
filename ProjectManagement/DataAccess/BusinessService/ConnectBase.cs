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
    public class ConnectBase
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        }
        protected static void Open(SqlConnection con)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        protected static void Close(SqlConnection con)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}

