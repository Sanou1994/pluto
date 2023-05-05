using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class LancerPreinscriptionRepositoryImpl : ILancerPreinscriptionRepository
    {
        private ICAllApi _callApi;
        public LancerPreinscriptionRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse ILancerPreinscriptionRepository.AjouterLancerPreinscription(LancerPreinscription lancerPreinscription, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/lancerpreinscriptions/add", lancerPreinscription, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de lancer cette pré-inscription  ";
            }
            return reponse;
        }

        Reponse ILancerPreinscriptionRepository.bloquerLancerPreinscription(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/lancerpreinscriptions/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse ILancerPreinscriptionRepository.ListeLancerPreinscription(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/lancerpreinscriptions/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse ILancerPreinscriptionRepository.ChercherLancerPreinscription(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




