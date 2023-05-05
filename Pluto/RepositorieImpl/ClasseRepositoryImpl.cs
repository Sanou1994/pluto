using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class ClasseRepositoryImpl : IClasseRepository
    {
        private ICAllApi _callApi;
        public ClasseRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IClasseRepository.AjouterClasse(ClasseList classe, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
               reponse = _callApi.CallBackendPost("/classes/add", classe, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce département  ";
            }
            return reponse;
        }

        Reponse IClasseRepository.bloquerClasse(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/classes/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IClasseRepository.ListeClasse(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/classes/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        Reponse IClasseRepository.ChercherClasse(long? id)
        {
            Reponse reponse = new Reponse();

           
            return reponse;
        }
    }
}




