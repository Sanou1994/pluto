
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IInscriptionRepository
    {
        Reponse ListeInscription(long? id, string tokenKey);
        Reponse ChercherInscription(long? id);
        Reponse AjouterInscription(InscriptionList departement, string tokenKey);
        Reponse bloquerInscription(long? id, string tokenKey);
    }
}
