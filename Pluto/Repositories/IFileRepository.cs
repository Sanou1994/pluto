using System.IO;
using System.Threading.Tasks;
using System.Web;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface IFileRepository
    {
        
        Reponse AjouterFile(HttpPostedFileBase File, string fileName,string type, string tokenKey);
        Reponse rechercherFile(string fileName, string tokenKey);
    }
}
