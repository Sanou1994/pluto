using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class DepartementRepositoryImpl : IDepartementRepository
    {
        private ICAllApi _callApi;
        public DepartementRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IDepartementRepository.AjouterDepartement(DepartementList departement, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/departements/add", departement, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce département  ";
            }
            return reponse;
        }

        Reponse IDepartementRepository.bloquerDepartement(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/departements/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IDepartementRepository.ListeDepartement(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/departements/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IDepartementRepository.ChercherDepartement(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




