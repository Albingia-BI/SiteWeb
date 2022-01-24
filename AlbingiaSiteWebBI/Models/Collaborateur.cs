using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbingiaSiteWebBI.Models
{
    public class Collaborateur
    {
        public string ColBranche { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public long IdCollaborateur { get; set; }
        public string Equipe { get; set; }
        public string Service { get; set; }
    }
}