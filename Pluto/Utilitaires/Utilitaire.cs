using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto.Utilitaires
{ 
    static class Utilitaire
    {
        public static string genererMatricule(long idUser, long idStructure)
        {

            var annee = DateTime.Now.Year.ToString();
            string idUserS = Convert.ToString(idUser);
            string idStructureS = Convert.ToString(idStructure);
            string matricule = "E"+ annee + idUserS+ idStructureS;
            return matricule;
        }

    }
}
