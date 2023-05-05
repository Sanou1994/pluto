using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class InscriptionRepositoryImpl : IInscriptionRepository
    {
        private ICAllApi _callApi;
        public InscriptionRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IInscriptionRepository.AjouterInscription(InscriptionList Inscription, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/inscriptions/add", Inscription, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce inscription  ";
            }
            return reponse;
        }

        Reponse IInscriptionRepository.bloquerInscription(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/inscriptions/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IInscriptionRepository.ListeInscription(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/inscriptions/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IInscriptionRepository.ChercherInscription(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




