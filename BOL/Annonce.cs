using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Annonce
    {
        public Annonce()
        {
            //instance des listes so when i use Products of annoce will not be null
            Products = new List<Product>();

            Regions = new List<Region>();

            UserID = new int();
        }
        public int AnnonceID { get; set; }
        public string AnnonceTitle { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        
        //
        public int? UserID { get; set; }
        public virtual User User { get; set; } 
        
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}
