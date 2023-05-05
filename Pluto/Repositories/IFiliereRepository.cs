
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IFiliereRepository
    {
        Reponse ListeFiliere(long? id, string tokenKey);
        Reponse ChercherFiliere(long? id);
        Reponse AjouterFiliere(FiliereList filiere, string tokenKey);
        Reponse bloquerFiliere(long? id, string tokenKey);
    }
}
