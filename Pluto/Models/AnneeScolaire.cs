using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class AnneeScolaire
    {
        public long? id { get; set; }
        public string libelle { get; set; }
        public bool status { get; set; }
        public long? structureID { get; set; }
    }
}
