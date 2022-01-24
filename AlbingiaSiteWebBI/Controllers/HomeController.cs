using AlbingiaSiteWebBI.Models;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AlbingiaSiteWebBI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var modelHome = new ModelHome();

            using (ALB_APPEntities db = new ALB_APPEntities())
            {
                var branches = (from name in db.IND_COLLABORATEUR  select name.COL_BRANCHE).Distinct().ToList();
                
                var collaborateur = (from name in db.IND_COLLABORATEUR
                                     select new Collaborateur
                                     {
                                         ColBranche = name.COL_BRANCHE,
                                         Nom = name.COL_NOM,
                                         Prenom = name.COL_PRENOM,
                                         IdCollaborateur = name.COL_ID,
                                     }).GroupBy(p => new { p.Nom, p.Prenom }).Select(p =>p.FirstOrDefault())
                                     .OrderBy(n => n.Nom).OrderBy(p => p.Prenom)
                                     .ToList();
                

                this.ViewBag.Titre = "Accueil";

                (modelHome.Branche, modelHome.Collaborateur, modelHome.Date) = GetHomeCache();

                modelHome.Collabs = string.IsNullOrWhiteSpace(modelHome.Branche) ? new List<Collaborateur>() : collaborateur.Where(x => x.ColBranche == modelHome.Branche).Distinct().ToList();

                modelHome.Branches = branches;

            }
            return View(modelHome);

        }

        public JsonResult GetCollaborateurByBranche(string branche)
        {
            using (ALB_APPEntities db = new ALB_APPEntities())
            {
                var collaborateur = (from name in db.IND_COLLABORATEUR
                                     select new Collaborateur
                                     {
                                         ColBranche = name.COL_BRANCHE,
                                         Nom = name.COL_NOM,
                                         Prenom = name.COL_PRENOM,
                                         IdCollaborateur = name.COL_ID,
                                     }).Distinct().Where(b => b.ColBranche == branche).GroupBy(p => new { p.Nom, p.Prenom })
                                     .Select(p => p.FirstOrDefault()).OrderBy(n => n.Nom).OrderBy(p => p.Prenom)
                                     .ToList();

                return Json(collaborateur, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Valider(ModelHome homeModel)
        {
            var titre = homeModel.Branche;
            var date = homeModel.Date;
            SetHomeCache(titre, homeModel.Collaborateur, date);
            return RedirectToAction("Index", "Indemnisation", new { id = homeModel.Collaborateur, date, titre });
        }

    }
}