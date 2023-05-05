using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Sceance
    {
        public long id { get; set; }
        public bool status;
        public User user { get; set; }
        public string type { get; set; }
        public float coefficient { get; set; }
        public int nombreHeure { get; set; }
        public double montantHoraire { get; set; }
        public Teacher teacher { get; set; }
        public Module module { get; set; }
        public Personnal personnal { get; set; }

    }
}
