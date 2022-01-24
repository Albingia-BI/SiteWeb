using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbingiaSiteWebBI.Models
{
    public class ModelHome
    {
        public string Branche { get; set; }
        public long Collaborateur { get; set; }
        public DateTime Date { get; set; }
        public List<Collaborateur> Collabs { get; set; }
        public List<string> Branches { get; set; }
    }
}