using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using static Unity.Storage.RegistrationSet;

namespace Pluto.Models
{
    public class Tempon
    {

        public class Reponse
        {
            public string message { get; set; }
            public int code { get; set; }
            public dynamic result { get; set; }
        }
        public class Login
        {
            public string telephone { get; set; }
            public string password { get; set; }
            
        }

        public class PreinscriptionData
        {
            public string nom { get; set; }
            public string prenom { get; set; }
            public string email { get; set; }
            public string sexe { get; set; }
            public string date_de_naissance { get; set; }
            public string lieu_de_naissance { get; set; }
            public string pays_de_naissance { get; set; }
            public string nationalite { get; set; }
            public string telephone { get; set; }
			public long id { get; set; }
			public string etat { get; set; }
			public string type { get; set; }
			public string pays_de_residence { get; set; }
            public string indicatif { get; set; }
            public string password { get; set; }
            public string dernier_etablissement_frequente { get; set; }
            public string adresse_complete { get; set; }
            public string dernier_diplome_obtenue { get; set; }
            public string type_de_cours { get; set; }
            public string programme_choisi { get; set; }
            public string niveau { get; set; }
            public string specialite { get; set; }
            public string frais_dossier { get; set; }
            public string responsable_financier { get; set; }
            public string nom_responsable { get; set; }
            public string profession_du_responsable{ get; set; }
			public HttpPostedFileBase photo_identite { get; set; }
		    public HttpPostedFileBase photocopie_piece_identite { get; set; }
	        public HttpPostedFileBase photocopie_releve_notes { get; set; }
            public HttpPostedFileBase photocopie_attestation { get; set; }

            public string numéro_de_téléphone_du_responsable{ get; set; }
            public string adresse_email_responsable { get; set; }
            public string adresse_responsable { get; set; }
            public string autre_contact { get; set; }
            private long? lancerPreinscriptionID { get; set; }
			public string message { get; set; }




		}
		public class InscriptionList
        {
            public string pays_de_naissance { get; set; }
            public string pays_de_residence { get; set; }
            public string indicatif { get; set; }
            public string dernier_etablissement_frequente { get; set; }
            public string adresse_complete { get; set; }
            public string dernier_diplome_obtenue { get; set; }
            public string type_de_cours { get; set; }
            public string programme_choisi { get; set; }
            public string niveau { get; set; }
            public string specialite { get; set; }
            public string frais_dossier { get; set; }
            public string responsable_financier { get; set; }
            public string nom_responsable { get; set; }
            public string profession_du_responsable { get; set; }

            public string numéro_de_téléphone_du_responsable { get; set; }
            public string adresse_email_responsable { get; set; }
            public string adresse_responsable { get; set; }
            public string autre_contact { get; set; }
            public long? lancerPreinscriptionID { get; set; }
            public long? studentID { get; set; }



        }
        public class Mettre_a_jour_profilData
		{
            public StudentList student { get; set; }
            public List<FiliereList> lstFilieres { get; set; }
			public List<DepartementList> lstDepartements { get; set; }
			public List<NiveauEtude> lstNiveauEtudes { get; set; }
            public Parent parent { get; set; }
		}
        public class ConfigurationData
        {
            public List<ClasseList> lstClasses { get; set; }
            public List<FiliereList> lstFilieres { get; set; }
            public List<DepartementList> lstDepartements { get; set; }
            public List<LancerPreinscription> lstLancerPreinscriptions { get; set; }

            public List<AnneeScolaire> lstAnneeScolaires { get; set; }
           
        }

    }
    public static class Utils
    {
        public static T ToObject<T>(this Object fromObject)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(fromObject));
        }


        public static List<T> ToObjectList<T>(this Object fromObject)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(fromObject));
        }
    }
	


    }