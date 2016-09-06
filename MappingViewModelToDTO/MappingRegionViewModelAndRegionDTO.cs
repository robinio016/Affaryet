using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VientVendreMVC.Areas.Admin.Models;

namespace VientVendreMVC.MappingViewModelToDTO
{
    public class MappingRegionViewModelAndRegionDTO
    {
        public MappingRegionViewModelAndRegionDTO()
        {

        }

        public RegionDTO MapRegionViewModelToRegionDTO(RegionViewModel regionViewModel)
        {
            RegionDTO regionDTO = new RegionDTO();
            regionDTO.RegionName = regionViewModel.RegionNames;

            return regionDTO;


        }
    }
}
