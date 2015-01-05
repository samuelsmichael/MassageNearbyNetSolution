using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Common;

namespace MassageNearby {
    public partial class FileUpload : System.Web.UI.Page {
        private string ConnectionString {
            get {
                ConnectionStringSettings settings =
                    ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["MassageNearby"]];
                return settings.ConnectionString;
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            string vTransaction = "";
            string vUserId = "";

            try {
                HttpFileCollection MyFileCollection = Request.Files;
                           //Response.Write("Hi Mike!</br>");
               

                if (!string.IsNullOrEmpty(Request.Form["Transaction"])) {
                    vTransaction = Request.Form["Transaction"];
                           //Response.Write("Transaction=" + vTransaction + "</br>");
                    if (vTransaction.Equals("MasseurUploadMainPicture")) {
                        if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                            vUserId = Request.Form["UserId"];
                            //Response.Write("UserId=" + vUserId + "</br>");

                            string fileName = MyFileCollection[0].FileName + ".jpg";
                            //Response.Write("FileName="+fileName+"\n");
                            if (MyFileCollection.Count > 0) {
                                // Save the File
                                MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                            }

                            //Response.Write("bud: " + fileName+"\n");
                            SqlCommand cmd = new SqlCommand("uspMasseurSet");
                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                            cmd.Parameters.Add("@MainPictureURL", SqlDbType.VarChar).Value = fileName;
                            DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                            // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                            List<Masseur> list = new List<Masseur>();
                            Masseur mass = new Masseur();
                            foreach (DataRow dr in ds.Tables[0].Rows) {
                                list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                            }
                            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                            MemoryStream ms = new MemoryStream();

                            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                serializer.Serialize(jsonTextWriter,
                                    list
                                );
                                jsonTextWriter.Flush();
                            }
                            Utils.jsonSerializeStep2(ms, Response);
                        }
                    }
                    else {
                        if (vTransaction.Equals("MasseurUploadPrivatePicture1")) {
                            if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                                vUserId = Request.Form["UserId"];
                                //Response.Write("UserId=" + vUserId + "</br>");

                                string fileName = MyFileCollection[0].FileName + ".jpg";
                                //Response.Write("FileName="+fileName+"\n");
                                if (MyFileCollection.Count > 0) {
                                    // Save the File
                                    MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                                }

                                //Response.Write("bud: " + fileName+"\n");
                                SqlCommand cmd = new SqlCommand("uspMasseurSet");
                                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                                cmd.Parameters.Add("@PrivatePicture1URL", SqlDbType.VarChar).Value = fileName;
                                DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                                // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                                List<Masseur> list = new List<Masseur>();
                                Masseur mass = new Masseur();
                                foreach (DataRow dr in ds.Tables[0].Rows) {
                                    list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                                }
                                JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                                MemoryStream ms = new MemoryStream();

                                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                    new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                    serializer.Serialize(jsonTextWriter,
                                        list
                                    );
                                    jsonTextWriter.Flush();
                                }
                                Utils.jsonSerializeStep2(ms, Response);
                            }
                        }
                        else {
                            if (vTransaction.Equals("MasseurUploadPrivatePicture2")) {
                                if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                                    vUserId = Request.Form["UserId"];
                                    //Response.Write("UserId=" + vUserId + "</br>");

                                    string fileName = MyFileCollection[0].FileName + ".jpg";
                                    //Response.Write("FileName="+fileName+"\n");
                                    if (MyFileCollection.Count > 0) {
                                        // Save the File
                                        MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                                    }

                                    //Response.Write("bud: " + fileName+"\n");
                                    SqlCommand cmd = new SqlCommand("uspMasseurSet");
                                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                                    cmd.Parameters.Add("@PrivatePicture2URL", SqlDbType.VarChar).Value = fileName;
                                    DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                                    // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                                    List<Masseur> list = new List<Masseur>();
                                    Masseur mass = new Masseur();
                                    foreach (DataRow dr in ds.Tables[0].Rows) {
                                        list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                                    }
                                    JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                                    MemoryStream ms = new MemoryStream();

                                    using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                        new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                        serializer.Serialize(jsonTextWriter,
                                            list
                                        );
                                        jsonTextWriter.Flush();
                                    }
                                    Utils.jsonSerializeStep2(ms, Response);
                                }
                            } else {
                                if (vTransaction.Equals("MasseurUploadPrivatePicture3")) {
                                    if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                                        vUserId = Request.Form["UserId"];
                                        //Response.Write("UserId=" + vUserId + "</br>");

                                        string fileName = MyFileCollection[0].FileName + ".jpg";
                                        //Response.Write("FileName="+fileName+"\n");
                                        if (MyFileCollection.Count > 0) {
                                            // Save the File
                                            MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                                        }

                                        //Response.Write("bud: " + fileName+"\n");
                                        SqlCommand cmd = new SqlCommand("uspMasseurSet");
                                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                                        cmd.Parameters.Add("@PrivatePicture3URL", SqlDbType.VarChar).Value = fileName;
                                        DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                                        // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                                        List<Masseur> list = new List<Masseur>();
                                        Masseur mass = new Masseur();
                                        foreach (DataRow dr in ds.Tables[0].Rows) {
                                            list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                                        }
                                        JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                                        MemoryStream ms = new MemoryStream();

                                        using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                            new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                            serializer.Serialize(jsonTextWriter,
                                                list
                                            );
                                            jsonTextWriter.Flush();
                                        }
                                        Utils.jsonSerializeStep2(ms, Response);
                                    }
                                } else {
                                    if (vTransaction.Equals("MasseurUploadPrivatePicture4")) {
                                        if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                                            vUserId = Request.Form["UserId"];
                                            //Response.Write("UserId=" + vUserId + "</br>");

                                            string fileName = MyFileCollection[0].FileName + ".jpg";
                                            //Response.Write("FileName="+fileName+"\n");
                                            if (MyFileCollection.Count > 0) {
                                                // Save the File
                                                MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                                            }

                                            //Response.Write("bud: " + fileName+"\n");
                                            SqlCommand cmd = new SqlCommand("uspMasseurSet");
                                            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                                            cmd.Parameters.Add("@PrivatePicture4URL", SqlDbType.VarChar).Value = fileName;
                                            DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                                            // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                                            List<Masseur> list = new List<Masseur>();
                                            Masseur mass = new Masseur();
                                            foreach (DataRow dr in ds.Tables[0].Rows) {
                                                list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                                            }
                                            JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                                            MemoryStream ms = new MemoryStream();

                                            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                                new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                                serializer.Serialize(jsonTextWriter,
                                                    list
                                                );
                                                jsonTextWriter.Flush();
                                            }
                                            Utils.jsonSerializeStep2(ms, Response);
                                        }
                                    }
                                    else {
                                        if (vTransaction.Equals("MasseurUploadCertifiedPicture")) {
                                            if (!string.IsNullOrEmpty(Request.Form["UserId"])) {
                                                vUserId = Request.Form["UserId"];
                                                //Response.Write("UserId=" + vUserId + "</br>");

                                                string fileName = MyFileCollection[0].FileName + ".jpg";
                                                //Response.Write("FileName="+fileName+"\n");
                                                if (MyFileCollection.Count > 0) {
                                                    // Save the File
                                                    MyFileCollection[0].SaveAs(Server.MapPath("/files/" + fileName));
                                                }

                                                //Response.Write("bud: " + fileName+"\n");
                                                SqlCommand cmd = new SqlCommand("uspMasseurSet");
                                                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Convert.ToInt32(vUserId);
                                                cmd.Parameters.Add("@CertifiedPictureURL", SqlDbType.VarChar).Value = fileName;
                                                DataSet ds = Common.Utils.getDataSet(cmd, ConnectionString);
                                                // Response.Write("fred."+ds.Tables.Count+"  -  "+ds.Tables[0].Rows.Count+"\n");

                                                List<Masseur> list = new List<Masseur>();
                                                Masseur mass = new Masseur();
                                                foreach (DataRow dr in ds.Tables[0].Rows) {
                                                    list.Add((Masseur)mass.objectFromDatasetRowPublic(dr));
                                                }
                                                JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                                                MemoryStream ms = new MemoryStream();

                                                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(
                                                    new StreamWriter(ms, new UTF8Encoding(false, true))) { CloseOutput = false }) {
                                                    serializer.Serialize(jsonTextWriter,
                                                        list
                                                    );
                                                    jsonTextWriter.Flush();
                                                }
                                                Utils.jsonSerializeStep2(ms, Response);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /*
                //          for(int c=0;c<Request.Form.Count;c++) {
                //              Response.Write("Name: " + Request.Form.GetKey(c) + "</br>");
                //             Response.Write("Value: " + Request.Form.Get(c) + "</br>");
                //         }



                int loop1;
                string FilePath = null;

                String[] arr1 = MyFileCollection.AllKeys;  // This will get names of all files into a string array. 
                for (loop1 = 0; loop1 < arr1.Length; loop1++) {
                    //              Response.Write("File: " + Server.HtmlEncode(arr1[loop1]) + "<br />");
                    //             Response.Write("  size = " + MyFileCollection[loop1].ContentLength + "<br />");
                    //            Response.Write("  content type = " + MyFileCollection[loop1].ContentType + "<br />");
                    //           Response.Write("  FileName = " + MyFileCollection[loop1].FileName + "<br />");
                    FilePath = Server.MapPath("/files/" + MyFileCollection[loop1].FileName + ".jpg");
                }

                //     Response.Write("Hi Mike! Count="+MyFileCollection.Count+"</br>");
                */
              
            } catch (Exception ee) {
               // Response.Write("ERROR! - "+ee.Message + " Stack:" +ee.StackTrace);
            }
        } 
    }
}
