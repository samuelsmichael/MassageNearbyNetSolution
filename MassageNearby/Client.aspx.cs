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

namespace MassageNearby {
    public partial class Client1 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            String name = Request.QueryString["Name"];
            String url = Request.QueryString["URL"];
            String clientId = Request.QueryString["ClientId"];
            MemoryStream ms = new MemoryStream();
            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
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
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                serializer.Serialize(jsonTextWriter, new Client().buildList(name,command));
                jsonTextWriter.Flush();
            }
            Utils.jsonSerializeStep2(ms, Response);
        }
    }
}
