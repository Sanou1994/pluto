using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class FiliereRepositoryImpl : IFiliereRepository
    {
        private ICAllApi _callApi;
        public FiliereRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IFiliereRepository.AjouterFiliere(FiliereList filiere, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/filieres/add", filiere, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer cette filières  ";
            }
            return reponse;
        }

        Reponse IFiliereRepository.bloquerFiliere(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/filieres/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IFiliereRepository.ListeFiliere(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/filieres/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IFiliereRepository.ChercherFiliere(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




