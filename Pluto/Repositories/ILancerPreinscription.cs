
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface ILancerPreinscriptionRepository
    {
        Reponse ListeLancerPreinscription(long? id, string tokenKey);
        Reponse ChercherLancerPreinscription(long? id);
        Reponse AjouterLancerPreinscription(LancerPreinscription lancerPreinscription, string tokenKey);
        Reponse bloquerLancerPreinscription(long? id, string tokenKey);
    }
}
