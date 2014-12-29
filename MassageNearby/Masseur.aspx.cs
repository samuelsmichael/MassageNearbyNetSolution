using System;
using System.IO;
using Newtonsoft.Json;
using Common;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MassageNearby {
    public partial class Masseur1 : System.Web.UI.Page {
        private string ConnectionString {
            get {
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["MassageNearby"]];
                return settings.ConnectionString;
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            String name = Request.QueryString["Name"];
            String url = Request.QueryString["URL"];
            String masseurId = Request.QueryString["MasseurId"];
            String action=Request.QueryString["Action"];
            MemoryStream ms = new MemoryStream();
            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            if (Common.Utils.isNothing(action)) { // for legacy
                SqlCommand command = new SqlCommand("uspMasseurGet");
                SqlParameter parm = new SqlParameter("Name", SqlDbType.VarChar);
                parm.Value = name;
                command.Parameters.Add(parm);
                if (url != null) {
                    SqlParameter parm2 = new SqlParameter("URL", SqlDbType.VarChar);
                    parm2.Value = url;
                    command.Parameters.Add(parm2);
                }
                if (masseurId != null) {
                    SqlParameter parm3 = new SqlParameter("MasseurId", SqlDbType.Int);
                    parm3.Value = Convert.ToInt32(masseurId);
                    command.Parameters.Add(parm3);
                }
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                    new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                    serializer.Serialize(jsonTextWriter, new Masseur().buildList(name, command));
                    jsonTextWriter.Flush();
                }
                Utils.jsonSerializeStep2(ms, Response);
            }
            else {
                if (action.ToLower().Equals("set")) {
                    SqlCommand cmd = new SqlCommand("uspMasseurSet");
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["UserId"]);
                    string masseurname = Request.QueryString["Name"];
                    if (Common.Utils.isNothingNot(masseurname)) {
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = masseurname;
                    }
                    string longitude = Request.QueryString["Longitude"];
                    if (Common.Utils.isNothingNot(longitude)) {
                        cmd.Parameters.Add("@Longitude", SqlDbType.Decimal).Value = Convert.ToDecimal(longitude);
                    }
                    string latitude = Request.QueryString["Latitude"];
                    if (Common.Utils.isNothingNot(latitude)) {
                        cmd.Parameters.Add("@Latitude", SqlDbType.Decimal).Value = Convert.ToDecimal(latitude);
                    }
                    DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                    // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                    List<Masseur> list = new List<Masseur>();
                    Masseur mass = new Masseur();
                    foreach (DataRow dr in ds.Tables[0].Rows) {
                        list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                    }
                    JsonSerializer serializer2 = new Newtonsoft.Json.JsonSerializer();
                    MemoryStream ms2 = new MemoryStream();

                    using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                        new StreamWriter(ms2, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                        serializer2.Serialize(jsonTextWriter,
                            list
                        );
                        jsonTextWriter.Flush();
                    }
                    Utils.jsonSerializeStep2(ms2, Response);

                }
            }
        }
    }
}
