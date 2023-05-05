using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Module
    {
        public long id { get; set; }
        public List<Sceance> seances { get; set; }
        public ClasseList classe { get; set; }
        
    }
}
