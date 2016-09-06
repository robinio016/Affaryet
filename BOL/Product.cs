using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Product
    {
        public Product()
        {
            Categories = new List<Category>();
            CommentUserProds = new List<CommentUserProd>();
            PhotoInfos = new List<PhotoInfo>();
            AnnonceID = new int();
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public string IsApproved { get; set; }
        //use fluent api : foriegn key for product
        public int? AnnonceID { get; set; }
        public virtual Annonce Annonce { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<CommentUserProd> CommentUserProds { get; set; }

        public virtual ICollection<PhotoInfo> PhotoInfos { get; set; }
    }
}
