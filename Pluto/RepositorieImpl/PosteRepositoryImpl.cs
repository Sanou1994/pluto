using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class PosteRepositoryImpl : IPosteRepository
    {
        private ICAllApi _callApi;
        public PosteRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IPosteRepository.AjouterPoste(Poste poste, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/postes/add", poste, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer cette filières  ";
            }
            return reponse;
        }

        Reponse IPosteRepository.bloquerPoste(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/postes/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IPosteRepository.ListePoste(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/postes/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IPosteRepository.ChercherPoste(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




