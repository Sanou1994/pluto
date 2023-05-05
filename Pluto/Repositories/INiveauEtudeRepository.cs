
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface INiveauEtudeRepository
    {
        Reponse ListeNiveauEtude(long? id, string tokenKey);
        Reponse ChercherNiveauEtude(long? id);
        Reponse AjouterNiveauEtude(NiveauEtude niveauEtude, string tokenKey);
        Reponse bloquerNiveauEtude(long? id, string tokenKey);
    }
}
