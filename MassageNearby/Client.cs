using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Runtime.Serialization;
using Common;


namespace MassageNearby {
    public class Client : WebServiceItem {
        [DataMemberAttribute]
        public int UserId { get; set; }
        [DataMemberAttribute]
        public int ClientId { get; set; }
        [DataMemberAttribute]
        public string Name { get; set; }
        [DataMemberAttribute]
        public string URL { get; set; }
        public string Password { get; set; }
        [DataMemberAttribute]
        public string Email { get; set; }
        [DataMemberAttribute]
        public int Port { get; set; }

        public WebServiceItem objectFromDatasetRowPublic(DataRow dr) {
            return objectFromDatasetRow(dr);
        }
        protected override WebServiceItem objectFromDatasetRow(System.Data.DataRow dr) {
            Client client = new Client();
            client.UserId = Utils.ObjectToInt(dr["UserId"]);
            client.ClientId = Utils.ObjectToInt(dr["ClientId"]);
            client.Name = Utils.ObjectToString(dr["Name"]);
            client.URL = Utils.ObjectToString(dr["URL"]);
            client.Password = Utils.ObjectToString(dr["Password"]);
            client.Email = Utils.ObjectToString(dr["Email"]);
            client.Port = Utils.ObjectToInt(dr["Port"]);
            return client;
        }

        public List<Client> buildList(String name, SqlCommand command) {
            List<Client> list = new List<Client>();
            foreach (DataRow dr in getDataSet(command).Tables[0].Rows) {
                list.Add((Client)objectFromDatasetRow(dr));
            }
            return list;
        }
    }
}