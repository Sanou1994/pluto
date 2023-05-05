using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto.Models
{
    public class ProfilData
    {
        public StudentList student { get; set; }
        public   List<FiliereList> lstFilieres { get; set; }
        public   List<DepartementList> lstDepartements { get; set; }
        public   List<NiveauEtude> lstNiveauEtudes { get; set; }
		public Parent parent { get; set; }

	}
}
