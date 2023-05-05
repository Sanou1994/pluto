
using Pluto.Models;

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IDepartementRepository
    {
        Reponse ListeDepartement(long? id, string tokenKey);
        Reponse ChercherDepartement(long? id);
        Reponse AjouterDepartement(DepartementList Departement, string tokenKey);
        Reponse bloquerDepartement(long? id, string tokenKey);
    }
}
