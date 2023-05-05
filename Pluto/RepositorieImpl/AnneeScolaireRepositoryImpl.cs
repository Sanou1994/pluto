using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class AnneeScolaireRepositoryImpl : IAnneeScolaireRepository
    {
        private ICAllApi _callApi;
        public AnneeScolaireRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IAnneeScolaireRepository.AjouterAnneeScolaire(AnneeScolaire anneeScolaire, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/anneescolaires/add", anneeScolaire, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce département  ";
            }
            return reponse;
        }

        Reponse IAnneeScolaireRepository.bloquerAnneeScolaire(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/anneescolaires/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IAnneeScolaireRepository.ListeAnneeScolaire(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/anneescolaires/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IAnneeScolaireRepository.ChercherAnneeScolaire(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




