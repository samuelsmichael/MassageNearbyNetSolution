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
    public class Masseur : WebServiceItem {
        [DataMemberAttribute]
        public int UserId { get; set; }
        [DataMemberAttribute]
        public int MasseurId { get; set; }
        [DataMemberAttribute]
        public string Name { get; set; }
        [DataMemberAttribute]
        public string URL { get; set; }
        [DataMemberAttribute]
        public bool IsOnline { get; set; }
        [DataMemberAttribute]
        public string MainPictureURL { get; set; }
        [DataMemberAttribute]
        public string CertifiedPictureURL { get; set; }
        [DataMemberAttribute]
        public decimal Longitude { get; set; }
        [DataMemberAttribute]
        public decimal Latitude { get; set; }

        public WebServiceItem objectFromDatasetRowPublic(DataRow dr) {
            return objectFromDatasetRow(dr);
        }

        protected override WebServiceItem objectFromDatasetRow(System.Data.DataRow dr) {
            Masseur masseur = new Masseur();
            masseur.UserId = Utils.ObjectToInt(dr["UserId"]);
            masseur.MasseurId = Utils.ObjectToInt(dr["MasseurId"]);
            masseur.Name = Utils.ObjectToString(dr["Name"]);
            masseur.URL = Utils.ObjectToString(dr["URL"]);
            masseur.IsOnline = Utils.ObjectToBool(dr["IsOnline"]);
            masseur.MainPictureURL=Utils.ObjectToString(dr["MainPictureURL"]);
            masseur.CertifiedPictureURL = Utils.ObjectToString(dr["CertifiedPictureURL"]);
            masseur.Longitude = Utils.ObjectToDecimal(dr["Longitude"]);
            masseur.Latitude=Utils.ObjectToDecimal(dr["Latitude"]);
            return masseur;
        }

        public List<Masseur> buildList(String name, SqlCommand command) {
            List<Masseur> list = new List<Masseur>();
            foreach (DataRow dr in getDataSet(command).Tables[0].Rows) {
                list.Add((Masseur)objectFromDatasetRow(dr));
            }
            return list;
        }
    }
}