

using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IAuthentificationRepository
    {
        Reponse Seconnecter(string phone, string pwd);
        Reponse activationCode( string code);
        
    }
}
