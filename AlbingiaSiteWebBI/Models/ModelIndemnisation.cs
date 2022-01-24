using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbingiaSiteWebBI.Models
{
    public class ModelIndemnisation
    {
        public long Id { get; set; }
        public DateTime DateModif { get; set; }
        public string Titre { get; set; }
        public string NomCollaborateur { get; set; }
        public string PrenomCollaborateur { get; set; }
        public string Service { get; set; }
        public List<Equipes> Equipes { get; set; }
        public List<Services> Services { get; set; }
        public string Equipe { get; set; }
        public string ServiceNom { get; set; }
        public string EquipeNom { get; set; }

        public int? Entree { get; set; }
        public int? Sortie { get; set; }
        public List<int?> EntreeConstruction { get; set; }
        public List<int?> SortieConstruction { get; set; }
        public List<string> ServiceConstruction { get; set; }
        public List<string> EquipeConstruction { get; set; }
        public List<SujetModel> SujetConstruction { get; set; }
        public List<string> BrancheConstruction { get; set; }
        public IDictionary<List<string>, IDictionary<List<int?>, List<int?>>> ConstValeur { get; set; }
        public IDictionary<List<int?>, List<int?>> sortieEntree { get; set; }
        public long IdComptage { get; set; }
    }

    public class Services
    {
        public string Service { get; set; }
        public long Id { get; set; }
    }
    public class Equipes
    {
        public string Equipe { get; set; }
        public long Id { get; set; }
    }
}