using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Note
    {
        public long id { get; set; }
        public StudentList student { get; set; }
        public Personnal personnal { get; set; }
    }
}
