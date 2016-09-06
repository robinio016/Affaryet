using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseBs
    {
        public UserBs userBs { get; set; }
        public RegionBs regionBs { get; set; }
        public CategoryBs categoryBs { get; set; }
        public ProductBs productBs { get; set; }
        public AnnonceBs annonceBs { get; set; }

        public CommentUserProdBs commentUserProdBs { get; set; }
        public PhotoInfoBs photoInfoBs;
        public BaseBs()
        {
            userBs = new UserBs();
            regionBs = new RegionBs();
            categoryBs = new CategoryBs();
            productBs = new ProductBs();
            annonceBs = new AnnonceBs();
            commentUserProdBs = new CommentUserProdBs();
            photoInfoBs = new PhotoInfoBs();
        }

    }
}
