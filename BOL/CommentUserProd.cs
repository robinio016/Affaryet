using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class CommentUserProd
    {
        public int CommentUserProdID { get; set; }
        public int? UserID { get; set; }
        public virtual User User{get;set;}
        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }
        public string Comment { get; set; }
        public DateTime? PostDate { get; set; }
    }
}
