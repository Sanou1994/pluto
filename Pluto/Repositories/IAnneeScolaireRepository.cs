
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IAnneeScolaireRepository
    {
        Reponse ListeAnneeScolaire(long? id, string tokenKey);
        Reponse ChercherAnneeScolaire(long? id);
        Reponse AjouterAnneeScolaire(AnneeScolaire anneeScolaire, string tokenKey);
        Reponse bloquerAnneeScolaire(long? id, string tokenKey);
    }
}
