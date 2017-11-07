using System;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Web.Caching;
using ProjectManagement.Models;
using Business;
namespace ProjectManagement.App_Start.Classes
{
    public class PortalConfig
    {

        #region Fields

        private XmlDocument _xmlDoc = null;
        private const string _cacheKey = "portalConfiguration";
        #endregion

        #region Site

        private string _sitemaster = string.Empty;

        public string SiteMaster
        {

            get
            {
                return this._sitemaster;
            }
        }



        private string _sitetheme = "";

        public string SiteTheme
        {
            get
            {
                return this._sitetheme;
            }
        }
        public PortalConfig() { }
        #endregion

        public static string GetValueByKeyName(string keyName)
        {
            var configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = HttpContext.Current.Server.MapPath("~/portal.config");
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            KeyValueConfigurationCollection appSettings = config.AppSettings.Settings;

            if (appSettings.Count > 0)
            {
                return appSettings[keyName].Value;
            }
            return "";
        }
        public static string GetSettingValueById(int Id)
        {
            DataAccessService db = new DataAccessService();
            var dt = db.GetDataTableStoredProcedure(new object[] { "ID" }, new object[] { Id }, "SETTING_GET_BY_ID");
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["SETTING_VALUE"] + "";
            }
            return "";
        }
    }
}


