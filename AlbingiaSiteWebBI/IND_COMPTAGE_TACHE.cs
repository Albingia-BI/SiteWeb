//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlbingiaSiteWebBI
{
    using System;
    using System.Collections.Generic;
    
    public partial class IND_COMPTAGE_TACHE
    {
        public long ICT_ID { get; set; }
        public Nullable<long> ICT_ID_COLLABORATEUR { get; set; }
        public Nullable<long> ICT_ID_SUJET { get; set; }
        public Nullable<System.DateTime> ICT_DATE { get; set; }
        public Nullable<int> ICT_STOCK_INITIAL { get; set; }
        public Nullable<int> ICT_STOCK_FINAL { get; set; }
        public Nullable<int> ICT_ENTREE { get; set; }
        public Nullable<int> ICT_SORTIE { get; set; }
        public Nullable<System.DateTime> ICT_DATE_CREATION { get; set; }
        public Nullable<System.DateTime> ICT_DATE_MODIFICATION { get; set; }
        public string ICT_CREATEUR { get; set; }
        public string ICT_MODIFICATEUR { get; set; }
        public Nullable<System.DateTime> Loading_Date { get; set; }
    
        public virtual IND_COLLABORATEUR IND_COLLABORATEUR { get; set; }
        public virtual IND_SUJET IND_SUJET { get; set; }
    }
}
