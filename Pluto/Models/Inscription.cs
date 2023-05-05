using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Inscription
    {
		public long id { get; set; }
		public string etat { get; set; }
		public string type { get; set; }
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
        public Teacher teacher { get; set; }
        public StudentList student { get; set; }
        public Personnal personnal { get; set; }
		public List<SupportPysique> supportPysiques { get; set; }
    }
}
