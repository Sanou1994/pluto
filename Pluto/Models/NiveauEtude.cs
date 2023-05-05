using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class NiveauEtude
    {
        public long? id { get; set; }
        public string nom { get; set; }
        public bool status { get; set; }
        public long? structureID { get; set; }
        public long? filiereID { get; set; }
        public long? niveauEtudeID { get; set; }
    }
}
