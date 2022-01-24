using AlbingiaSiteWebBI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace AlbingiaSiteWebBI.Controllers
{
    public class IndemnisationController : BaseController
    {

        public ActionResult Index(long id, DateTime date, string titre)
        {
            var modelIndemnisation = new ModelIndemnisation();

            using (ALB_APPEntities db = new ALB_APPEntities())
            {
                var modelConst = (from com in db.IND_COMPTAGE_TACHE
                                  join suj in db.IND_SUJET on com.ICT_ID_SUJET equals suj.SUJ_ID
                                  join col in db.IND_COLLABORATEUR on com.ICT_ID_COLLABORATEUR equals col.COL_ID
                                  where (com.ICT_ID_COLLABORATEUR == id && com.ICT_DATE == date)
                                 && (suj.SUJ_BRANCHE == col.COL_BRANCHE &&  suj.COL_SERVICE == col.COL_SERVICE || string.IsNullOrEmpty(suj.COL_SERVICE))
                                  select new { com.ICT_ENTREE, com.ICT_SORTIE, col.COL_SERVICE, suj.SUJ_ID, col.COL_ID, com.ICT_ID, col.COL_EQUIPE, suj.SUJ_LIBELLE, col.COL_NOM, col.COL_PRENOM, suj.SUJ_TRI, com.ICT_ID_COLLABORATEUR });

                var nomColo = (from nm in db.IND_COLLABORATEUR
                           where nm.COL_ID == id
                           select nm.COL_PRENOM + " " + nm.COL_NOM).First();

                var equipes = (from equi in db.IND_COLLABORATEUR
                               where (equi.COL_PRENOM + " " + equi.COL_NOM == nomColo) && equi.COL_BRANCHE == titre
                               select new { equi.COL_EQUIPE, equi.COL_ID }).Distinct().ToList();

                var services = (from ser in db.IND_COLLABORATEUR
                               where (ser.COL_PRENOM + " " + ser.COL_NOM == nomColo) && ser.COL_BRANCHE == titre && ser.COL_EQUIPE == modelConst.FirstOrDefault().COL_EQUIPE
                               select new { ser.COL_SERVICE, ser.COL_ID }).Distinct().ToList();

                var subjects = (from sujet in modelConst where sujet.COL_ID == id orderby sujet.SUJ_TRI select new { sujet.ICT_ID, sujet.SUJ_ID, sujet.SUJ_LIBELLE, sujet.ICT_ENTREE, sujet.ICT_SORTIE }).Distinct().ToList();

                modelIndemnisation.SujetConstruction = subjects.Select( p => new SujetModel { 
                    IdComptage = p.ICT_ID,
                    Id = p.SUJ_ID,
                    LibelleSujet = p.SUJ_LIBELLE,
                    Entree = p.ICT_ENTREE,
                    Sortie = p.ICT_SORTIE
                }).ToList();

                modelIndemnisation.Services = services.Select(x => new Services() { Id = x.COL_ID, Service = x.COL_SERVICE }).ToList();
                modelIndemnisation.Equipes = equipes.Select(x => new Equipes() { Id = x.COL_ID, Equipe = x.COL_EQUIPE }).ToList();
                modelIndemnisation.PrenomCollaborateur = ((from nom in db.IND_COLLABORATEUR where nom.COL_ID == id select (nom.COL_PRENOM)).Distinct().First());
                modelIndemnisation.NomCollaborateur = ((from nom in db.IND_COLLABORATEUR where nom.COL_ID == id select (nom.COL_NOM)).Distinct().First());
                modelIndemnisation.Id = id;
                modelIndemnisation.DateModif = date;
                modelIndemnisation.Service = modelConst.FirstOrDefault().COL_ID.ToString();
                modelIndemnisation.ServiceNom = modelConst.FirstOrDefault().COL_SERVICE.ToString();
                modelIndemnisation.Equipe = modelConst.FirstOrDefault().COL_ID.ToString();
                modelIndemnisation.EquipeNom = modelConst.FirstOrDefault().COL_EQUIPE.ToString();
                modelIndemnisation.Titre = titre;

            }

            return View(modelIndemnisation);
        }
        [HttpPost]
        public string GetLibelleByService(long id, string titre, DateTime date)
        {
            return $"/Indemnisation/Index/{id}?date={Url.Encode(date.ToString("MM/dd/yyyy"))}&titre={titre}";
        }

        [HttpPost]
        public ActionResult Create(ModelIndemnisation modelIndemnisation)
        {
            var id = modelIndemnisation.Id;
            var date = modelIndemnisation.DateModif;
            var titre = modelIndemnisation.Titre;

            using (ALB_APPEntities db = new ALB_APPEntities())
            {
                var idCol = new SqlParameter
                {
                    ParameterName = "@ID_COLLAB",
                    DbType = DbType.Double,
                    Value = id
                };

                var dateMJA = new SqlParameter
                {
                    ParameterName = "@DATE_DEBUT",
                    DbType = DbType.Date,
                    Value = date
                };

                if (this.ModelState.IsValid)
                {
                    foreach (var item in modelIndemnisation.SujetConstruction)
                    {
                        var col = db.IND_COMPTAGE_TACHE.First(u => u.ICT_ID == item.IdComptage);
                        col.ICT_SORTIE = item.Sortie;
                        col.ICT_ENTREE = item.Entree;
                        col.ICT_MODIFICATEUR = User.Identity.Name;
                        db.SaveChanges();
                    }

                }

                db.Database.ExecuteSqlCommand("exec Mise_A_Jour_Apres_Validation @ID_COLLAB , @DATE_DEBUT ", idCol, dateMJA);

            }
            SetIndemnisationCache(modelIndemnisation.Service, modelIndemnisation.Titre, modelIndemnisation.Id);

            return RedirectToAction("Index", "Indemnisation", new { id, titre, date });
        }
    }
}