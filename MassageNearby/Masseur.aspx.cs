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
            String ipAddress = Request.QueryString["IPAddress"];
            bool isDoingLogin = Utils.ObjectToBool(Request.QueryString["IsDoingLogin"]);
            MemoryStream ms = new MemoryStream();
            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            if (Common.Utils.isNothing(action)) { // for legacy
                SqlCommand command = new SqlCommand("uspMasseurGet");
                SqlParameter parm = new SqlParameter("Name", SqlDbType.VarChar);
                parm.Value = name;
                command.Parameters.Add(parm);
                if (isDoingLogin) {
                    command.Parameters.Add("@IsDoingLogin", SqlDbType.Bit).Value = isDoingLogin;
                }
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
                List<Masseur> jdListMasseur = new Masseur().buildList(name, command);
                if (isDoingLogin && jdListMasseur.Count == 0) { // try logging in as a client
                    Server.Transfer("Client.aspx?Name="+Server.UrlEncode(name)+"&URL="+Server.UrlEncode(ipAddress),true);
                } else {
                    using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                        new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                        serializer.Serialize(jsonTextWriter, jdListMasseur);
                        jsonTextWriter.Flush();
                    }
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
                    string email=Request.QueryString["Email"];
                    if (Common.Utils.isNothingNot(email)) {
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    }
                    string port = Request.QueryString["Port"];
                    if (Common.Utils.isNothingNot(port)) {
                        cmd.Parameters.Add("@Port", SqlDbType.Int).Value = Convert.ToInt32(port);
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
                    string mpurl = Request.QueryString["MainPictureURL"];
                    if (Common.Utils.isNothingNot(mpurl)) {
                        cmd.Parameters.Add("@MainPictureURL", SqlDbType.VarChar).Value = mpurl;
                    }

                    string mpurl1 = Request.QueryString["PrivatePicture1URL"];
                    if (Common.Utils.isNothingNot(mpurl1)) {
                        cmd.Parameters.Add("@PrivatePicture1URL", SqlDbType.VarChar).Value = mpurl1;
                    }
                    string mpurl2 = Request.QueryString["PrivatePicture2URL"];
                    if (Common.Utils.isNothingNot(mpurl2)) {
                        cmd.Parameters.Add("@PrivatePicture2URL", SqlDbType.VarChar).Value = mpurl2;
                    }
                    string mpurl3 = Request.QueryString["PrivatePicture3URL"];
                    if (Common.Utils.isNothingNot(mpurl3)) {
                        cmd.Parameters.Add("@PrivatePicture3URL", SqlDbType.VarChar).Value = mpurl3;
                    }
                    string mpurl4 = Request.QueryString["PrivatePicture4URL"];
                    if (Common.Utils.isNothingNot(mpurl4)) {
                        cmd.Parameters.Add("@PrivatePicture4URL", SqlDbType.VarChar).Value = mpurl4;
                    }
                    string cpurl = Request.QueryString["CertifiedPictureURL"];
                    if (Common.Utils.isNothingNot(cpurl)) {
                        cmd.Parameters.Add("@CertifiedPictureURL", SqlDbType.VarChar).Value = cpurl;
                    }
                    string longitude = Request.QueryString["Longitude"];
                    if (Common.Utils.isNothingNot(longitude)) {
                        cmd.Parameters.Add("@Longitude", SqlDbType.Decimal).Value = Convert.ToDecimal(longitude);
                    }
                    string latitude = Request.QueryString["Latitude"];
                    if (Common.Utils.isNothingNot(latitude)) {
                        cmd.Parameters.Add("@Latitude", SqlDbType.Decimal).Value = Convert.ToDecimal(latitude);
                    }
                    string birthDate = Request.QueryString["Birthdate"];
                    if (birthDate != null) {
                        cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = birthDate;
                    }
                    string subenddate = Request.QueryString["SubscriptionEndDate"];
                    if (subenddate != null) {
                        cmd.Parameters.Add("@SubscriptionEndDate", SqlDbType.DateTime).Value = subenddate;
                    }
                    string height = Request.QueryString["Height"];
                    if (Common.Utils.isNothingNot(height)) {
                        cmd.Parameters.Add("@Height", SqlDbType.VarChar).Value = height;
                    }
                    string ethnicity = Request.QueryString["Ethnicity"];
                    if (Common.Utils.isNothingNot(ethnicity)) {
                        cmd.Parameters.Add("@Ethnicity", SqlDbType.VarChar).Value = ethnicity;
                    }
                    string bio = Request.QueryString["Bio"];
                    if (Common.Utils.isNothingNot(bio)) {
                        cmd.Parameters.Add("@Bio", SqlDbType.VarChar).Value = bio;
                    }
                    string services = Request.QueryString["Services"];
                    if (Common.Utils.isNothingNot(ethnicity)) {
                        cmd.Parameters.Add("@Services", SqlDbType.VarChar).Value = services;
                    }
                    String certificationNumberStr = Request.QueryString["CertificationNumber"];
                    if (Common.Utils.isNothingNot(certificationNumberStr)) {
                        int certificationNumber = Convert.ToInt32(certificationNumberStr);
                        cmd.Parameters.Add("CertificationNumber", SqlDbType.Int).Value = certificationNumber;
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
