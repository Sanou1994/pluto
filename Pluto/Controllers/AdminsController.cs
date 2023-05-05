using Pluto.Models;
using Pluto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web.Mvc;
using static Pluto.Models.Tempon;
using System.Web;
using System.IO;
using System.Web.UI;

namespace Pluto.Controllers
{
    public class AdminsController : Controller
    {
        private INiveauEtudeRepository _niveauEtudeRepository;
        private IFiliereRepository _filiereRepository;
        private IDepartementRepository _departementRepository;
        private IUserRepository _userRepository;
        private IPosteRepository _posteRepository;
        private IContratRepository _contratRepository;
        private IFileRepository _fileRepository;
        private IAnneeScolaireRepository _anneeScolaireRepository;
        private IClasseRepository _classeRepository;
        private ILancerPreinscriptionRepository _lancerPreinscriptionRepository;

        public AdminsController(IUserRepository userRepository,
                               INiveauEtudeRepository niveauEtudeRepository,
                               IContratRepository contratRepository,
                               IPosteRepository posteRepository,
                               IFiliereRepository filiereRepository,
                               IFileRepository fileRepository,
                               IClasseRepository classeRepository,
                               IAnneeScolaireRepository anneeScolaireRepository,
                               ILancerPreinscriptionRepository lancerPreinscriptionRepository,
                               IDepartementRepository departementRepository)
        {
            _lancerPreinscriptionRepository = lancerPreinscriptionRepository;
            _classeRepository = classeRepository;
            _anneeScolaireRepository = anneeScolaireRepository;
            _fileRepository = fileRepository;
            _niveauEtudeRepository = niveauEtudeRepository;
            _userRepository = userRepository;
            _departementRepository = departementRepository;
            _filiereRepository = filiereRepository;
            _contratRepository = contratRepository;
            _posteRepository = posteRepository;
        }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Details_inscription()
        {

            return View();
        }
        public ActionResult Details_preinscription()
        {

            return View();
        }
        public ActionResult Preinscription()
        {

            return View();
        }

        public string Photo(string name)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _fileRepository.rechercherFile(name, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return reponse.result as String;
                    }
                    else
                    {
                        return null;

                    }

                }
                else
                {
                    return null;
                }


            }
            catch
            {

                return null;

            }
        }
        public ActionResult Candidature(int? page, string searching)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _userRepository.ListeUser("STUDENT", Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<StudentList> students = Utils.ToObjectList<StudentList>(reponse.result);
                            var lstStudents = (searching != null) ? students.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.prenom != null && a.structureID == structureID && a.prenom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.adresse != null && a.structureID == structureID && a.adresse.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.telephone != null && a.structureID == structureID && a.telephone.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : students.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstStudents);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<StudentList>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<StudentList>().ToPagedList(1, 1);
                return View(pers);
            }

        }
        public ActionResult Details_candidature()
        {
            return View();
        }
        public ActionResult Profil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult bloquerUser(long ? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);
                   
                    Reponse reponse = _userRepository.bloquerUser(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        public ActionResult Administration(int? page, string searching)
        {
             try
            {
                

                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _userRepository.ListeUser("PERSONNAL",Convert.ToString(Session["token"]));
                    Reponse reponsePoste = _posteRepository.ListePoste(structureID, Convert.ToString(Session["token"]));
                    Reponse reponseContrat = _contratRepository.ListeContrat(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<Personnal> personnals = Utils.ToObjectList<Personnal>(reponse.result);
                            var lstPersonnals = (searching != null) ? personnals.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.prenom != null && a.structureID == structureID && a.prenom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.adresse != null && a.structureID == structureID && a.adresse.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.telephone != null && a.structureID == structureID && a.telephone.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : personnals.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            switch (reponsePoste.code)
                            {
                                case 200:
                                    List<Poste> postes = Utils.ToObjectList<Poste>(reponsePoste.result);
                                    switch (reponseContrat.code)
                                    {
                                        case 200:
                                            List<Contrat> contrats = Utils.ToObjectList<Contrat>(reponseContrat.result);
                                            var tpl = new Tuple<IPagedList<Personnal>,List<Poste>,List<Contrat>>(lstPersonnals, postes, contrats);
                                            ViewBag.CurrentFilter = searching;
                                            return View(tpl);
                                        default:
                                            var tplDefault = new Tuple<IPagedList<Personnal>, List<Poste>, List<Contrat>>(lstPersonnals, postes, new List<Contrat>());
                                            ViewBag.CurrentFilter = searching;
                                            return View(tplDefault);
                                    };

                                default:
                                    var tplDefault1 = new Tuple<IPagedList<Personnal>, List<Poste>, List<Contrat>>(lstPersonnals, new List<Poste>(), new List<Contrat>());
                                    ViewBag.CurrentFilter = searching;
                                    return View(tplDefault1);
                            }

                         case 401:
                            TempData["sms"] = "la session a expiré ";
                            return RedirectToAction("Index", "Home");
                        default:
                            var tpl1 = new Tuple<IPagedList<Personnal>, List<Poste>, List<Contrat>>(new List<Personnal>().ToPagedList(1, 1), new List<Poste>(), new List<Contrat>());
                            ViewBag.CurrentFilter = searching;
                            return View(tpl1);
                    }
                }
                else
                {
                    TempData["sms"] = "la session a expiré";
                    return RedirectToAction("Index", "Home");
                }
                               

            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var tpl1 = new Tuple<IPagedList<Personnal>, List<Poste>, List<Contrat>>(new List<Personnal>().ToPagedList(1, 1), new List<Poste>(), new List<Contrat>());
                ViewBag.CurrentFilter = searching;
                return View(tpl1);
            } 
           
            
        }
       [HttpPost]
        public ActionResult ajouterUser(string prenom, string nom, string id, string adresse, string numeroMatriciule, string naissance, string typeDeRecrutement, string sexe, int type, long? departementID
          , long? filiereID, string professionParent,long? studentID, string relationTuteur, string typeParent, long? niveauEtudeID, string email, string telephone, string compteBancaire, string role,string niveauEtude, HttpPostedFileBase file, long? profession, long? type_contrat, string lieu_naissance,string nationalite)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                     var structureID = Convert.ToInt64(Session["structureID"]);
                     Personnal personnal = null;
                    StudentList student = null;
                     Teacher teacher = null;
                     Parent parent = null;

                    switch (type)
                     {
                         case 1:
                            personnal = new Personnal
                             {
                                 adresse =adresse,
                                 compteBancaire =compteBancaire,
                                 dateCreation = DateTime.Now.Ticks,
                                 nationalite= nationalite,
                                 email = email,
                                 login = telephone,
                                 naissance = naissance,
                                 nom = nom,
                                 numeroMatriciule = numeroMatriciule,
                                 lieu_naissance=lieu_naissance,
                                 password = telephone,
                                 prenom= prenom,
                                 role=(role != null)? role : "PERSONNAL",
                                 contratID =type_contrat,
                                 sexe= sexe,
                                 id = id != null ? Convert.ToInt64(id) : 0,
                                 status=true,
                                 structureID= structureID,
                                 type= profession,
                                 typeUser="PERSONNAL",
                                 typeDeRecrutement= typeDeRecrutement,                             
                                 telephone= telephone
                             };

                             break;
                         case 2:
                            student = new StudentList
                            {
                                 adresse = adresse,
                                 compteBancaire = compteBancaire,
                                 dateCreation = DateTime.Now.Ticks,
                                 nationalite = nationalite,
                                 email = email,
                                 login = telephone,
                                 naissance = naissance,                                 
                                 lieu_naissance = lieu_naissance,
                                 nom = nom,
                                 numeroMatriciule = numeroMatriciule,
                                 password = telephone,
                                 prenom = prenom,
                                 id = id != null ? Convert.ToInt64(id) : 0,
                                 sexe = sexe,
                                 status = true,
                                 structureID = structureID,
                                 role = (role != null) ? role : "STUDENT",
                                 type = profession,
                                 typeUser = "STUDENT",
                                 typeDeRecrutement = typeDeRecrutement,
                                 telephone = telephone,
                                 departementID=departementID,
                                 filiereID=filiereID,
                                 niveauEtudeID=niveauEtudeID
                             };break;
                         case 3:
                             parent = new Parent
                             {
                                 adresse = adresse,
                                 compteBancaire = compteBancaire,
                                 dateCreation = DateTime.Now.Ticks,
                                 nationalite = nationalite,
                                 email = email,
                                 login = telephone,
                                 naissance = naissance,
                                 nom = nom,
                                 numeroMatriciule = numeroMatriciule,
                                 lieu_naissance = lieu_naissance,
                                 password = telephone,
                                 prenom = prenom,
                                 role = (role != null) ? role : "PARENT",
                                 id = id != null ? Convert.ToInt64(id) : 0,
                                 sexe = sexe,
                                 status = true,
                                 structureID = structureID,
                                 typeUser = "PARENT",
                                 typeDeRecrutement = typeDeRecrutement,
                                 telephone = telephone,
                                 professionParent=professionParent,
                                 typeParent=typeParent,
                                 relationTuteur=relationTuteur,
                             }; break;
                         default:
                             teacher = new Teacher
                             {
                                 adresse = adresse,
                                 nationalite = nationalite,
                                 compteBancaire = compteBancaire,
                                 dateCreation = DateTime.Now.Ticks,
                                 email = email,
                                 login = telephone,
                                 naissance = naissance,
                                 nom = nom,
                                 numeroMatriciule = numeroMatriciule,
                                 lieu_naissance = lieu_naissance,
                                 password = telephone,
                                 prenom = prenom,
                                 niveauEtude= niveauEtude,
                                 id = id != null ? Convert.ToInt64(id) : 0,
                                 sexe = sexe,
                                 status = true,
                                 structureID = structureID,
                                 type = profession,
                                 role = (role != null) ? role : "TEACHER",
                                 typeUser = "TEACHER",
                                 typeDeRecrutement = typeDeRecrutement,
                                 telephone = telephone
                             }; break;

                     }


                    if (id  == null)
                    {
						String photo_type = Pluto.Properties.Resources.FILE_PHOTO;
						String piece_type = Pluto.Properties.Resources.FILE_PIECE;

						var typeFile = (parent != null) ? piece_type : photo_type;

						Reponse reponseFile = _fileRepository.AjouterFile(file, typeFile, telephone, Convert.ToString(Session["token"]));

						Reponse reponse = null; 

                        if (reponseFile.code == 200)
                         {

                                reponse = enregisterUser(telephone, type,  personnal,  student,  teacher,  parent);


                               if (reponse.code == 200)
                               {

                                   return Json(new { code = 200, status = true, message = reponse.message});
                               }
                               else
                               {
                                   return Json(new { code = reponse.code, status = false, message = reponse.message });

                               }
                        }
                        else
                        {
                            reponse = enregisterUser(null, type, personnal, student, teacher, parent);


                            if (reponse.code == 200)
                               {

                                   return Json(new { code = 200, status = true, message = reponse.message });
                               }
                               else
                               {
                                   return Json(new { code = reponse.code, status = false, message = reponse.message });

                               }
                       }
                    }
                    else
                    {
                        Reponse reponse = enregisterUser(null, type, personnal, student, teacher, parent);

                        if (reponse.code == 200)
                        {

                            return Json(new { code = 200, status = true, message = reponse.message });
                        }
                        else
                        {
                            return Json(new { code = reponse.code, status = false, message = reponse.message });

                        }
                    } 
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
               
                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

                } 
        }

        public ActionResult Professeurs(int? page, string searching)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _userRepository.ListeUser("TEACHER", Convert.ToString(Session["token"]));
                    Reponse reponsePoste = _posteRepository.ListePoste(structureID, Convert.ToString(Session["token"]));
                    Reponse reponseContrat = _contratRepository.ListeContrat(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<Teacher> teachers = Utils.ToObjectList<Teacher>(reponse.result); 
                            var lstTeachers = (searching != null) ? teachers.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.typeDeRecrutement != null && a.structureID == structureID && a.typeDeRecrutement.StartsWith(searching, StringComparison.OrdinalIgnoreCase)

                                                                                           || a.prenom != null && a.structureID == structureID && a.prenom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.adresse != null && a.structureID == structureID && a.adresse.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.telephone != null && a.structureID == structureID && a.telephone.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : teachers.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            switch (reponsePoste.code)
                            {
                                case 200:
                                    List<Poste> postes = Utils.ToObjectList<Poste>(reponsePoste.result);
                                    switch (reponseContrat.code)
                                    {
                                        case 200:
                                            List<Contrat> contrats = Utils.ToObjectList<Contrat>(reponseContrat.result);
                                            var tpl = new Tuple<IPagedList<Teacher>, List<Poste>, List<Contrat>>(lstTeachers, postes, contrats);
                                            ViewBag.CurrentFilter = searching;
                                            return View(tpl);
                                        default:
                                            var tplDefault = new Tuple<IPagedList<Teacher>, List<Poste>, List<Contrat>>(lstTeachers, postes, new List<Contrat>());
                                            ViewBag.CurrentFilter = searching;
                                            return View(tplDefault);
                                    };

                                default:
                                    var tplDefault1 = new Tuple<IPagedList<Teacher>, List<Poste>, List<Contrat>>(lstTeachers, new List<Poste>(), new List<Contrat>());
                                    ViewBag.CurrentFilter = searching;
                                    return View(tplDefault1);
                            }

                        case 401:
                            TempData["sms"] = "la session a expiré ";
                            return RedirectToAction("Index", "Home");
                        default:
                            var tpl1 = new Tuple<IPagedList<Teacher>, List<Poste>, List<Contrat>>(new List<Teacher>().ToPagedList(1, 1), new List<Poste>(), new List<Contrat>());
                            ViewBag.CurrentFilter = searching;
                            return View(tpl1);
                    }
                }
                else
                {
                    TempData["sms"] = "la session a expiré";
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var tpl1 = new Tuple<IPagedList<Teacher>, List<Poste>, List<Contrat>>(new List<Teacher>().ToPagedList(1, 1), new List<Poste>(), new List<Contrat>());
                ViewBag.CurrentFilter = searching;
                return View(tpl1);
            }


        }
        public ActionResult Etudiants(int? page, string searching)
        {
            try
            {

                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _userRepository.ListeUser("STUDENT", Convert.ToString(Session["token"]));
                    Reponse filieres = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));
                    Reponse departements = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));
                    Reponse niveauEtudes = _niveauEtudeRepository.ListeNiveauEtude(structureID, Convert.ToString(Session["token"]));
                    List<FiliereList> lstFilieres = null;
                    List<DepartementList> lstDepartements = null;
                    List<NiveauEtude> lstNiveauEtudes = null;

                    if (departements.code == 200)
                    {


                        if (filieres.code == 200)
                        {

                            if(niveauEtudes.code == 200)
                            {
                                lstNiveauEtudes = Utils.ToObjectList<NiveauEtude>(niveauEtudes.result);
                                lstFilieres = Utils.ToObjectList<FiliereList>(filieres.result);
                                lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                            }
                            else
                            {
                                lstNiveauEtudes = new List<NiveauEtude>();
                                lstFilieres = Utils.ToObjectList<FiliereList>(filieres.result);
                                lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                            }                            
                        }
                        else
                        {
                            lstNiveauEtudes = new List<NiveauEtude>();
                            lstFilieres = new List<FiliereList>();
                            lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                        }

                    }
                    else
                    {
                        lstNiveauEtudes = new List<NiveauEtude>();
                        lstFilieres = new List<FiliereList>();
                        lstDepartements = new List<DepartementList>();

                    }


                    switch (reponse.code)
                    {
                        case 200:
                            List<StudentList> students = Utils.ToObjectList<StudentList>(reponse.result);
                            var lstStudents = (searching != null) ? students.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.prenom != null && a.structureID == structureID && a.prenom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.adresse != null && a.structureID == structureID && a.adresse.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.telephone != null && a.structureID == structureID && a.telephone.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                  : students.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);

                            var tuple = new Tuple<IPagedList<StudentList>, List<DepartementList>, List<FiliereList>, List<NiveauEtude>>(lstStudents, lstDepartements, lstFilieres,lstNiveauEtudes);
                            ViewBag.CurrentFilter = searching;
                            return View(tuple);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var tuplePers = new Tuple<IPagedList<StudentList>, List<DepartementList>, List<FiliereList>, List<NiveauEtude>>(new List<StudentList>().ToPagedList(1, 1), lstDepartements, lstFilieres, lstNiveauEtudes);
                            return View(tuplePers);
                    }
                }
                else
                {
                    TempData["sms"] = "La session a expiré"; 

					return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                var tuple = new Tuple<IPagedList<StudentList>, List<DepartementList>, List<FiliereList>, List<NiveauEtude>>(new List<StudentList>().ToPagedList(1, 1), new List<DepartementList>(), new List<FiliereList>(), new List<NiveauEtude>());
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<StudentList>().ToPagedList(1, 1);
                return View(pers);
            }

        }
        public ActionResult Details_Etudiants(long? No)
        {
            return View();
        }
        public ActionResult Parents(int? page, string searching)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _userRepository.ListeUser("PARENT", Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<Parent> parents = Utils.ToObjectList<Parent>(reponse.result);
                            var lstParents = (searching != null) ? parents.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.prenom != null && a.structureID == structureID && a.prenom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.adresse != null && a.structureID == structureID && a.adresse.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.telephone != null && a.structureID == structureID && a.telephone.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : parents.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstParents);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<Parent>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<Parent>().ToPagedList(1, 1);
                return View(pers);
            }
        }
        [HttpPost]
        public ActionResult ajouterDepartement( string nom, string id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    DepartementList departement = new DepartementList
                    {
                                nom = nom,                                
                                id = id != null ? Convert.ToInt64(id) : 0,                               
                                status = true,
                                structureID = structureID,                               
                            }; 

               
                    Reponse reponse = _departementRepository.AjouterDepartement(departement, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }
              
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerDepartement(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _departementRepository.bloquerDepartement(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }


        public ActionResult Departements(int? page, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<DepartementList> departements = Utils.ToObjectList<DepartementList>(reponse.result);
                            var lstDepartements = (searching != null) ? departements.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)                                                                    : departements.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstDepartements);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<DepartementList>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<DepartementList>().ToPagedList(1, 1);
                return View(pers);
            }
        }

        public ActionResult ajouterLancerPreinscription(string id, string departementID, string anneeScolaireID, string classeID, long? datePrologement, long? dateDebut, long? dateFin, string filiereID)
        {
         
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    LancerPreinscription lancerPreinscription = new LancerPreinscription
                    {
                        anneeScolaireID = Convert.ToInt64(anneeScolaireID),
                        classeID = Convert.ToInt64(classeID),
                        dateDebut = dateDebut,
                        dateFin = dateFin,
                        datePrologement =datePrologement,
                        departementID = Convert.ToInt64(departementID),
                        filiereID = Convert.ToInt64(filiereID),
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        structureID = structureID
                        
                    };


                    Reponse reponse = _lancerPreinscriptionRepository.AjouterLancerPreinscription(lancerPreinscription, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


           }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            } 
        }
        [HttpPost]
        public ActionResult bloquerLancerPreinscription(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _lancerPreinscriptionRepository.bloquerLancerPreinscription(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }


        public ActionResult LancerPreinscriptions(int? page, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _lancerPreinscriptionRepository.ListeLancerPreinscription(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<LancerPreinscription> lancerPreinscriptions = Utils.ToObjectList<LancerPreinscription>(reponse.result);
                            var lstDepartements = (searching != null) ? lancerPreinscriptions.Where(a => a.structureID == structureID && a.anneeScolaireID.ToString().StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize) : lancerPreinscriptions.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lancerPreinscriptions);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<LancerPreinscription>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<DepartementList>().ToPagedList(1, 1);
                return View(pers);
            }
        }





        [HttpPost]
        public ActionResult ajouterFiliere(string nom, string id , long?departementID)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    FiliereList filiere = new FiliereList
                    {
                        titre = nom,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        structureID=structureID,
                        status = true,
                        departementID = departementID,
                    };


                    Reponse reponse = _filiereRepository.AjouterFiliere(filiere, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerFiliere(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _filiereRepository.bloquerFiliere(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }

       
        public ActionResult Filieres(int? page, long ? departement, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));
                    Reponse departements = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));
                    IPagedList<FiliereList> lstFilieres = null;
                    switch (reponse.code)
                    {
                        case 200 :
                            List<FiliereList> filieres = Utils.ToObjectList<FiliereList>(reponse.result);                          
                            

                            if(searching != null && departement != null)
                            {
                                lstFilieres = filieres.Where(a => a.departementID != null && a.departementID == departement && a.titre != null && a.titre.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);

                            }
                            else if(searching != null && departement == null)
                            {
                                lstFilieres = filieres.Where(a =>  a.titre != null && a.titre.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);

                            }
                            else if(searching == null && departement != null)
                            {
                                lstFilieres = filieres.Where(a => a.departementID != null && a.departementID == departement && a.titre != null).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);

                            }
                            else
                            {
                                lstFilieres = filieres.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            }




                            if(departements.code == 200)
                            {
                                var tuple = new Tuple<IPagedList<FiliereList>,List<DepartementList>>(lstFilieres, Utils.ToObjectList<DepartementList>(departements.result));
                                ViewBag.CurrentFilter = searching;
                                ViewBag.departement = departement;

                                return View(tuple);
                            }
                            else
                            {
                                var tuple = new Tuple<IPagedList<FiliereList>, List<DepartementList>>(lstFilieres, new List<DepartementList>() );
                                ViewBag.CurrentFilter = searching;
                                ViewBag.departement = departement;
                                return View(tuple);
                            }
                            
                            
                            
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<FiliereList>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<DepartementList>().ToPagedList(1, 1);
                return View(pers);
            }
        }
        public ActionResult Poste(int? page, string searching)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _posteRepository.ListePoste(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<Poste> postes = Utils.ToObjectList<Poste>(reponse.result);
                            var lstPostes = (searching != null) ? postes.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                             ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : postes.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstPostes);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<Poste>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<Poste>().ToPagedList(1, 1);
                return View(pers);
            }
        }
        [HttpPost]
        public ActionResult ajouterPoste(string nom, long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Poste poste = new Poste
                    {
                        nom = nom,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        structureID = structureID,
                    };


                    Reponse reponse = _posteRepository.AjouterPoste(poste, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerPoste(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _posteRepository.bloquerPoste(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        public ActionResult Contrat(int? page, string searching)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _contratRepository.ListeContrat(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<Contrat> contrats = Utils.ToObjectList<Contrat>(reponse.result);
                            var lstContrats = (searching != null) ? contrats.Where(a => a.type != null && a.structureID == structureID && a.type.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.categorie != null && a.structureID == structureID && a.categorie.StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           || a.structureID == structureID && a.montant.ToString().StartsWith(searching, StringComparison.OrdinalIgnoreCase)
                                                                                           ).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize)
                                                                    : contrats.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstContrats);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<Contrat>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<Contrat>().ToPagedList(1, 1);
                return View(pers);
            }
        }
        [HttpPost]
        public ActionResult ajouterContrat(string type, string categorie, double? montant, long? duree, long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Contrat contrat = new Contrat
                    {
                        type = type,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        categorie=categorie,
                        montant=montant,
                        duree = duree,
                        structureID = structureID,
                    };


                    Reponse reponse = _contratRepository.AjouterContrat(contrat, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerContrat(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _contratRepository.bloquerContrat(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult ajouterClasse(string nom, string filiere, string id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    ClasseList classe = new ClasseList
                    {
                        nom = nom,
                        filiere = filiere != null ? Convert.ToInt64(filiere) : 0,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        structureID = structureID,
                    };


                    Reponse reponse = _classeRepository.AjouterClasse(classe, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerClasse(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _classeRepository.bloquerClasse(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }


        public ActionResult Classes(int? page, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _classeRepository.ListeClasse(structureID, Convert.ToString(Session["token"]));
                    Reponse reponseFiliere = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:

                            List<FiliereList> filieres =(reponseFiliere.code == 200) ? Utils.ToObjectList<FiliereList>(reponseFiliere.result) : new List<FiliereList>() ;

                            List<ClasseList> classes = Utils.ToObjectList<ClasseList>(reponse.result);
                            var lstClasses = (searching != null) ? classes.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize) : classes.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            var tuple = new Tuple<IPagedList<ClasseList>, List<FiliereList>>(lstClasses, filieres);
                            return View(tuple);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            List<FiliereList> filiers = (reponseFiliere.code == 200) ? Utils.ToObjectList<FiliereList>(reponseFiliere.result) : new List<FiliereList>();
                            var pers = new List<ClasseList>().ToPagedList(1, 1);
                            var tupl = new Tuple<IPagedList<ClasseList>, List<FiliereList>>(pers, filiers);
                            return View(tupl);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                List<FiliereList> filiers = new List<FiliereList>(); 
                var pers = new List<ClasseList>().ToPagedList(1, 1);
                var tupl = new Tuple<IPagedList<ClasseList>, List<FiliereList>>(pers, filiers);
                return View(tupl);
            }
        }
        public ActionResult Scolarite()
        {
            return View();
        }
        public ActionResult Inscription()
        {
            return View();
        }
        public ActionResult Cours()
        {
            return View();
        }

        public ActionResult Emploi_du_temps()
        {
            return View();
        }

        public ActionResult Frais_payes()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult ajouterNiveauEtude(string nom, string id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    NiveauEtude niveauEtude = new NiveauEtude
                    {
                        nom = nom,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        structureID = structureID,
                    };


                    Reponse reponse = _niveauEtudeRepository.AjouterNiveauEtude(niveauEtude, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerNiveauEtude(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _niveauEtudeRepository.bloquerNiveauEtude(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        public ActionResult niveau_etudes(int? page, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _niveauEtudeRepository.ListeNiveauEtude(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<NiveauEtude> niveauEtudes = Utils.ToObjectList<NiveauEtude>(reponse.result);
                            var lstNiveauEtudes = (searching != null) ? niveauEtudes.Where(a => a.nom != null && a.structureID == structureID && a.nom.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize) : niveauEtudes.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstNiveauEtudes);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<NiveauEtude>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<NiveauEtude>().ToPagedList(1, 1);
                return View(pers);
            }
        }

        public ActionResult Frais_impayes()
        {
            return View();
        }

        public ActionResult Evaluations()
        {
            return View();
        }

        public ActionResult Absences_Retards()
        {
            return View();
        }

        public ActionResult Cahiers_de_texte()
        {
            return View();
        }
        
        public ActionResult Configuration()
        {

            try
            {

                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse anneeScolaires = _anneeScolaireRepository.ListeAnneeScolaire(structureID, Convert.ToString(Session["token"]));
                    Reponse filieres = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));
                    Reponse departements = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));
                    Reponse classes = _classeRepository.ListeClasse(structureID, Convert.ToString(Session["token"]));
                    Reponse reponseLancerPreinscription = _lancerPreinscriptionRepository.ListeLancerPreinscription(structureID, Convert.ToString(Session["token"]));

                    List<FiliereList> lstFilieres = null;
                    List<DepartementList> lstDepartements = null;
                    List<ClasseList> lstClasses = null;
                    List<AnneeScolaire> lstAnneeScolaires = null;


                    if (departements.code == 200)
                    {


                        if (filieres.code == 200)
                        {

                            if (classes.code == 200)
                            {
                               
                                if (anneeScolaires.code == 200)
                                {
                                    lstClasses = Utils.ToObjectList<ClasseList>(classes.result);
                                    lstFilieres = Utils.ToObjectList<FiliereList>(filieres.result);
                                    lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                                    lstAnneeScolaires = Utils.ToObjectList<AnneeScolaire>(anneeScolaires.result);

                                }
                                else
                                {
                                    lstAnneeScolaires = new List<AnneeScolaire>();
                                    lstClasses = new List<ClasseList>();
                                    lstFilieres = Utils.ToObjectList<FiliereList>(filieres.result);
                                    lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                                }

                            }
                            else
                            {
                                lstAnneeScolaires = new List<AnneeScolaire>();
                                lstClasses = new List<ClasseList>();
                                lstFilieres = Utils.ToObjectList<FiliereList>(filieres.result);
                                lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                            }
                        }
                        else
                        {
                            lstAnneeScolaires = new List<AnneeScolaire>();
                            lstClasses = new List<ClasseList>();
                            lstFilieres = new List<FiliereList>();
                            lstDepartements = Utils.ToObjectList<DepartementList>(departements.result);
                        }

                    }
                    else
                    {
                        lstClasses = new List<ClasseList>();
                        lstFilieres = new List<FiliereList>();
                        lstDepartements = new List<DepartementList>();
                        lstAnneeScolaires = new List<AnneeScolaire>();


                    }


                    ConfigurationData configurationData = new ConfigurationData
                    {
                        lstAnneeScolaires = lstAnneeScolaires,
                        lstClasses= lstClasses,
                        lstFilieres = lstFilieres,
                        lstDepartements= lstDepartements,
                        lstLancerPreinscriptions=(reponseLancerPreinscription.code == 200) ?  Utils.ToObjectList<LancerPreinscription>(reponseLancerPreinscription.result) :new  List <LancerPreinscription >() 

                };
                    return View(configurationData);


                }
                else
                {
                    TempData["sms"] = "La session a expiré";

                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                ConfigurationData configurationData = new ConfigurationData
                {
                    lstAnneeScolaires = new List<AnneeScolaire>(),
                    lstClasses = new List<ClasseList>(),
                    lstFilieres = new List<FiliereList>(),
                    lstDepartements= new List<DepartementList>(),
                };
                   ViewBag.sms = "Une erreur interne coté client est survenue";
                return View(configurationData);
            } 

                   
        }

        [HttpPost]
        public ActionResult ajouterAnneeScolaire(string nom, string id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    AnneeScolaire anneeScolaire = new AnneeScolaire
                    {
                        libelle = nom,
                        id = id != null ? Convert.ToInt64(id) : 0,
                        status = true,
                        structureID = structureID,
                    };


                    Reponse reponse = _anneeScolaireRepository.AjouterAnneeScolaire(anneeScolaire, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }
        [HttpPost]
        public ActionResult bloquerAnneeScolaire(long? id)
        {
            try
            {


                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse reponse = _anneeScolaireRepository.bloquerAnneeScolaire(id, Convert.ToString(Session["token"]));
                    if (reponse.code == 200)
                    {
                        return Json(new { code = 200, status = true, message = reponse.message });
                    }
                    else
                    {
                        return Json(new { code = reponse.code, status = false, message = reponse.message });

                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            catch
            {

                return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue" });

            }
        }


        public ActionResult AnneeScolaires(int? page, string searching)
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
                {
                    int pageSize = 10;
                    int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    var structureID = Convert.ToInt64(Session["structureID"]);
                    Reponse reponse = _anneeScolaireRepository.ListeAnneeScolaire(structureID, Convert.ToString(Session["token"]));

                    switch (reponse.code)
                    {
                        case 200:
                            List<AnneeScolaire> anneeScolaires = Utils.ToObjectList<AnneeScolaire>(reponse.result);
                            var lstAnneeScolaires = (searching != null) ? anneeScolaires.Where(a => a.libelle != null && a.structureID == structureID && a.libelle.StartsWith(searching, StringComparison.OrdinalIgnoreCase)).OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize) : anneeScolaires.OrderByDescending(a => a.id).ToPagedList(pageIndex, pageSize);
                            ViewBag.CurrentFilter = searching;
                            return View(lstAnneeScolaires);
                        case 401:
                            return RedirectToAction("Index", "Home");
                        default:
                            var pers = new List<AnneeScolaire>().ToPagedList(1, 1);
                            return View(pers);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


           }
            catch
            {
                ViewBag.sms = "Une erreur interne coté client est survenue";
                var pers = new List<AnneeScolaire>().ToPagedList(1, 1);
                return View(pers);
            } 
        }

        public Reponse enregisterUser(string telephone ,int type, Personnal personnal, StudentList student, Teacher teacher, Parent parent)

        {
            Reponse reponse = null;
            switch (type)
            {
                case 1:    
                      personnal.name_logo =(telephone != null) ? telephone + ".png" : null;
                    reponse = _userRepository.AjouterPersonnal(personnal, Convert.ToString(Session["token"]));

                    break;
                case 2:
                    student.name_logo = (telephone != null) ? telephone + ".png" : null;

                    reponse = _userRepository.AjouterStudent(student, Convert.ToString(Session["token"]));
                      break;
                case 3:
                    parent.name_logo = (telephone != null) ? telephone + ".png" : null;

                    reponse = _userRepository.AjouterParent(parent, Convert.ToString(Session["token"]));

                    break;
                default:
                    teacher.name_logo = (telephone != null) ? telephone + ".png" : null;

                    reponse = _userRepository.AjouterTeacher(teacher, Convert.ToString(Session["token"]));

                    break;

            }
            return reponse;
        }
    }
}