using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pluto.Models
{
    public class StudentList : User
    {
        public long? parentID { get; set; }
		public long? filiereID { get; set; }
        public long? departementID { get; set; }
        public long? niveauEtudeID { get; set; }     
     
    }
	public class Student : User
	{
		public long? parentID { get; set; }
		public long? filiereID { get; set; }
		public long? departementID { get; set; }
		public long? niveauEtudeID { get; set; }

	}
}
