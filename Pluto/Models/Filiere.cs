using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class FiliereList
    {
		public long id { get; set; }
		public string titre { get; set; }
		public bool status { get; set; }
		public long? departementID { get; set; }
        public long? structureID { get; set; }


    }
}