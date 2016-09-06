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
    public class AnnonceMapping 
    {
        private Map map;
        private AdminAreaBs adminAreaBs;
        public AnnonceMapping()
        {
           
        }

        public AnnonceDTO AnnonceToAnnonceDTO(Annonce a)
        {
            map = new Map();
            AnnonceDTO annonceDTO = new AnnonceDTO();
            annonceDTO.AnnonceID = a.AnnonceID;
            annonceDTO.AnnonceTitle = a.AnnonceTitle;
            annonceDTO.CreatedDate = a.CreatedDate;
            annonceDTO.Description = a.Description;
            if(a.UserID != null)
                annonceDTO.UserDTOID = a.UserID;

            foreach (var prod in map.baseBs.productBs.GetProductByAnnonce(a.AnnonceID))
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.ProductID = prod.ProductID;
                productDTO.ProductName = prod.ProductName;
                productDTO.ProductPrice = prod.ProductPrice;

                annonceDTO.Products.Add(productDTO);
            }

            /////////////////////////////////////////////////////////
            //retourner les regions d'une annonces
            ///////////////////////////////////////////////////////
            foreach (var reg in map.baseBs.regionBs
                .GetRegionByAnnonce(annonceDTO.AnnonceID)
                .Distinct())
            {
                RegionDTO regionDTO = new RegionDTO();
                regionDTO.RegionID = reg.RegionID;
                regionDTO.RegionName = reg.RegionName;

                annonceDTO.Regions.Add(regionDTO);
            }
            //////////////////////////////////////////////////////////
            return annonceDTO;
        }
        public Annonce AnnonceDTOToAnnonce(AnnonceDTO annonceDto)
        {
            Annonce _annonce = new Annonce();
            //mapping
            _annonce.AnnonceTitle = annonceDto.AnnonceTitle;
            _annonce.CreatedDate = annonceDto.CreatedDate;
            _annonce.Description = annonceDto.Description;
            var user = adminAreaBs.userBs.GetUserByID(annonceDto.AnnonceID);
            _annonce.User = user;

            foreach (int regId in annonceDto.RegionsDTOID)
            {
                adminAreaBs = new AdminAreaBs();
                Region reg = new Region();
                reg = adminAreaBs.regionBs.GetRegionByID(regId);
                _annonce.Regions.Add(reg);
            }
            return _annonce;
        }

    }
}
