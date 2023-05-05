using Pluto.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Pluto
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers    
            container.RegisterType<INiveauEtudeRepository, NiveauEtudeRepositoryImpl>();
            container.RegisterType<IAuthentificationRepository, AuthentificationRepositoryImpl>();
            container.RegisterType<ICAllApi, CallApiRepositoryImpl>();
            container.RegisterType<IUserRepository, UserRepositoryImpl>();
            container.RegisterType<IAnneeScolaireRepository, AnneeScolaireRepositoryImpl>();
            container.RegisterType<IDepartementRepository, DepartementRepositoryImpl>();
            container.RegisterType<IFiliereRepository, FiliereRepositoryImpl>();
            container.RegisterType<IPosteRepository, PosteRepositoryImpl>();
            container.RegisterType<IContratRepository, ContratRepositoryImpl>();
            container.RegisterType<IClasseRepository, ClasseRepositoryImpl>();
            container.RegisterType<IFileRepository, FileRepositoryImpl>();
			container.RegisterType<IInscriptionRepository, InscriptionRepositoryImpl>();
			container.RegisterType<ILancerPreinscriptionRepository, LancerPreinscriptionRepositoryImpl>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}