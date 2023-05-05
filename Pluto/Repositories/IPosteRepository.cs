
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IPosteRepository
    {
        Reponse ListePoste(long? id, string tokenKey);
        Reponse ChercherPoste(long? id);
        Reponse AjouterPoste(Poste poste, string tokenKey);
        Reponse bloquerPoste(long? id, string tokenKey);
    }
}
