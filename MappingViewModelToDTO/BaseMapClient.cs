using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class BaseMapClient
    {
        public MappingAnnonceViewModelAndAnnonceDTO AnnonceVMAndDTOMapping;
        public MappingCategoryViewModelAndCategoryDTO CategoryVMAndDTOMapping;
        public MappingProductViewModelAndProductDTO ProductVMAndDTOMapping;
        public MappingRegionViewModelAndRegionDTO RegionVMAndDTOMapping;
        public MappingUserViewModelAndUserDTO UserVMAndDTOMapping;

        public BaseMapClient()
        {
            AnnonceVMAndDTOMapping = new MappingAnnonceViewModelAndAnnonceDTO();
            CategoryVMAndDTOMapping = new MappingCategoryViewModelAndCategoryDTO();
            ProductVMAndDTOMapping = new MappingProductViewModelAndProductDTO();
            RegionVMAndDTOMapping = new MappingRegionViewModelAndRegionDTO();
            UserVMAndDTOMapping = new MappingUserViewModelAndUserDTO();
        }
    }
}
