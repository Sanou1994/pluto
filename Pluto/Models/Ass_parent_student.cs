using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto.Models
{
    public class Ass_parent_student
    {
        public long? id { get; set; }
        public long? studentID { get; set; }
        public bool? approuve { get; set; }
        public bool? rejette { get; set; }
        public string date_enregistrement { get; set; }
        public long? parentID { get; set; }
    }
}
