using System;
using System.IO;
using Newtonsoft.Json;
using Common;
using System.Text;
using System.Collections;
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
using System.Collections.Generic;

namespace MassageNearby {
    public partial class Client1 : System.Web.UI.Page {
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
            String clientId = Request.QueryString["ClientId"];
            String action = Request.QueryString["Action"];
            String port = Request.QueryString["Port"];
            MemoryStream ms = new MemoryStream();
            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            if (Common.Utils.isNothing(action)) { // for legacy

                SqlCommand command = new SqlCommand("uspClientGet");
                SqlParameter parm = new SqlParameter("Name", SqlDbType.VarChar);
                parm.Value = name;
                command.Parameters.Add(parm);
                if (url != null) {
                    SqlParameter parm2 = new SqlParameter("URL", SqlDbType.VarChar);
                    parm2.Value = url;
                    command.Parameters.Add(parm2);
                }
                if (clientId != null) {
                    SqlParameter parm3 = new SqlParameter("ClientId", SqlDbType.Int);
                    parm3.Value = Convert.ToInt32(clientId);
                    command.Parameters.Add(parm3);
                }
                if (Common.Utils.isNothingNot(port)) {
                    command.Parameters.Add("@Port", SqlDbType.Int).Value = Convert.ToInt32(port);
                }

                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                    new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                    serializer.Serialize(jsonTextWriter, new Client().buildList(name, command));
                    jsonTextWriter.Flush();
                }
                Utils.jsonSerializeStep2(ms, Response);
            }
            else {
                if (action.ToLower().Equals("set")) {
                    SqlCommand cmd = new SqlCommand("uspClientSet");

                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["UserId"]);
                    if (Common.Utils.isNothingNot(name)) {
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    }
                    if (Common.Utils.isNothingNot(port)) {
                        cmd.Parameters.Add("@Port", SqlDbType.Int).Value = Convert.ToInt32(port);
                    }
                    string email = Request.QueryString["Email"];
                    if (Common.Utils.isNothingNot(email)) {
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    }
                    string password = Request.QueryString["Password"];
                    if (Common.Utils.isNothingNot(password)) {
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    }
                    if (url != null) {
                        SqlParameter parm2 = new SqlParameter("URL", SqlDbType.VarChar);
                        parm2.Value = url;
                        cmd.Parameters.Add(parm2);
                    }

                    DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                    // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                    List<Client> list = new List<Client>();
                    Client mass = new Client();
                    foreach (DataRow dr in ds.Tables[0].Rows) {
                        list.Add((Client)mass.objectFromDatasetRowPublic(dr));
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
