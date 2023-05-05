using Pluto.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class FileRepositoryImpl : IFileRepository
    {
        private ICAllApi _callApi;
        public FileRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        public Reponse AjouterFile(HttpPostedFileBase file,string type,string file_name, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
                 
                reponse = _callApi.CallBackendPostFile("/file/add", file,type, file_name, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message =(type != "PHOTO") ? "Impossible d'enregistrer cette photo " : "Impossible d'enregistrer ce fichier"; 
            }
            return reponse;
        }

        public  Reponse rechercherFile(string fileName, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {

                reponse =  _callApi.CallBackendGetFile($"/photo/{fileName}" , tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible d'enregistrer cette photo  ";
            }
            return reponse;
        }
    }
}




