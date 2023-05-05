using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class ContratRepositoryImpl : IContratRepository
    {
        private ICAllApi _callApi;
        public ContratRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IContratRepository.AjouterContrat(Contrat contrat, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/contrats/add", contrat, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer cette filières  ";
            }
            return reponse;
        }

        Reponse IContratRepository.bloquerContrat(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/contrats/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IContratRepository.ListeContrat(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/contrats/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IContratRepository.ChercherContrat(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




