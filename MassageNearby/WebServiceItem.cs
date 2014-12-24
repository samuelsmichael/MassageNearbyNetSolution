using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace MassageNearby {
    [Serializable]
    public abstract class WebServiceItem {
        protected abstract WebServiceItem objectFromDatasetRow(DataRow dr);

        protected string ConnectionString {
            get {
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["MassageNearby"]];
                return settings.ConnectionString;
            }
        }
        protected DataSet getDataSet(SqlCommand command) {
            return Common.Utils.getDataSet(command,ConnectionString);
        }
        protected System.Data.DataSet getDataSet() {
            string query = "Select * from " + ConfigurationManager.AppSettings[GetType().Name + "TableName"] + (getIncludeOnlyWhereIsApprovedEqual1()?" WHERE isApproved=1":"");
            return Common.Utils.getDataSetFromQuery(query, ConnectionString);
        }
        private bool getIncludeOnlyWhereIsApprovedEqual1() {
            return Common.Utils.ObjectToBool(ConfigurationManager.AppSettings[GetType().Name + "IncludeOnlyWhereIsApprovedEqual1"]);
        }
    }
}
