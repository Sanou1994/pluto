using Pluto.Models;
using System;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class UserRepositoryImpl : IUserRepository
    {
        private ICAllApi _callApi;
        public UserRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IUserRepository.AjouterPersonnal(Personnal personnal, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/user/personnal", personnal, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            } 
            return reponse;
        }
        Reponse IUserRepository.AjouterParent(Parent parent, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
                reponse = _callApi.CallBackendPost("/user/parent", parent, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            }
            return reponse;
        }

        Reponse IUserRepository.AjouterStudent(StudentList student, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
                reponse = _callApi.CallBackendPost("/user/student", student, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            }
            return reponse;
        }

        Reponse IUserRepository.AjouterTeacher(Teacher teacher, string tokenKey)
        {
            Reponse reponse = new Reponse();

            try
            {
                reponse = _callApi.CallBackendPost("/user/teacher", teacher, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            }
            return reponse;
        }


        Reponse IUserRepository.bloquerUser(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/user/users/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IUserRepository.ListeUser(string type, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/user/users/type/{type}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        

        Reponse IUserRepository.ChercherUser(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/user/users/type/{type}/id/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }
    }
}




