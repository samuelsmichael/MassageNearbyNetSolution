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
        [DataMemberAttribute]
        public DateTime Birthdate { get; set; }
        [DataMemberAttribute]
        public string Height { get; set; }
        [DataMemberAttribute]
        public string Ethnicity { get; set; }
        [DataMemberAttribute]
        public string Services { get; set; }
        [DataMemberAttribute]
        public string Bio { get; set; }
        [DataMemberAttribute]
        public DateTime SubscriptionEndDate { get; set; }
        [DataMemberAttribute]
        public string PrivatePicture1URL { get; set; }
        [DataMemberAttribute]
        public string PrivatePicture2URL { get; set; }
        [DataMemberAttribute]
        public string PrivatePicture3URL { get; set; }
        [DataMemberAttribute]
        public string PrivatePicture4URL { get; set; }
        [DataMemberAttribute]
        public bool IsCertified { get; set; }
        [DataMemberAttribute]
        public int CertificationNumber { get; set; }
        [DataMemberAttribute]
        public string Password { get; set; }


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
            masseur.Birthdate = Utils.ObjectToDateTime(dr["Birthdate"]);
            masseur.Height = Utils.ObjectToString(dr["Height"]);
            masseur.Ethnicity = Utils.ObjectToString(dr["Ethnicity"]);
            masseur.Services = Utils.ObjectToString(dr["Services"]);
            masseur.Bio = Utils.ObjectToString(dr["Bio"]);
            masseur.SubscriptionEndDate = Utils.ObjectToDateTime(dr["SubscriptionEndDate"]);
            masseur.PrivatePicture1URL = Utils.ObjectToString(dr["PrivatePicture1URL"]);
            masseur.PrivatePicture2URL = Utils.ObjectToString(dr["PrivatePicture2URL"]);
            masseur.PrivatePicture3URL = Utils.ObjectToString(dr["PrivatePicture3URL"]);
            masseur.PrivatePicture4URL = Utils.ObjectToString(dr["PrivatePicture4URL"]);
            masseur.IsCertified = Utils.ObjectToBool(dr["IsCertified"]);
            masseur.CertificationNumber = Utils.ObjectToInt(dr["CertificationNumber"]);
            masseur.Password = Utils.ObjectToString(dr["Password"]);

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