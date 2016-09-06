using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{
    public class BaseMap
    {
        public UserMapping userMapping { get; set; }
        public AnnonceMapping annonceMapping { get; set; }
        public ProductMapping productMapping { get; set; }
        public CategoryMapping categoryMapping { get; set; }
        public RegionMapping regionMapping { get; set; }
        public PhotoInfoMapping photoInfoMapping { get; set; }
        public BaseBs baseBs { get; set; }


        public BaseMap()
        {
            userMapping = new UserMapping();
            annonceMapping = new AnnonceMapping();
            productMapping = new ProductMapping();
            categoryMapping = new CategoryMapping();
            regionMapping = new RegionMapping();
            photoInfoMapping = new PhotoInfoMapping();

            baseBs = new BaseBs();
        }
    }
}
