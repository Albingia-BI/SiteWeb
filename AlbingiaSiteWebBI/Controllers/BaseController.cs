using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlbingiaSiteWebBI.Controllers
{
    public class BaseController : Controller
    {
        public (string, long, DateTime) GetHomeCache()
        {
            string branche = HttpContext.Session["Branche"] == null ? string.Empty : HttpContext.Session["Branche"].ToString();
            long collaborateur = HttpContext.Session["Collaborateur"] == null ? 0 : (HttpContext.Session["Collaborateur"] as long? ?? 0);
            DateTime date = HttpContext.Session["Date"] == null ? DateTime.Now : (HttpContext.Session["Date"] as DateTime? ?? DateTime.Now);

            return (branche, collaborateur, date);
        }

        public void SetHomeCache(string branche, long collaborateur, DateTime date)
        {
            HttpContext.Session.Add("Branche", branche);
            HttpContext.Session.Add("Collaborateur", collaborateur);
            HttpContext.Session.Add("Date", date); 
        }
        public void SetIndemnisationCache(string service, string branche, long collaborateur)
        {
            HttpContext.Session.Add("Service", service);
            HttpContext.Session.Add("Branche", branche);
            HttpContext.Session.Add("Collaborateur", collaborateur);
        }
    }
}