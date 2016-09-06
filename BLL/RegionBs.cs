using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RegionBs
    {
        private RegionProvider regionProvider;
        public RegionBs()
        {
            regionProvider = new RegionProvider();

        }

        public List<Region> GetAllRegion()
        {
            return regionProvider.GetAllRegion();
        }

        public Region GetRegionByID(int id)
        {
            return regionProvider.GetRegionByID(id);
        }
        public List<Region> GetRegionByAnnonce(int annID)
        {
            return regionProvider.GetRegionByAnnonce(annID);
        }
        public void CreateRegion(Region region)
        {
            regionProvider.CreateRegion(region);
        }

        public void DeleteRegion(int id)
        {
            regionProvider.DeleteRegion(id);
        }

        public void UpdateRegion(Region region)
        {
            regionProvider.UpdateRegion(region);
        }
    }
}
