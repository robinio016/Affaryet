using BLL;
using BOL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingBOLAndDTO
{//pour le moment le mapping est assuré pour l'envoie de données vers la partie client uniquement.
    public class RegionMapping 
    {
        private Map map;
        public RegionMapping()
        {
           
        }

        public RegionDTO RegionToRegionDTO(Region r)
        {
            map = new Map();
            RegionDTO regionDTO = new RegionDTO();
            regionDTO.RegionID = r.RegionID;
            regionDTO.RegionName = r.RegionName;

            foreach(var ann in map.baseBs.annonceBs.GetAnnonceByRegion(r.RegionID))
            {
                AnnonceDTO annonceDTO = new AnnonceDTO();
                annonceDTO.AnnonceID = ann.AnnonceID;
                annonceDTO.AnnonceTitle = ann.AnnonceTitle;
                annonceDTO.CreatedDate = ann.CreatedDate;
                annonceDTO.Description = ann.Description;

                regionDTO.Annonces.Add(annonceDTO);
            }

            return regionDTO;
        }

        public Region regionDTOToRegion(RegionDTO region)
        {
            Region _region = new Region();
            //mapping
            return _region;
        }
    }
}
