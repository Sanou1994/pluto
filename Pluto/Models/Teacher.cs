using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class Teacher : User
    {
        public long? departementID { get; set; }
        public string niveauEtude { get; set; }

        public StudentList student { get; set; }
        public ClasseList classe { get; set; }
        public List<ClasseList> classes { get; set; }
        public List<StudentList> students { get; set; }

        

    }
}
