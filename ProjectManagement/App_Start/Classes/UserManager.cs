using System;
using System.Data;
using System.Configuration;
using System.Data.Entity;
//using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ProjectManagement.Controllers;
using ProjectManagement.Models;

//using System.Xml.Linq;


namespace ProjectManagement.App_Start.Classes
{
    public static class UserManager
    {
        private static ProjectManagementEntities db = new ProjectManagementEntities();
        public static decimal totalOnlineUser = 25;
        //public static decimal totalHitcount = 0;
        public static bool Authenticated
        {
            get
            {
                Identification iden = Identification.GetInstance();
                return iden.IsAuthenticated;
            }
        }

        public static string GetUserName
        {
            get
            {
                Identification iden = Identification.GetInstance();
                return iden.GetUserName();
            }
        }
        public static decimal GetUserId
        {
            get
            {
                Identification iden = Identification.GetInstance();
                return iden.GetUserId();
            }
        }

        public static decimal OnlineUser
        {
            get { return totalOnlineUser; }            
        }
    }
}
