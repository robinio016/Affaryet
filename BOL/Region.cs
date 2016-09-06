using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Region
    {
        public Region()
        {
            Annonces = new List<Annonce>();
        }
        public int RegionID { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Annonce> Annonces { get; set; }
    }
}
