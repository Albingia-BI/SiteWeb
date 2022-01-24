using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbingiaSiteWebBI.Models
{
    public class SujetModel
    {
        public long Id { get; set; }
        public long IdComptage { get; set; }
        public string LibelleSujet { get; set; }
        public int? Entree { get; set; }
        public int? Sortie { get; set; }
    }
}