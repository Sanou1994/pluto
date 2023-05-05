using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class NiveauEtudeRepositoryImpl : INiveauEtudeRepository
    {
        private ICAllApi _callApi;
        public NiveauEtudeRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse INiveauEtudeRepository.AjouterNiveauEtude(NiveauEtude niveauEtude, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/niveauetudes/add", niveauEtude, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce niveau  ";
            }
            return reponse;
        }

        Reponse INiveauEtudeRepository.bloquerNiveauEtude(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/niveauetudes/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse INiveauEtudeRepository.ListeNiveauEtude(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/niveauetudes/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse INiveauEtudeRepository.ChercherNiveauEtude(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




