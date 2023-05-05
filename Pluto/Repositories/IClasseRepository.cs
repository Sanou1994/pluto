
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IClasseRepository
    {
        Reponse ListeClasse(long? id, string tokenKey);
        Reponse ChercherClasse(long? id);
        Reponse AjouterClasse(ClasseList classe, string tokenKey);
        Reponse bloquerClasse(long? id, string tokenKey);
    }
}
