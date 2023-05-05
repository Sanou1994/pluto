﻿using System;
using System.Linq;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class AuthentificationRepositoryImpl : IAuthentificationRepository
    {
        private ICAllApi _callApi;
        public AuthentificationRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;
           
        }

        Reponse IAuthentificationRepository.Seconnecter(string phone, string pwd)
        {
                Reponse reponse = new Reponse();
                Login login = new Login
                {
                    telephone = phone,
                    password = pwd

                };
           
               try
                {
                   reponse= _callApi.CallBackendPost("/user/login", login, null);


               }
                catch (Exception)
                {
                    reponse.code = 500;
                    reponse.message = "Impossible de vous connecter  ";
                } 


            
            return reponse;
        }

        Reponse IAuthentificationRepository.activationCode(string code)
        {
            Reponse reponse = new Reponse();

                try
                {
                  reponse = _callApi.CallBackendPost("/user/activation", Convert.ToInt32(code), null);


                }
                catch (Exception)
                {
                    reponse.code = 500;
                    reponse.message = "Code Incorrect";
                } 

            return reponse;



        }
    }
}


