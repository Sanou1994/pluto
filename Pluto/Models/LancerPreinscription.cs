using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class LancerPreinscription  
    {
        public long? id { get; set; }
        public long? departementID { get; set; }
        public long? anneeScolaireID { get; set; }
        public long? filiereID { get; set; }
        public long? classeID { get; set; }
        public long? datePrologement { get; set; }
        public long? dateDebut { get; set; }
        public long? dateFin { get; set; }
        public bool? status { get; set; }
        public long? structureID { get; set; }
    }
}
