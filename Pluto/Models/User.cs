using System;
using System.Collections.Generic;

namespace Pluto.Models
{
   
   public  class User
    {
        public long? id { get; set; }
        public string prenom { get; set; }
        public string nationalite { get; set; }
        public string nom { get; set; }
        public string adresse { get; set; }
        public long? structureID { get; set; }
        public string numeroMatriciule { get; set; }
        public string typeDeRecrutement { get; set; }
        public long ? type { get; set; }
        public string typeUser { get; set; }
        public string lieu_naissance { get; set; }
        public string naissance { get; set; }
        public string sexe { get; set; }

        public long dateCreation { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public string telephone { get; set; }
        public string monToken { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string compteBancaire { get; set; }
        public string resetPasswordToken { get; set; }
        public long?  contratID { get; set; }
        public string role { get; set; }
        public string name_logo { get; set; }
        public string url_logo { get; set; }
        public List<Abscence> abscences { get; set; }
        public List<SupportPysique> supportPysiques { get; set; }

        public List<Paiement> paiements { get; set; }

        public List<Sceance> seances { get; set; }

        public bool? monPremiereConnexion { get; set; }
        public Contrat contrat { get; set; }
        public List<Inscription> inscriptions { get; set; }

    }
}
