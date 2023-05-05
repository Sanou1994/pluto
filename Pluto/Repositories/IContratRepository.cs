
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IContratRepository
    {
        Reponse ListeContrat(long? id, string tokenKey);
        Reponse ChercherContrat(long? id);
        Reponse AjouterContrat(Contrat contrat, string tokenKey);
        Reponse bloquerContrat(long? id, string tokenKey);
    }
}
