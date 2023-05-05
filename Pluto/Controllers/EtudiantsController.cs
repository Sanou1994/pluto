using Pluto.Models;
using Pluto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using static Pluto.Models.Tempon;

namespace Pluto.Controllers
{
    public class EtudiantsController : Controller
    {
       
        private IAuthentificationRepository _authentificationRepository;
        private IFileRepository _fileRepository;
        private IUserRepository _userRepository;
        private INiveauEtudeRepository _niveauEtudeRepository;
        private IFiliereRepository _filiereRepository;
        private IDepartementRepository _departementRepository;
		private IInscriptionRepository _inscriptionRepository;


		public EtudiantsController(
            IAuthentificationRepository authentificationRepository,
            IUserRepository userRepository,
            IFileRepository fileRepository,
             INiveauEtudeRepository niveauEtudeRepository,
            IFiliereRepository filiereRepository,
			IInscriptionRepository inscriptionRepository,
			IDepartementRepository departementRepository
            )
        {
            _authentificationRepository = authentificationRepository;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _niveauEtudeRepository = niveauEtudeRepository;
            _departementRepository = departementRepository;
            _filiereRepository = filiereRepository;
			_inscriptionRepository = inscriptionRepository;


		}
		// GET: Etudiants
		public ActionResult Index()
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null && Session["fullName"] != null )
                {
                    var fullname = Convert.ToString(Session["fullName"]) ;
                    var name_logo = Convert.ToString(Session["name_logo"]);
                    ViewBag.Name_logo = name_logo;
                    ViewBag.Fullname = fullname;
                    return View();

                }
                else
                {
                    TempData["sms"] = "la session a expiré";
                    return RedirectToAction("Index", "Home");
                }

                }
            catch
            {
                TempData["sms"] = "la session a expiré";
                return RedirectToAction("Index", "Home");
            }

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
        public ActionResult Profil()
        {
            try
             {
                if(Session["token"] != null && Session["id"] != null && Session["structureID"] != null && Session["fullName"] != null)
                {
                    var etudiantID = Convert.ToInt64(Session["id"]); 
                    var token = Convert.ToString(Session["token"]);
                    var structureID = Convert.ToInt64(Session["structureID"]);

					Reponse checkEtudiantExist = _userRepository.ChercherUser(etudiantID,"STUDENT", token);
                    Reponse filieres = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));
                    Reponse departements = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));
                    Reponse niveauEtudes = _niveauEtudeRepository.ListeNiveauEtude(structureID, Convert.ToString(Session["token"]));
                    var profileData = new ProfilData();
                    List<FiliereList> lstFilieres = null;
                    List<DepartementList> lstDepartements = null;
                    List<NiveauEtude> lstNiveauEtudes = null;

                    if (departements.code == 200)
                    {


                        if (filieres.code == 200)
                        {

                            if (niveauEtudes.code == 200)
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



                    if(checkEtudiantExist.code == 200)
                    {
                        profileData.student= Utils.ToObject<StudentList>(checkEtudiantExist.result);
						Reponse checkParentExist = _userRepository.ChercherUser(profileData.student.parentID, "PARENT", token);

						if (checkParentExist.code == 200)
						{
							Parent pt = Utils.ToObject<Parent>(checkParentExist.result);

							profileData.parent =(pt != null) ? pt : new Parent();


						}
						else
						{
							profileData.parent = new Parent();
						}


					}
					else
                    {
                        profileData.student = new StudentList();
						profileData.parent = new Parent();
					}

                    profileData.lstFilieres= lstFilieres;
                    profileData.lstDepartements= lstDepartements;
                    profileData.lstNiveauEtudes= lstNiveauEtudes;
                    var fullname = Convert.ToString(Session["fullName"]);
                    var name_logo = Convert.ToString(Session["name_logo"]);
                    ViewBag.Name_logo = name_logo;
                    ViewBag.Fullname = fullname;
                    return View(profileData);

                }
                else
                {
                    TempData["sms"] = "la session a expiré";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                TempData["sms"] = "un problème est surenu ";
                return RedirectToAction("Index", "Home");
            } 
        }
		public ActionResult ajouterUser(string prenom, string nom, string id, string adresse, string numeroMatriciule, string naissance, string typeDeRecrutement, string sexe, int type, long? departementID
		 , long? filiereID, string professionParent, long? studentID, string relationTuteur, string typeParent, long? niveauEtudeID, string email, string telephone, string compteBancaire, string role, string niveauEtude, HttpPostedFileBase file, long? profession, long? type_contrat, string lieu_naissance, string nationalite)
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
								role = (role != null) ? role : "PERSONNAL",
								contratID = type_contrat,
								sexe = sexe,
								id = id != null ? Convert.ToInt64(id) : 0,
								status = true,
								structureID = structureID,
								type = profession,
								typeUser = "PERSONNAL",
								typeDeRecrutement = typeDeRecrutement,
								telephone = telephone
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
								departementID = departementID,
								filiereID = filiereID,
								niveauEtudeID = niveauEtudeID
							}; break;
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
								contratID = type_contrat,
								sexe = sexe,
								id =( id != null) ? Convert.ToInt64(id) : 0,
								status = true,
								structureID = structureID,
								type = profession,
								typeUser = "PARENT",
								typeDeRecrutement = typeDeRecrutement,
								telephone = telephone,
								typeParent = typeParent,
								professionParent = professionParent,
								relationTuteur = relationTuteur

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
								niveauEtude = niveauEtude,
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


					if (id == null)
					{
						String photo_type = Pluto.Properties.Resources.FILE_PHOTO;
						String piece_type = Pluto.Properties.Resources.FILE_PIECE;

						var typeFile = (parent != null ) ? piece_type : photo_type;

						Reponse reponseFile = _fileRepository.AjouterFile(file, typeFile, telephone, Convert.ToString(Session["token"]));
						Reponse reponse = null;

						if (reponseFile.code == 200)
						{

							reponse = enregisterUser(telephone, type, personnal, student, teacher, parent);


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
			catch(Exception e) 
			{

				return Json(new { code = 500, status = false, message = "Une erreur interne coté client est survenue"+e.Message });

			}
		}



		public ActionResult Mettre_a_jour_profil()
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null && Session["fullName"] != null)
                {
                    var etudiantID = Convert.ToInt64(Session["id"]);
                    var token = Convert.ToString(Session["token"]);
                    var structureID = Convert.ToInt64(Session["structureID"]);

                    Reponse checkEtudiantExist = _userRepository.ChercherUser(etudiantID, "STUDENT", token);
					Reponse filieres = _filiereRepository.ListeFiliere(structureID, Convert.ToString(Session["token"]));
                    Reponse departements = _departementRepository.ListeDepartement(structureID, Convert.ToString(Session["token"]));
                    Reponse niveauEtudes = _niveauEtudeRepository.ListeNiveauEtude(structureID, Convert.ToString(Session["token"]));
                    var mettre_a_jour_profilData = new Mettre_a_jour_profilData();
                    List<FiliereList> lstFilieres = null;
                    List<DepartementList> lstDepartements = null;
                    List<NiveauEtude> lstNiveauEtudes = null;
					if (departements.code == 200)
                    {


                        if (filieres.code == 200)
                        {

                            if (niveauEtudes.code == 200)
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



                    if (checkEtudiantExist.code == 200)
                    {
                        mettre_a_jour_profilData.student = Utils.ToObject<StudentList>(checkEtudiantExist.result);

                        if(mettre_a_jour_profilData.student.parentID != null)
                        {
							Reponse getParent = _userRepository.ChercherUser(mettre_a_jour_profilData.student.parentID, "PARENT", token);
                            if (checkEtudiantExist.code == 200)
                            {
								mettre_a_jour_profilData.parent = Utils.ToObject<Parent>(getParent.result);

							}
							else
                            {
								mettre_a_jour_profilData.parent = new Parent();

							}
						}
						else
                        {
                            mettre_a_jour_profilData.parent = new Parent();

						}


                    }
                    else
                    {
                        mettre_a_jour_profilData.student = new StudentList();
                    }

					mettre_a_jour_profilData.lstFilieres = lstFilieres;
                    mettre_a_jour_profilData.lstDepartements = lstDepartements;
                    mettre_a_jour_profilData.lstNiveauEtudes = lstNiveauEtudes;
                    var fullname = Convert.ToString(Session["fullName"]);
                    var name_logo = Convert.ToString(Session["name_logo"]);
                    ViewBag.Name_logo = name_logo;
                    ViewBag.Fullname = fullname;
					ViewBag.etudiantID = etudiantID;
					return View(mettre_a_jour_profilData);

                }
                else
                {
                    TempData["sms"] = "la session a expiré";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                TempData["sms"] = "un problème est surenu ";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Tests()
        {
            return View();
        }
        public ActionResult Preinscription()
        {
            try
            {
                if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null && Session["fullName"] != null)
                {
					var studentID = Convert.ToInt64(Session["id"]);
					var token = Convert.ToString(Session["token"]);
					Reponse checkEtudiantExist = _userRepository.ChercherUser(studentID, "STUDENT", token);
					var fullname = Convert.ToString(Session["fullName"]);
					var name_logo = Convert.ToString(Session["name_logo"]);
					ViewBag.Name_logo = name_logo;
					ViewBag.Fullname = fullname;


					if (checkEtudiantExist.code == 200)
                    {
						StudentList student = Utils.ToObject<StudentList>(checkEtudiantExist.result);
                        var preinscription = (student.inscriptions.Count() != 0) ? student.inscriptions.OrderByDescending(a => a.id).FirstOrDefault() : new Inscription() ;


                        var preinscriptionData = new PreinscriptionData
                        {
                            etat= preinscription.etat,
                            id= preinscription.id,
                            type= preinscription.type,
                            adresse_complete = preinscription.adresse_complete,
                            adresse_email_responsable = preinscription.adresse_email_responsable,
                            adresse_responsable = preinscription.adresse_responsable,
                            autre_contact = preinscription.autre_contact,
                            date_de_naissance = student.naissance,
                            dernier_diplome_obtenue = preinscription.dernier_diplome_obtenue,
                            dernier_etablissement_frequente = preinscription.dernier_etablissement_frequente,
                            email = student.email,
                            frais_dossier= preinscription.frais_dossier,
                            indicatif= preinscription.indicatif,
                            lieu_de_naissance= student.lieu_naissance,
                            nationalite= student.nationalite,
                            niveau= preinscription.niveau,
                            nom= student.nom,
                            nom_responsable= preinscription.nom_responsable,
                            numéro_de_téléphone_du_responsable= preinscription.numéro_de_téléphone_du_responsable,
                            pays_de_naissance= preinscription.pays_de_naissance,
                            pays_de_residence= preinscription.pays_de_residence,
                            prenom= student.prenom,
                            profession_du_responsable= preinscription.profession_du_responsable,
                            programme_choisi= preinscription.programme_choisi,
                            responsable_financier= preinscription.responsable_financier,
                            sexe= student.sexe,
                            telephone= student.telephone,
                            specialite= preinscription.specialite,

                        };
						return View(preinscriptionData);




					}
					else
                    {
                        var preinscriptionData = new PreinscriptionData
                        {
                            message = "Ce personnage n'existe plus"
                        };
						return View(preinscriptionData);

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
                TempData["sms"] = "la session a expiré";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
		public ActionResult Preinscription(PreinscriptionData preinscriptionData)
		{
			try
			{
				if (Session["token"] != null && Session["id"] != null && Session["structureID"] != null)
				{
					var studentID = Convert.ToInt64(Session["id"]);
					var token = Convert.ToString(Session["token"]);
					Reponse reponsePhoto_Inscripotion = _fileRepository.AjouterFile(preinscriptionData.photo_identite, Pluto.Properties.Resources.PHOTO_PREINSCRIPTION, preinscriptionData.telephone, Convert.ToString(Session["token"]));
					Reponse reponsePhotocopieCinb = _fileRepository.AjouterFile(preinscriptionData.photocopie_piece_identite, Pluto.Properties.Resources.FILE_PHOTOCOPIE_PIECE_IDENTITE_PREINSCRIPTION, preinscriptionData.telephone, Convert.ToString(Session["token"]));
					Reponse reponsePhotocopieReleve = _fileRepository.AjouterFile(preinscriptionData.photocopie_releve_notes, Pluto.Properties.Resources.FILE_PHOTOCOPIE_RELEVE_NOTE_PREINSCRIPTION, preinscriptionData.telephone, Convert.ToString(Session["token"]));
					Reponse reponsePhotocopieAttestation = _fileRepository.AjouterFile(preinscriptionData.photocopie_attestation, Pluto.Properties.Resources.FILE_PHOTOCOPIE_PREINSCRIPTION_ATTESTATION, preinscriptionData.telephone, Convert.ToString(Session["token"]));

                    // IL RESTE A CONVERTIR ET CREER L'INSCRIPTION



					InscriptionList Inscription = Utils.ToObject<InscriptionList>(preinscriptionData);
					Reponse reponse = _inscriptionRepository.AjouterInscription(Inscription, token);

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
					TempData["sms"] = "la session a expiré";
					return RedirectToAction("Index", "Home");
				}

			}
			catch
			{
				TempData["sms"] = "la session a expiré";
				return RedirectToAction("Index", "Home");
			}
		}
		public ActionResult Inscription()
        {
            return View();
        }
        public ActionResult Cours_magistrals()
        {
            return View();
        }
        public ActionResult Cours_en_ligne()
        {
            return View();
        }
        public ActionResult Travaux_diriges()
        {
            return View();
        }
        public ActionResult Travaux_pratiques()
        {
            return View();
        }
        public ActionResult Interrogations()
        {
            return View();
        }
        public ActionResult Devoirs()
        {
            return View();
        }
        public ActionResult Examens()
        {
            return View();
        }
        public ActionResult Rattrapages()
        {
            return View();
        }
		public Reponse enregisterUser(string telephone, int type, Personnal personnal, StudentList student, Teacher teacher, Parent parent)

		{
			Reponse reponse = null;
			switch (type)
			{
				case 1:
					personnal.name_logo = (telephone != null) ? telephone + ".png" : null;
					reponse = _userRepository.AjouterPersonnal(personnal, Convert.ToString(Session["token"]));

					break;
				case 2:
					student.name_logo = (telephone != null) ? telephone + ".png" : null;

					reponse = _userRepository.AjouterStudent(student, Convert.ToString(Session["token"]));
					break;
				case 3:
					parent.name_logo = (telephone != null) ? telephone + ".pdf" : null;

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