using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Contrat
    {
        

        public long? id { get; set; }
        public String type { get; set; }
        public String categorie { get; set; }
        public double? montant { get; set; }
        public bool status { get; set; }
        public long? structureID { get; set; }
        public long? duree { get; set; }
    }
}