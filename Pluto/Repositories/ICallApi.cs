

using System.IO;
using System.Threading.Tasks;
using System.Web;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
  public interface ICAllApi
    {
        Reponse CallBackendPost(string url, object obj, string tokenKey);
        Reponse CallBackendGet(string url, string tokenKey);
        Reponse CallBackendPostFile(string url, HttpPostedFileBase file,string type,string file_name, string tokenKey);
         Reponse CallBackendGetFile(string url, string tokenKey);

    }
}
